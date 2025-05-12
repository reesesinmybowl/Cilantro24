using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<CardData> allCards;
    public GameObject cardPrefab;
    public Transform spawnOrigin;

    void Start()
    {
        ShuffleAndSpawn();
    }

    void ShuffleAndSpawn()
    {
        for (int i = 0; i < allCards.Count; i++)
        {
            Vector3 spawnPos = spawnOrigin.position + new Vector3(i * 1.5f, 0, 0);
            GameObject card = Instantiate(cardPrefab, spawnPos, Quaternion.identity);
            card.GetComponent<CardDisplay>().Setup(allCards[i]);
        }
    }
}