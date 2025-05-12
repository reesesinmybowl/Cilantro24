using UnityEngine;
using System.Collections.Generic;

public class DeckManager : MonoBehaviour
{
    public List<CardData> allCards; // ← this is the field you're looking for
    public GameObject cardPrefab;
    public Transform spawnOrigin;

    void Start()
    {
        ShowCardsOnTable();
    }

    void ShowCardsOnTable()
    {
        for (int i = 0; i < allCards.Count; i++)
        {
            Vector3 pos = spawnOrigin.position + new Vector3(i % 13 * 1.2f, 0, -Mathf.Floor(i / 13) * 2f);
            GameObject card = Instantiate(cardPrefab, pos, Quaternion.Euler(90, 0, 0));
            card.GetComponent<CardDisplay>().Setup(allCards[i]);
        }
    }
}