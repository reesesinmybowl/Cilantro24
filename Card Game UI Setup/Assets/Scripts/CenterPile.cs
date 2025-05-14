using System;
using UnityEngine;

public class CenterPile : MonoBehaviour
{
    public GameObject cardPrefab;
    public GameObject objectToSpawn;
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

        if (pileCount > 0)
        {
            // Suit check
            switch (currentCardData.suit)
            {
                case Suit.Hearts:
                    Debug.Log("♥️ Hearts");
                    break;
                case Suit.Spades:
                    Debug.Log("♠️ Spades");
                    break;
                case Suit.Diamonds:
                    Debug.Log("♦️ Diamonds");
                    break;
                case Suit.Clubs:
                    Debug.Log("♣️ Clubs");
                    break;
            }

            int value = currentCardData.cardValue;

            switch (value)
            {
                case 1:
                    Debug.Log("Rule for Ace");
                    if (objectToSpawn != null)
                    {
                        Vector3 spawnPos = transform.position + new Vector3(0, 1f, 0);
                        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
                    }
                    break;
                case 7:
                    Debug.Log("🎯 7 played — spawning object");
                    if (objectToSpawn != null)
                    {
                        Vector3 spawnPos = transform.position + new Vector3(0, 1f, 0);
                        Instantiate(objectToSpawn, spawnPos, Quaternion.identity);
                    }
                    break;
                default:
                    Debug.Log($"Number card: {value}");
                    break;
            }

            if (currentCardData.suit == Suit.Hearts && currentCardData.cardValue == 1)
            {
                Debug.Log("🔥 This is the Ace of Hearts!");
            }
        }

        // Card always gets added visually, no matter pileCount
        cardObject.transform.SetParent(transform);

        MeshRenderer renderer = cardObject.GetComponent<MeshRenderer>();
        if (renderer != null)
        {
            renderer.sortingOrder = 1 + transform.childCount;
        }

        pileCount++;

        Debug.Log($"Card added to pile via physics: {newCard.cardName}");
    }

}