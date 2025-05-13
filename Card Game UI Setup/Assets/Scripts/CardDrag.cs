using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CardDrag : MonoBehaviour
{
    private Camera mainCam;
    private Rigidbody rb;
    private bool isDragging = false;
    private float zOffset;
    private Vector3 dragOffset;
    private float hoverHeight = 0.2f;
    private float initialY;

    void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        isDragging = true;
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        zOffset = mainCam.WorldToScreenPoint(transform.position).z;

        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zOffset;
        Vector3 worldMouse = mainCam.ScreenToWorldPoint(mousePoint);

        dragOffset = transform.position - worldMouse;
        initialY = transform.position.y + hoverHeight; // elevate once on pickup
        transform.position = new Vector3(transform.position.x, initialY, transform.position.z);
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zOffset;
        Vector3 worldMouse = mainCam.ScreenToWorldPoint(mousePoint);

        Vector3 flatPosition = worldMouse + dragOffset;
        transform.position = new Vector3(flatPosition.x, initialY, flatPosition.z); // lock Y
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}