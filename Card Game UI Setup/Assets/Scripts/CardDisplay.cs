using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public void Setup(CardData data)
    {
        if (data.cardTexture == null)
        {
            Debug.LogWarning("Card texture is missing for: " + data.cardName);
            return;
        }

        // Create a new material with the URP Unlit shader
        Shader urpUnlitShader = Shader.Find("Universal Render Pipeline/Unlit");
        if (urpUnlitShader == null)
        {
            Debug.LogError("URP Unlit shader not found.");
            return;
        }

        Material newMaterial = new Material(urpUnlitShader);
        newMaterial.SetTexture("_BaseMap", data.cardTexture);
        meshRenderer.material = newMaterial;

        Debug.Log("Applied texture: " + data.cardTexture.name + " to card: " + data.cardName);
    }
}