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

        // Spawn at pile position (physics will handle rest)
        Vector3 spawnPos = transform.position + Vector3.up * 0.1f;
        Quaternion spawnRot = Quaternion.Euler(0, 0, 0);

        currentCardObject = Instantiate(cardPrefab, spawnPos, spawnRot);
        currentCardObject.GetComponent<CardDisplay>().Setup(data);

        // Optional physics setup
        Rigidbody rb = currentCardObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }

        Debug.Log($"🃏 Spawned center card: {data.cardName} (Suit: {data.suit}, Value: {data.cardValue})");
    }

    public void PlayCard(CardData newCard, GameObject cardObject)
    {
        currentCardData = newCard;

        // Let physics handle position — don't force it
        cardObject.transform.SetParent(transform);

        // Optional: raise render layer slightly so new card shows clearly
        MeshRenderer renderer = cardObject.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.sortingOrder = 1 + transform.childCount;
        }

        pileCount++;

        Debug.Log($"Card added to pile via physics: {newCard.cardName}");
    }
}