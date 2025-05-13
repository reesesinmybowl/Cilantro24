using UnityEngine;
using System.Collections;

public class CardDisplay : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private CardData cardData;

    private Material frontMaterial;
    private Material backMaterial;

    private bool isFaceUp = false;
    private bool isFlipping = false;

    public Texture backTexture; // Drag your card back texture here in Inspector

    public void Setup(CardData data)
    {
        cardData = data;

        Shader urpUnlit = Shader.Find("Universal Render Pipeline/Unlit");

        // Front material with cardData texture
        frontMaterial = new Material(urpUnlit);
        frontMaterial.SetTexture("_BaseMap", cardData.cardTexture);

        // Back material
        backMaterial = new Material(urpUnlit);
        backMaterial.SetTexture("_BaseMap", backTexture);

        ShowBackImmediate();
    }

    public void ShowFrontImmediate()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material = frontMaterial;
            isFaceUp = true;
        }
    }

    public void ShowBackImmediate()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material = backMaterial;
            isFaceUp = false;
        }
    }

    public void FlipToFront()
    {
        if (!isFaceUp && !isFlipping)
        {
            StartCoroutine(FlipCard(true));
        }
    }

    public void FlipToBack()
    {
        if (isFaceUp && !isFlipping)
        {
            StartCoroutine(FlipCard(false));
        }
    }

    private IEnumerator FlipCard(bool toFront)
    {
        isFlipping = true;
        float duration = 0.5f;
        float time = 0f;

        float startY = transform.localEulerAngles.y;
        float targetY = startY + 180f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float currentY = Mathf.LerpAngle(startY, targetY, time / duration);

            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                currentY,
                transform.localEulerAngles.z
            );

            // Swap material around halfway point (90 degrees into flip)
            if (time >= duration / 2f && meshRenderer.material != (toFront ? frontMaterial : backMaterial))
            {
                meshRenderer.material = toFront ? frontMaterial : backMaterial;
                isFaceUp = toFront;
            }

            yield return null;
        }

        // Snap to final rotation
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            targetY,
            transform.localEulerAngles.z
        );

        // Optional: normalize rotation
        float normalizedY = Mathf.Repeat(transform.localEulerAngles.y, 360f);
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            normalizedY,
            transform.localEulerAngles.z
        );

        isFlipping = false;
    }

    public bool IsFaceUp()
    {
        return isFaceUp;
    }

    public CardData GetCardData()
    {
        return cardData;
    }
}
