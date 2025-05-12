using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CardDropZone))]
public class CardDropZoneAutoFiller : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CardDropZone dropZone = (CardDropZone)target;

        if (GUILayout.Button("Auto-fill all CardData assets"))
        {
            string[] guids = AssetDatabase.FindAssets("t:CardData");
            dropZone.allCards.Clear();

            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                CardData card = AssetDatabase.LoadAssetAtPath<CardData>(path);
                if (card != null)
                {
                    dropZone.allCards.Add(card);
                }
            }

            EditorUtility.SetDirty(dropZone);
            Debug.Log($"✅ Auto-filled {dropZone.allCards.Count} cards into CardDropZone.");
        }
    }
}