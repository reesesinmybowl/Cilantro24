using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class CardDrag : MonoBehaviour
{
    private Camera mainCam;
    private Rigidbody rb;
    private bool isDragging = false;
    private float zOffset;

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
        rb.isKinematic = true; // prevent physics from fighting during drag

        zOffset = mainCam.WorldToScreenPoint(transform.position).z;
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zOffset;
        Vector3 targetPos = mainCam.ScreenToWorldPoint(mousePoint);
        transform.position = targetPos + Vector3.up * 0.2f; // hover slightly
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.isKinematic = false;
        rb.useGravity = true;
    }
}