using UnityEngine;

public class CardDrag : MonoBehaviour
{
    private Camera mainCam;
    private bool isDragging = false;
    private float zOffset;
    private Vector3 dragOffset;

    void Start()
    {
        mainCam = Camera.main;
    }

    void OnMouseDown()
    {
        isDragging = true;

        // Save offset between card position and click point
        zOffset = mainCam.WorldToScreenPoint(transform.position).z;
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zOffset;
        dragOffset = transform.position - mainCam.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zOffset;
        Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePoint) + dragOffset;

        transform.position = new Vector3(worldPos.x, transform.position.y, worldPos.z); // keeps card level
    }

    void OnMouseUp()
    {
        isDragging = false;
    }
}