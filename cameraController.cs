using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // The target (initial tetrahedron) to look at
    public float distance = 5.0f; // Distance from the target
    public float sensitivity = 1; // Sensitivity of the camera rotation
    public float minYAngle = -20f; // Minimum vertical angle
    public float maxYAngle = 80f; // Maximum vertical angle
    public float zoomSpeed = 1.0f; // Speed of zooming in and out

    private float rotationX = 0f; // Rotation around the x-axis (up/down)
    private float rotationY = 0f; // Rotation around the y-axis (left/right)

    void Start()
    {
        // Set the initial position of the camera
        Vector3 angles = transform.eulerAngles;
        rotationX = angles.x;
        rotationY = angles.y;
    }

    void Update()
    {
        // Use arrow keys to control rotation
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rotationX -= sensitivity; // Rotate up
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rotationX += sensitivity; // Rotate down
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotationY -= sensitivity; // Rotate left
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rotationY += sensitivity; // Rotate right
        }

        // Clamp the vertical rotation
        rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);

        // Zooming in and out using the mouse wheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            distance -= scroll * zoomSpeed; // Adjust distance based on scroll
            distance = Mathf.Clamp(distance, 1f, 20f); // Clamp the distance to a range (adjust values as needed)
        }

        // Calculate the new camera rotation
        Quaternion rotation = Quaternion.Euler(rotationX, rotationY, 0);
        Vector3 position = target.position - rotation * Vector3.forward * distance;

        // Update the camera's position and rotation
        transform.position = position;
        transform.LookAt(target.position); // Always look at the target
    }
}
