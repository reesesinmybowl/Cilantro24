using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card Game/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cardValue; // 1 = Ace, etc.
    public Texture cardTexture;
}