using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyraminxManager : MonoBehaviour
{
    public GameObject tetrahedron; // prefab of the tetrahedron
    private GameObject[][] layers; // 2D array to hold tetrahedrons for each layer
    public int layersCount = 3; // Total layers in the Pyraminx

    void Start()
    {
        layers = new GameObject[layersCount][];

        // Create each layer of the Pyraminx
        for (int i = 0; i < layersCount; i++)
        {
            int tetrahedronsInLayer = layersCount - i; // Tetrahedrons decrease with each layer
            layers[i] = new GameObject[tetrahedronsInLayer];

            for (int j = 0; j < tetrahedronsInLayer; j++)
            {
                // Calculate position for each tetrahedron
                float xPos = j * (1f / (tetrahedronsInLayer - 1)) - 0.5f; // Center the layer horizontally
                float yPos = i * Mathf.Sqrt(2f / 3f); // Height of the layer
                float zPos = -i * (1f / (layersCount - 1)) * (Mathf.Sqrt(2f) / 2f); // Pull back for perspective

                // Instantiate the tetrahedron
                layers[i][j] = Instantiate(tetrahedron, new Vector3(xPos, yPos, zPos), Quaternion.identity);

                // Set the parent of each tetrahedron to keep hierarchy
                layers[i][j].transform.parent = transform; // Keeps the hierarchy organized
            }
        }

        // Adjust rotations or transformations as necessary for the tetrahedrons
        PositionTetrahedrons();
    }

    void PositionTetrahedrons()
    {
        // Here, you can set specific positions or rotations for tetrahedrons if necessary
    }

    void Update()
    {
        // Update logic, if necessary, such as rotation or transformations
    }
}
