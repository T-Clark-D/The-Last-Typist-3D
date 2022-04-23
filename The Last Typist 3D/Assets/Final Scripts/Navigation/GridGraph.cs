using UnityEngine;

using System.Collections.Generic;

/// <summary>
/// Very quick basic graph implementation that was created to be used only for COMP 476 Lab on pathfinding.
/// It is most likely not suitable for more practical use cases without modification.
/// </summary>
public class GridGraph : MonoBehaviour
{
    [SerializeField, HideInInspector] public List<GridGraphNode> nodes = new List<GridGraphNode>();
    [SerializeField] public GameObject nodePrefab;

    public int Count => nodes.Count;

    public void Clear()
    {
        nodes.Clear();
        gameObject.DestroyChildren();
    }

    public void Remove(GridGraphNode node)
    {
        if (node == null || !nodes.Contains(node)) return;

        foreach (GridGraphNode n in node.adjacencyList)
            n.adjacencyList.Remove(node);

        nodes.Remove(node);
    }

    public void GenerateGrid(bool checkCollisions = true)
    {
        Clear();

        GridGraphNode[,] nodeGrid = new GridGraphNode[generationGridRows, generationGridColumns];

        float width = (generationGridColumns > 0 ? generationGridColumns - 1 : 0) * generationGridCellSize;
        float height = (generationGridRows > 0 ? generationGridRows - 1 : 0) * generationGridCellSize;
        Vector3 genPosition = new Vector3(transform.position.x - (width / 2), transform.position.y, transform.position.z - (height / 2));

        // first pass : generate nodes
        for (int r = 0; r < generationGridRows; ++r)
        {
            float startingX = genPosition.x;
            for (int c = 0; c < generationGridColumns; ++c)
            {
                if (checkCollisions)
                {
                    if (Physics.CheckBox(genPosition, Vector3.one / 2, Quaternion.identity, LayerMask.GetMask("Obstacle")))
                    {
                        genPosition = new Vector3(genPosition.x + generationGridCellSize, genPosition.y, genPosition.z);
                        continue;
                    }
                }

                GameObject obj;
                if (nodePrefab == null)
                    obj = new GameObject("Node", typeof(GridGraphNode));
                else
                    obj = Instantiate(nodePrefab);

                obj.name = $"Node ({nodes.Count})";
                obj.tag = "Node";
                obj.transform.parent = transform;
                obj.transform.position = genPosition;

                GridGraphNode addedNode = obj.GetComponent<GridGraphNode>();                
                nodes.Add(addedNode);
                nodeGrid[r, c] = addedNode;

                genPosition = new Vector3(genPosition.x + generationGridCellSize, genPosition.y, genPosition.z);
            }
            genPosition = new Vector3(startingX, genPosition.y, genPosition.z + generationGridCellSize);
        }

        // second pass : create adjacency lists (edges)
        int[,] operations = new int[,] { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 }, { 1, 1 }, { 1, -1 }, { -1, 1 }, { -1, -1 } };
        for (int r = 0; r < generationGridRows; ++r)
        {
            for (int c = 0; c < generationGridColumns; ++c)
            {
                if (nodeGrid[r, c] == null) continue;

                for (int i = 0; i < operations.GetLength(0); ++i)
                {
                    int[] neighborId = new int[2] { r + operations[i, 0], c + operations[i, 1] };

                    // check to see if operation brings us out of bounds
                    if (neighborId[0] < 0 || neighborId[0] >= nodeGrid.GetLength(0) || neighborId[1] < 0 || neighborId[1] >= nodeGrid.GetLength(1))
                        continue;

                    GridGraphNode neighbor = nodeGrid[neighborId[0], neighborId[1]];

                    if (neighbor != null)
                    {
                        if (checkCollisions)
                        {
                            Vector3 direction = neighbor.transform.position - nodeGrid[r, c].transform.position;
                            if (Physics.Raycast(nodeGrid[r, c].transform.position, direction, direction.magnitude, LayerMask.GetMask("Obstacle")))
                                continue;
                        }

                        nodeGrid[r, c].adjacencyList.Add(neighbor);
                    }
                }
            }
        }
    }

    public List<GridGraphNode> GetNeighbors(GridGraphNode node)
    {
        return node.adjacencyList;
    }

#region grid_generation_properties

    // grid generation properties
    [HideInInspector, Min(0)] public int generationGridColumns = 1;
    [HideInInspector, Min(0)] public int generationGridRows = 1;
    [HideInInspector, Min(0)] public float generationGridCellSize = 1;

#if UNITY_EDITOR
    [Header("Gizmos")]
    /// <summary>WARNING: This property is used by Gizmos only and is removed from the build. DO NOT reference it outside of Editor-Only code.</summary>
    public float _nodeGizmoRadius = 0.5f;
    /// <summary>WARNING: This property is used by Gizmos only and is removed from the build. DO NOT reference it outside of Editor-Only code.</summary>
    public Color _edgeGizmoColor = Color.white;

    private void OnDrawGizmos()
    {
        if (nodes == null) return;

        // nodes
        foreach (GridGraphNode node in nodes)
        {
            if (node == null) continue;

            Gizmos.color = node._nodeGizmoColor;
            Gizmos.DrawSphere(node.transform.position, _nodeGizmoRadius);

            Gizmos.color = _edgeGizmoColor;
            List<GridGraphNode> neighbors = GetNeighbors(node);
            foreach (GridGraphNode neighbor in neighbors)
            {
                Gizmos.DrawLine(node.transform.position, neighbor.transform.position);
            }
        }
    }
#endif
#endregion
}
