using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : MonoBehaviour
{
    // Mesh
    Mesh mesh;
    MeshRenderer meshRenderer;

    // Triangle vertices
    List<Vector3> vertices;
    List<int> triangles;

    public Material material;

    // Cone Parameters
    public float height;
    public float radius;
    public int segments;

    Vector3 pos;

    float angle = 0.0f;
    float angleAmount = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize mesh & material
        gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = material;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        // Initialize vertices 
        vertices = new List<Vector3>();
        pos = new Vector3();

        angleAmount = 2 * Mathf.PI / segments;
        angle = 0.0f;

        // Center of high point of cone
        pos.x = 0.0f;
        pos.y = height;
        pos.z = 0.0f;
        vertices.Add(new Vector3(pos.x, pos.y, pos.z));

        // Center of base of cone
        pos.y = 0.0f;
        vertices.Add(new Vector3(pos.x, pos.y, pos.z));

        // Add vertices around center of cone
        for(int i = 0; i < segments; i++)
        {
            pos.x = radius * Mathf.Sin(angle);
            pos.z = radius * Mathf.Cos(angle);

            vertices.Add(new Vector3(pos.x, pos.y, pos.z));

            angle -= angleAmount;
        }

        // Assign vertices to mesh
        mesh.vertices = vertices.ToArray();

        // Initialize list of triangles
        triangles = new List<int>();

        for (int i = 2; i < segments + 1; i++)
        {
            triangles.Add(0);
            triangles.Add(i + 1);
            triangles.Add(i);
        }

        // Close
        triangles.Add(0);
        triangles.Add(2);
        triangles.Add(segments + 1);

        mesh.triangles = triangles.ToArray();

        //transform.localScale.x = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
