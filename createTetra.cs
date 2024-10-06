using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(LineRenderer))] // ** Added LineRenderer requirement

public class createTetra : MonoBehaviour {

    public bool sharedVertices = false; //true;

    private float edgeLength = 1.0f; // Edge length of the tetrahedron
    private Vector3 p0, p1, p2, p3; // Vertices of the tetrahedron

    // Vector3 p0 = new Vector3(0, 0, 0);
    // Vector3 p1 = new Vector3(1, 0, 0);
    // Vector3 p2 = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
    // Vector3 p3 = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3); // ** updated to divide by 2 for a regular tetrahedron
    
    Mesh mesh;
    LineRenderer lineRenderer; // ** Added LineRenderer variable

    private void InitializeVertices()
    {
        float height = Mathf.Sqrt(2f / 3f) * edgeLength; // Calculate height based on edge length

        p0 = new Vector3(0, 0, 0); // Vertex A
        p1 = new Vector3(edgeLength, 0, 0); // Vertex B
        p2 = new Vector3(edgeLength / 2, 0, Mathf.Sqrt(3) / 2 * edgeLength); // Vertex C
        p3 = new Vector3(edgeLength / 2, height, edgeLength / (2 * Mathf.Sqrt(3))); // Vertex D
    }

    public Vector3[] getVectors()
    {
        Vector3[] vertex = new Vector3[] { p0, p1, p2, p3 };
        return vertex;
    }

    public void Rebuild()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null)
        {
            Debug.LogError("MeshFilter not found!");
            return;
        }

        mesh = meshFilter.sharedMesh;
        if (mesh == null)
        {
            meshFilter.mesh = new Mesh();
            mesh = meshFilter.sharedMesh;
        }
        mesh.Clear();

        if (sharedVertices)
        {
            mesh.vertices = new Vector3[] { p0, p1, p2, p3 };
            mesh.triangles = new int[]{
                0,1,2,
                0,2,3,
                2,1,3,
                0,3,1
            };
            // UVs for shared vertices
            mesh.uv = new Vector2[]{
                new Vector2(0,0),
                new Vector2(1,0),
                new Vector2(0.5f, 1),
                new Vector2(0.5f, 0.5f) //** added UV for the fourth vertex for better texturing
            };
        }
        else
        {
            mesh.vertices = new Vector3[]{
                p0,p1,p2,
                p0,p2,p3,
                p2,p1,p3,
                p0,p3,p1
            };
            mesh.triangles = new int[]{ // 3 vertices per face
                0,1,2, // base
                3,4,5, // side 1
                6,7,8, // side 2
                9,10,11 // side 3
            };

            Vector2 uv0 = new Vector2(0, 0);
            Vector2 uv1 = new Vector2(1, 0);
            Vector2 uv2 = new Vector2(0.5f, 1);

            mesh.uv = new Vector2[]{
                uv0,uv1,uv2,
                uv1,uv2,uv0,
                uv2,uv0,uv1,
                uv1,uv2,uv0 //** adjusted UVs to ensure proper texturing
            };
        }

        // Assign colors for Pyraminx faces: red, blue, green, yellow
        Color[] color = new Color[mesh.vertices.Length]; // Adjusted size to 12 for the tetrahedron
        color[0] = Color.blue;   // Base face
        color[1] = Color.blue;
        color[2] = Color.blue;

        color[3] = Color.red;    // Side face 1
        color[4] = Color.red;
        color[5] = Color.red;

        color[6] = Color.green;  // Side face 2
        color[7] = Color.green;
        color[8] = Color.green;

        color[9] = Color.yellow;  // Side face 3
        color[10] = Color.yellow;
        color[11] = Color.yellow;

        mesh.colors = color;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        
        // ** Call to draw edges
        DrawEdges(); // ** added method to draw edges
    }

    // ** New method to draw the edges of the tetrahedron
    private void DrawEdges()
    {
        lineRenderer = GetComponent<LineRenderer>(); // ** Get the LineRenderer component
        lineRenderer.positionCount = 6; // ** Set the number of points for edges

        // ** Define the points for the edges
        Vector3[] edgePositions = new Vector3[] {
            p0, p1, // Base edge 1
            p1, p2, // Base edge 2
            p2, p0, // Base edge 3
            p0, p3, // Side edge 1
            p1, p3, // Side edge 2
            p2, p3  // Side edge 3
        };

        lineRenderer.SetPositions(edgePositions); // ** Set the positions of the edges

        // ** Configure the LineRenderer
        lineRenderer.startWidth = 0.05f; // ** Set line width
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // ** Use default shader
        lineRenderer.startColor = Color.black; // ** Set color to black
        lineRenderer.endColor = Color.black; // ** Ensure both ends are the same color
    }

    // Use this for initialization
    void Start () {
        InitializeVertices();

        // Rebuild the mesh
        Rebuild();
    }
    
    // Update is called once per frame
    void Update () {
        
    }
}
