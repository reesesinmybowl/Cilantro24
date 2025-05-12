using UnityEngine;

public class CardDisplay : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    private CardData cardData;

    public void Setup(CardData data)
    {
        cardData = data;
        meshRenderer.material.mainTexture = cardData.cardTexture;
    }

    public CardData GetCardData() => cardData;
}