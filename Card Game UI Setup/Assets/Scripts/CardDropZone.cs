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
        if (display != null)
        {
            CardData data = display.GetCardData();

            if (centerPile.CanAcceptCard(data))
            {
                centerPile.PlayCard(data);
                Destroy(other.gameObject); // Remove card from player's hand
            }
            else
            {
                Debug.Log("❌ Cannot play " + data.cardName + " on current pile.");
            }
        }
    }
}