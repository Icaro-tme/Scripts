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

public class pyraminxManager : MonoBehaviour
{
    public GameObject tetrahedronPrefab; // Reference to the tetrahedron prefab
    private List<Tetrahedron> tetrahedrons = new List<Tetrahedron>(); // List to hold tetrahedrons

    void Start()
    {
        CreatePyraminx();
    }

    void CreatePyraminx()
    {
        // Position offsets for the layers of the Pyraminx
        Vector3[] positions = new Vector3[]
        {
            Vector3.zero, // Top tetrahedron
            new Vector3(-0.5f, -0.866f, 0), // Left lower tetrahedron
            new Vector3(0.5f, -0.866f, 0), // Right lower tetrahedron
            new Vector3(0, -0.866f * 2, 0), // Bottom tetrahedron
            // Add more positions as necessary for the complete Pyraminx
        };

        // Create and assign outward faces
        int[] outwardFaces = new int[] { 2, 0, 1, 3 }; // Example: 0 is red, 1 is blue, etc.

        for (int i = 0; i < positions.Length; i++)
        {
            GameObject tetra = Instantiate(tetrahedronPrefab, positions[i], Quaternion.identity);
            tetrahedrons.Add(new Tetrahedron(tetra, outwardFaces[i]));
        }
    }

    // Additional methods to manipulate tetrahedrons later
}
