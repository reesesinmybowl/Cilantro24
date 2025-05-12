using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(DeckManager))]
public class DeckAutoFiller : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DeckManager deck = (DeckManager)target;

        if (GUILayout.Button("Auto-Fill AllCards from Folder"))
        {
            string[] guids = AssetDatabase.FindAssets("t:CardData", new[] { "Assets/Cards" });
            deck.allCards.Clear();

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                CardData card = AssetDatabase.LoadAssetAtPath<CardData>(path);
                deck.allCards.Add(card);
            }

            EditorUtility.SetDirty(deck);
            Debug.Log($"✔ Auto-filled {deck.allCards.Count} cards.");
        }
    }
}