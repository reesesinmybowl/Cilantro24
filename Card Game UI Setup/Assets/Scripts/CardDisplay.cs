using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private CardData cardData;

    public void Setup(CardData data)
    {
        cardData = data;

        Shader urpUnlit = Shader.Find("Universal Render Pipeline/Unlit");
        Material mat = new Material(urpUnlit);
        mat.SetTexture("_BaseMap", cardData.cardTexture);
        meshRenderer.material = mat;
    }

    public CardData GetCardData()
    {
        return cardData;
    }
}