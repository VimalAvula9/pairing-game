using System;
using System.Collections.Generic;

[Serializable]
public class GameData
{
    public int turns;
    public int firstSelectedCardId = -1;
    public int secondSelectedCardId = -1;
    public List<CardData> cardDatas;

    public GameData(int turns, int firstSelectedCardId, int secondSelectedCardId, List<CardData> cardDatas)
    {
        this.turns = turns;
        this.firstSelectedCardId = firstSelectedCardId;
        this.secondSelectedCardId = secondSelectedCardId;
        this.cardDatas = cardDatas;
    }
}
