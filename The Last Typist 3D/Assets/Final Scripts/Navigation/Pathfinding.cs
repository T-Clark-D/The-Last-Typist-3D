using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

public class Pathfinding : MonoBehaviour
{
    public bool debug;
    [SerializeField] private GridGraph graph;

    // A delegate function which accepts any heuristic function to be used later
    public delegate float Heuristic(Transform start, Transform end);
    // For rendering the line between consecutive path points
    public LineRenderer lineRenderer;
    // Where the path starts
    public GridGraphNode startNode;
    // Where the path ends
    public GridGraphNode goalNode;
    // Prefabs for use in visualizing the closed, open and path points
    public GameObject openPointPrefab;
    public GameObject closedPointPrefab;
    public GameObject pathPointPrefab;
    // A reference to the NPC which will be walking the path
    public GameObject npcObj;
    // The path of nodes from the start node to the goal node
    public List<GridGraphNode> path;
    // The NPC movement script attached to the NPC object
    private MazeZombie npc;
    // A counter for where we are in the path when traversing it
    private int pathNodeIndex = 0;

    private void Start()
    {
        // Setting up the line renderer
        lineRenderer.startColor = Color.magenta;
        lineRenderer.endColor = Color.magenta;
        // Setting up the NPC
        npc = npcObj.GetComponent<MazeZombie>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // On-click behavior during game runtime
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Node")))
            {
                // Reset the nodes if we already have a start + goal node
                if (startNode != null && goalNode != null)
                {
                    startNode = null;
                    goalNode = null;
                    ClearPoints();
                    npc.targetObj = null;
                    pathNodeIndex = 0;
                }
                // Set the start node if it's not set
                if (startNode == null)
                {
                    startNode = hit.collider.gameObject.GetComponent<GridGraphNode>();
                    npcObj.transform.position = startNode.transform.position;
                }
                // Set the goal node if it's not set
                else if (goalNode == null)
                {
                    goalNode = hit.collider.gameObject.GetComponent<GridGraphNode>();
                    // Pass the heuristic (here, diagonal distance) to the FindPath function, then set the path with the returned one
                    path = FindPath(startNode, goalNode, DiagonalDistanceHeuristic, true);
                    //path = FindPath(startNode, goalNode);
                    // Set the target of the NPC to the first node in the path
                    npc.targetObj = path[pathNodeIndex].gameObject;
                }
            }
        }
        // Once the path is set...
        if (startNode != null && goalNode != null && path != null)
        {
            // Render the lines connecting the path nodes in sequence
            lineRenderer.positionCount = path.Count;
            for(int i = 0; i < path.Count; i++)
            {
                lineRenderer.SetPosition(i, path[i].transform.position);
            }
        }
    }

    private void FixedUpdate()
    {
        // If we aren't close to the target node and the goal node exists...
        if (npc.distance < 0.5f && goalNode != null)
        {
            Debug.Log("Path node #" + pathNodeIndex + " reached.");
            // Walk down the path by incrementing through the nodes and setting them as targets
            if (pathNodeIndex < (path.Count-1))
            {
                pathNodeIndex++;
                npc.targetObj = path[pathNodeIndex].gameObject;
                Debug.Log("Moving to path node #" + pathNodeIndex + ".");
            }
        }
    }

    public List<GridGraphNode> FindPath(GridGraphNode start, GridGraphNode goal, Heuristic heuristic = null, bool isAdmissible = true)
    {
        if (graph == null) return new List<GridGraphNode>();

        // if no heuristic is provided then set heuristic = 0
        if (heuristic == null) heuristic = (Transform s, Transform e) => 0;

        List<GridGraphNode> path = new List<GridGraphNode>();
        bool solutionFound = false;

        // dictionary to keep track of g(n) values (movement costs)
        Dictionary<GridGraphNode, float> gnDict = new Dictionary<GridGraphNode, float>();
        gnDict.Add(start, default);

        // dictionary to keep track of f(n) values (movement cost + heuristic)
        Dictionary<GridGraphNode, float> fnDict = new Dictionary<GridGraphNode, float>();
        fnDict.Add(start, heuristic(start.transform, goal.transform) + gnDict[start]);

        // dictionary to keep track of our path (came_from)
        Dictionary<GridGraphNode, GridGraphNode> pathDict = new Dictionary<GridGraphNode, GridGraphNode>();
        pathDict.Add(start, null);

        List<GridGraphNode> openList = new List<GridGraphNode>();
        openList.Add(start);

        OrderedDictionary closedODict = new OrderedDictionary();

        while (openList.Count > 0)
        {
            // mimic priority queue and remove from the back of the open list (lowest fn value)
            GridGraphNode current = openList[openList.Count - 1];
            openList.RemoveAt(openList.Count - 1);

            closedODict[current] = true;

            // early exit
            if (current == goal && isAdmissible)
            {
                solutionFound = true;
                break;
            }
            else if (closedODict.Contains(goal))
            {
                // early exit strategy if heuristic is not admissible (try to avoid this if possible)
                float gGoal = gnDict[goal];
                bool pathIsTheShortest = true;

                foreach (GridGraphNode entry in openList)
                {
                    if (gGoal > gnDict[entry])
                    {
                        pathIsTheShortest = false;
                        break;
                    }
                }

                if (pathIsTheShortest) break;
            }

            List<GridGraphNode> neighbors = graph.GetNeighbors(current);
            foreach (GridGraphNode n in neighbors)
            {
                // Assume the cost for movement in any direction is the same
                float movement_cost = 1;

                // Skip this node in the evaluation if we've already evaluated it
                if (closedODict.Contains(n))
                {
                    //continue;
                }

                // Set the gn value for the neighbor node
                var g_next = gnDict[current] + movement_cost;
                // If the gn dictionary doesn't contain the neighbor...
                if (!gnDict.ContainsKey(n) || gnDict[n] > g_next)
                {
                    // Set the movement cost of the neighbor
                    gnDict[n] = g_next;
                    // Calculate fn using the sum of gn and the heuristic
                    fnDict[n] = g_next + heuristic(n.transform, goal.transform);
                    // Update the openList using FakePQListInsert() function
                    FakePQListInsert(openList, fnDict, n);
                    // Set the "came from" node of the neighbor to this node
                    pathDict[n] = current;
                }
            }
        }

        // If the closed list contains the goal node then we have found a solution
        if (!solutionFound && closedODict.Contains(goal))
            solutionFound = true;

        if (solutionFound)
        {
            // Create the path by traversing the previous nodes in the pathDict, from the goal to the start
            path = new List<GridGraphNode>();
            path.Add(goal);
            // Custom method to accomplish this!
            RecursivePathSearch(path, pathDict, start, goal);
            // Reverse the path since we started adding nodes from the goal 
            path.Reverse();
        }

        if (debug)
        {
            ClearPoints();

            List<Transform> openListPoints = new List<Transform>();
            foreach (GridGraphNode node in openList)
            {
                openListPoints.Add(node.transform);
            }
            SpawnPoints(openListPoints, openPointPrefab, Color.magenta);

            List<Transform> closedListPoints = new List<Transform>();
            foreach (DictionaryEntry entry in closedODict)
            {
                GridGraphNode node = (GridGraphNode) entry.Key;
                if (solutionFound && !path.Contains(node))
                    closedListPoints.Add(node.transform);
            }
            SpawnPoints(closedListPoints, closedPointPrefab, Color.red);

            if (solutionFound)
            {
                List<Transform> pathPoints = new List<Transform>();
                foreach (GridGraphNode node in path)
                {
                    pathPoints.Add(node.transform);
                }
                SpawnPoints(pathPoints, pathPointPrefab, Color.green);
            }
        }

        return path;
    }

    private void SpawnPoints(List<Transform> points, GameObject prefab, Color color)
    {
        for (int i = 0; i < points.Count; ++i)
        {
#if UNITY_EDITOR
            // Scene view visuals
            points[i].GetComponent<GridGraphNode>()._nodeGizmoColor = color;
#endif

            // Game view visuals
            GameObject obj = Instantiate(prefab, points[i].position, Quaternion.identity, points[i]);
            obj.name = "DEBUG_POINT";
            obj.transform.localPosition += Vector3.up * 0.5f;
        }
    }

    private void ClearPoints()
    {
        foreach (GridGraphNode node in graph.nodes)
        {
            for (int c = 0; c < node.transform.childCount; ++c)
            {
                if (node.transform.GetChild(c).name == "DEBUG_POINT")
                {
                    Destroy(node.transform.GetChild(c).gameObject);
                }
            }
        }
    }

    /// <summary>
    /// mimics a priority queue here by inserting at the right position using a loop
    /// not a very good solution but ok for this lab example
    /// </summary>
    /// <param name="pqList"></param>
    /// <param name="fnDict"></param>
    /// <param name="node"></param>
    private void FakePQListInsert(List<GridGraphNode> pqList, Dictionary<GridGraphNode, float> fnDict, GridGraphNode node)
    {
        if (pqList.Count == 0)
            pqList.Add(node);
        else
        {
            for (int i = pqList.Count - 1; i >= 0; --i)
            {
                if (fnDict[pqList[i]] > fnDict[node])
                {
                    pqList.Insert(i + 1, node);
                    break;
                }
                else if (i == 0)
                    pqList.Insert(0, node);
            }
        }
    }

    // The heuristic used to calculate the value of the current node in A*
    public float DiagonalDistanceHeuristic(Transform start, Transform end)
    {
        // The proportial euclidian distance cost (traversing a square diagonally)
        float ddCost = 1 / (Mathf.Sqrt(2));
        // The cost for moving across the edge of a square space
        float orthCost = 1;
        // The math (pre-established in the slides)
        return
            (
                orthCost * 
                (
                Mathf.Abs(start.position.x - end.position.x)
                +
                Mathf.Abs(start.position.z - end.position.z)
                )
                +
                (ddCost - 2 * orthCost)
                *
                Mathf.Min(
                    Mathf.Abs(start.position.x - end.position.x)
                    ,
                    Mathf.Abs(start.position.z - end.position.z)
                    )
            );
    }

    // Fancy recursive way to iterate through the pathDict and re-construct the actual path
    private void RecursivePathSearch(List<GridGraphNode> path, Dictionary<GridGraphNode, GridGraphNode> pathDict, GridGraphNode start, GridGraphNode next)
    {
        if (next == start)
        {
            return;
        }
        path.Add(pathDict[next]);
        RecursivePathSearch(path, pathDict, start, pathDict[next]);
    }
}
