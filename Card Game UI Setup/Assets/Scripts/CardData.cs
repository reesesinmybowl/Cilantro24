using UnityEngine;

public enum Suit
{
    Hearts,
    Spades,
    Diamonds,
    Clubs
}

[CreateAssetMenu(fileName = "Card", menuName = "Card Game/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cardValue; // 1 = Ace, etc.
    public Texture2D cardTexture;
    public Suit suit;
}