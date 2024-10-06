using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetrahedron
{
    public GameObject gameObject;
    public int outwardFace; // 0: Red, 1: Blue, 2: Green, 3: Yellow

    public Tetrahedron(GameObject obj, int face)
    {
        gameObject = obj;
        outwardFace = face;
    }
}

public class PyraminxManager : MonoBehaviour
{
    public GameObject tetrahedronPrefab; // Reference to the tetrahedron prefab
    private List<Tetrahedron> tetrahedrons = new List<Tetrahedron>(); // List to hold tetrahedrons

    void Start()
    {
        CreatePyraminx();
    }

    void CreatePyraminx()
    {
        // Calculate vertices of the original tetrahedron
        Vector3 p0 = new Vector3(0, 0, 0);
        Vector3 p1 = new Vector3(1, 0, 0);
        Vector3 p2 = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
        Vector3 p3 = new Vector3(0.5f, Mathf.Sqrt(0.75f) / 2, Mathf.Sqrt(0.75f) / 2); // Top vertex

        // Create the top tetrahedron
        GameObject topTetra = Instantiate(tetrahedronPrefab, p3, Quaternion.identity);
        tetrahedrons.Add(new Tetrahedron(topTetra, 2)); // Assuming the top face is outward facing

        // Position offsets for the lower layer tetrahedrons
        Vector3[] lowerPositions = new Vector3[]
        {
            new Vector3(0.5f, 0, 0), // Left base vertex of top tetrahedron
            new Vector3(1, 0, 0), // Right base vertex of top tetrahedron
            new Vector3(0.5f, 0, Mathf.Sqrt(0.75f)), // Base vertex of the top tetrahedron
        };

        // Create lower tetrahedrons
        for (int i = 0; i < lowerPositions.Length; i++)
        {
            GameObject lowerTetra = Instantiate(tetrahedronPrefab, lowerPositions[i], Quaternion.identity);
            tetrahedrons.Add(new Tetrahedron(lowerTetra, i)); // Different outward face for each
        }
    }

    // Additional methods to manipulate tetrahedrons later
}
