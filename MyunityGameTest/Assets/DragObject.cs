using UnityEngine;
using UnityEngine.Events; // Needed for UnityEvent

public class DragObject : MonoBehaviour
{
    // The main camera in the scene.
    // We get a reference to this in the Start method.
    private Camera mainCamera;

    // The point in world space where the drag started.
    private Vector3 dragStartPoint;

    // The offset between the mouse position and the object's pivot.
    private Vector3 offset;

    // The plane on which we will be dragging the object.
    // We create a new plane at the object's initial position with a normal pointing up.
    private Plane dragPlane;

    // --- NEW: Add a UnityEvent for tap actions ---
    // This allows you to hook up functions from the Unity Inspector.
    public UnityEvent onTap;
    // ---------------------------------------------

    void Start()
    {
        // Get the main camera and check if it exists.
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Main Camera not found! Please tag a camera as 'MainCamera'.");
            this.enabled = false; // Disable the script if no camera is found.
        }

        // Set up the drag plane. This plane is parallel to the ground (xz-plane)
        // and its position is based on the object's initial position.
        dragPlane = new Plane(Vector3.up, transform.position);
    }

    void OnMouseDown()
    {
        // This method is called by Unity when the mouse button is pressed over the collider.
        
        // Raycast from the mouse cursor into the scene.
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float distance;

        // Check if the ray hits our drag plane.
        if (dragPlane.Raycast(ray, out distance))
        {
            // Get the world point where the ray intersected the plane.
            dragStartPoint = ray.GetPoint(distance);

            // Calculate the initial offset between the object's position and the mouse click point.
            offset = transform.position - dragStartPoint;
        }

        // --- NEW: Invoke the 'onTap' event when the mouse is pressed down on the object.
        onTap.Invoke();
        // -----------------------------------------------------------------------------
    }

    void OnMouseDrag()
    {
        // This method is called continuously while the mouse is held down over the collider.
        
        // Raycast again from the current mouse position.
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float distance;

        // Check if the ray hits the drag plane.
        if (dragPlane.Raycast(ray, out distance))
        {
            // Get the new world point where the ray intersected the plane.
            Vector3 currentPoint = ray.GetPoint(distance);

            // Calculate the new position for the object by adding the initial offset.
            Vector3 newPosition = currentPoint + offset;

            // Update the object's position.
            transform.position = newPosition;
        }
    }
}
