using UnityEngine;
using System.Collections.Generic;

public class PlayerHand : MonoBehaviour
{
    [Header("Card Setup")]
    public List<CardData> handCards;       // Drag 5 CardData assets here
    public GameObject cardPrefab;          // Your 3D card prefab

    [Header("Layout Settings")]
    public float spacing = 1.5f;           // Space between cards (X-axis)

    private List<GameObject> spawnedCards = new List<GameObject>();

    void Start()
    {
        DisplayHand();
    }

    public void DisplayHand()
    {
        ClearHand();

        // Center the cards around the hand position
        float startX = -((handCards.Count - 1) * spacing) / 2f;

        for (int i = 0; i < handCards.Count; i++)
        {
            Vector3 pos = transform.position + new Vector3(startX + i * spacing, 0, 0);
            Quaternion rot = Quaternion.Euler(0, 0, 0); // flat, face-up for top-down

            GameObject card = Instantiate(cardPrefab, pos, rot, transform);
            card.GetComponent<CardDisplay>().Setup(handCards[i]);

            spawnedCards.Add(card);
        }
    }

    public void ClearHand()
    {
        foreach (var card in spawnedCards)
        {
            Destroy(card);
        }
        spawnedCards.Clear();
    }
}