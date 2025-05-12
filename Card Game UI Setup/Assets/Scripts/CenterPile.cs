using UnityEngine;

public class CenterPile : MonoBehaviour
{
    public GameObject cardPrefab;
    public CardData currentCardData;
    private GameObject currentCardObject;
    private int pileCount = 0;

    public void SpawnInitialCard(CardData data)
    {
        if (currentCardObject != null)
        {
            Destroy(currentCardObject);
        }

        currentCardData = data;

        // 🧱 Fixed spawn position and rotation for top-down view
        Vector3 spawnPos = transform.position + new Vector3(0, 0.01f, 0); // fixed Y to avoid Z-fighting
        Quaternion spawnRot = Quaternion.Euler(0, 0, 0); // flat, face-up card

        currentCardObject = Instantiate(cardPrefab, spawnPos, spawnRot);
        currentCardObject.GetComponent<CardDisplay>().Setup(data);

        Debug.Log($"🃏 Spawned center card: {data.cardName} (Suit: {data.suit}, Value: {data.cardValue})");
    }

    public bool CanAcceptCard(CardData playedCard)
    {
        return playedCard.suit == currentCardData.suit || playedCard.cardValue == currentCardData.cardValue + 1;
    }
    
    public void PlayCard(CardData newCard, GameObject cardObject)
    {
        if (!CanAcceptCard(newCard)) return;

        currentCardData = newCard;

        float stackHeight = 0.02f + (pileCount * 0.02f);
        Vector3 pilePos = transform.position;
        cardObject.transform.position = new Vector3(pilePos.x, stackHeight, pilePos.z);
        cardObject.transform.rotation = Quaternion.Euler(90f, 180f, 0f);

        pileCount++;

        Debug.Log($"After drop: card position is {cardObject.transform.position}");
    }
}