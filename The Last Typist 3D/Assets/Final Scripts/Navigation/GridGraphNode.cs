using UnityEngine;

using System.Collections.Generic;

/// <summary>
/// This class represents a node in a <see cref="GridGraph"/>. The code will run in edit mode as well as play mode 
/// because of the attribute [<see cref="ExecuteInEditMode"/>]. As such, if you add any code to this class, 
/// you should check if the code is running in edit mode or not (you can check the Application.isPlaying property).
/// </summary>
[ExecuteInEditMode]
public class GridGraphNode : MonoBehaviour
{
    [SerializeField] public List<GridGraphNode> adjacencyList = new List<GridGraphNode>();

    private GridGraph graph;
    private GridGraph Graph
    {
        get
        {
            if (graph == null)
                graph = GetComponentInParent<GridGraph>();
            
            return graph;
        }
    }

    private void OnDestroy()
    {
        if (Graph != null)
        {
            Graph.Remove(this);
        }
    }

#if UNITY_EDITOR
    public Color _nodeGizmoColor = new Color(Color.white.r, Color.white.g, Color.white.b, 0.5f);
#endif
}
