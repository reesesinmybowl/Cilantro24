using UnityEngine;
using UnityEngine.Rendering;

public class CardDrag : MonoBehaviour
{
    private Camera mainCam;
    private bool isDragging = false;
    private float zOffset;
    private Vector3 dragOffset;
    private MeshRenderer meshRenderer;
    private CardDisplay cardDisplay; // Reference to CardDisplay

    private float hoverY = 0.2f;
    private float liftedY = 0.5f;

    void Start()
    {
        mainCam = Camera.main;
        meshRenderer = GetComponent<MeshRenderer>();
        cardDisplay = GetComponent<CardDisplay>();
    }

    void OnMouseDown()
    {
        isDragging = true;

        if (meshRenderer != null)
        {
            meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
        }

        if (cardDisplay != null)
        {
            cardDisplay.FlipToFront();
        }

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
        transform.position = new Vector3(worldPos.x, liftedY, worldPos.z);
    }

    void OnMouseUp()
    {
        isDragging = false;

        if (meshRenderer != null)
        {
            meshRenderer.shadowCastingMode = ShadowCastingMode.On;
        }

        // Optionally: You could keep the card face up or back to face down here
        // Example:
        // cardDisplay.ShowBack();
    }
}