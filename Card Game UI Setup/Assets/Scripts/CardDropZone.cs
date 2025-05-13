using UnityEngine;
using System.Collections.Generic;

public class CardDropZone : MonoBehaviour
{
    [Header("Pile + Deck Setup")]
    public CenterPile centerPile;
    public List<CardData> allCards; // Assign all 52 cards here

    void Start()
    {
        if (allCards.Count > 0 && centerPile != null)
        {
            CardData randomCard = allCards[Random.Range(0, allCards.Count)];
            centerPile.SpawnInitialCard(randomCard);
        }
        else
        {
            Debug.LogWarning("Missing cards or centerPile reference.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        CardDisplay display = other.GetComponent<CardDisplay>();
        if (display == null) return;

        CardData data = display.GetCardData();

        // Now ALWAYS accept the card (no validation)
        centerPile.PlayCard(data, other.gameObject);
    }
}