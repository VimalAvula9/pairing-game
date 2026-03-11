using System;

[Serializable]
public class CardData
{
    public int spriteId;
    public bool isRevealed;

    public CardData(int spriteId, bool isRevealed)
    {
        this.spriteId = spriteId;
        this.isRevealed = isRevealed;
    }
}
