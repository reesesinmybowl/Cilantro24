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
        Shuffle(handCards);  // Shuffle the cards when starting
        DisplayHand();
    }

    public void DisplayHand()
    {
        ClearHand();

        Vector3 stackPosition = transform.position; // bunken ligger præcis her

        for (int i = 0; i < handCards.Count; i++)
        {
            // Lille Y offset hvis du vil kunne ane flere kort, ellers sæt 0f
            Vector3 pos = stackPosition + new Vector3(0, i * 0.01f, 0);

            Quaternion rot = Quaternion.Euler(0, 0, 0); // kortene ligger fladt

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

    // Shuffle function using Fisher-Yates algorithm
    public void Shuffle(List<CardData> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            CardData value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}