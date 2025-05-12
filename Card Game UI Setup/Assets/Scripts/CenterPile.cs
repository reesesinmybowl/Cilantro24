using UnityEngine;

public class CenterPile : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardData currentCardData;
    private GameObject currentCardObject;

    public void SpawnInitialCard(CardData data)
    {
        if (currentCardObject != null)
            Destroy(currentCardObject);

        currentCardData = data;

        currentCardObject = Instantiate(cardPrefab, transform.position, Quaternion.Euler(90, 0, 0));
        currentCardObject.GetComponent<CardDisplay>().Setup(data);
    }

    public bool CanAcceptCard(CardData playedCard)
    {
        // Example rule: must be same suit OR one value higher
        return playedCard.suit == currentCardData.suit || playedCard.cardValue == currentCardData.cardValue + 1;
    }

    public void PlayCard(CardData newCard)
    {
        if (!CanAcceptCard(newCard))
        {
            Debug.Log("❌ Invalid card played: " + newCard.cardName);
            return;
        }

        SpawnInitialCard(newCard); // replace with new top card
        Debug.Log("✅ Played: " + newCard.cardName);
    }
}

