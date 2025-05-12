using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<CardData> allCards;
    public GameObject cardPrefab;
    public Transform spawnPoint;

    private GameObject currentCard;
    private int currentIndex = 0;

    void Start()
    {
        if (allCards.Count > 0)
        {
            SpawnCard(allCards[currentIndex]);
        }
        else
        {
            Debug.LogWarning("No cards available in the deck.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentIndex = (currentIndex + 1) % allCards.Count;
            SpawnCard(allCards[currentIndex]);
        }
    }

    void SpawnCard(CardData data)
    {
        if (currentCard != null)
        {
            Destroy(currentCard);
        }

        currentCard = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);
        CardDisplay display = currentCard.GetComponent<CardDisplay>();
        if (display != null)
        {
            display.Setup(data);
        }
        else
        {
            Debug.LogError("CardDisplay component not found on the card prefab.");
        }
    }
}