using UnityEngine;
using UnityEditor;
using System.IO;

public class CardDataGenerator : EditorWindow
{
    [MenuItem("Tools/Generate 52 CardData Assets")]
    public static void GenerateCards()
    {
        string assetPath = "Assets/Cards/";
        if (!Directory.Exists(assetPath))
            Directory.CreateDirectory(assetPath);

        string[] suits = { "Hearts", "Spades", "Diamonds", "Clubs" };
        string[] names = { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

        for (int s = 0; s < suits.Length; s++)
        {
            for (int v = 0; v < names.Length; v++)
            {
                CardData card = ScriptableObject.CreateInstance<CardData>();
                card.cardName = $"{names[v]} of {suits[s]}";
                card.cardValue = v + 1;
                card.suit = (Suit)s;

                string filename = $"{names[v]}Of{suits[s]}.asset";
                AssetDatabase.CreateAsset(card, assetPath + filename);
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("✔ 52 CardData assets generated in Assets/Cards/");
    }
}