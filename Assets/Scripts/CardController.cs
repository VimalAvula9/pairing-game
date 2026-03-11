using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] public Sprite[] iconsCollection;

    [Header("Menu Controller")]
    [SerializeField] private MenuController menuController;

    private List<Card> cards = new List<Card>();
    private int firstSelectedCardId;
    private int secondSelectedCardId;
    private int turns;

    public Sprite GetIcon(int id)
    {
        return iconsCollection[id];
    }
    public void LoadNewGame(int rows, int columns)
    {
        ClearBoard();
        int totalCards = rows * columns;

        if (totalCards % 2 != 0)
        {
            Debug.LogError("Total cards must be even.");
            return;
        }

        int noOfIcons = totalCards / 2;

        if (iconsCollection.Length - 1 < noOfIcons)
        {
            Debug.LogError("Not enough sprites assigned. Index 0 should be hidden card sprite.");
            return;
        }

        this.turns = 0;
        menuController.UpdateTurns(0);
        CreateCards(noOfIcons);
    }

    public void LoadSavedGame()
    {
        if (!PlayerPrefs.HasKey("save"))
        {
            Debug.Log("No saved game found");
            return;
        }
        ClearBoard();
        string json = PlayerPrefs.GetString("save");

        GameData gameData = JsonUtility.FromJson<GameData>(json);

        this.turns = gameData.turns;
        menuController.UpdateTurns(this.turns);
        this.firstSelectedCardId = gameData.firstSelectedCardId;
        this.secondSelectedCardId = gameData.secondSelectedCardId;

        for (int i = 0; i < gameData.cardDatas.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform);
            card.Initialize(this, i, 0, gameData.cardDatas[i].spriteId, gameData.cardDatas[i].isRevealed);
            cards.Add(card);
        }
    }

    public void SaveGame()
    {
        List<CardData> cardDatas = new List<CardData>();

        foreach (Card card in cards)
        {
            cardDatas.Add(new CardData(card.GetSpriteId(), card.GetIsRevealed()));
        }

        GameData gameData = new GameData(turns, firstSelectedCardId, secondSelectedCardId, cardDatas);

        string json = JsonUtility.ToJson(gameData);

        PlayerPrefs.SetString("save", json);
        PlayerPrefs.Save();
    }

    void ClearBoard()
    {
        foreach (Card card in cards)
        {
            Destroy(card.gameObject);
        }

        cards.Clear();

        firstSelectedCardId = -1;
        secondSelectedCardId = -1;
    }

    void ShuffleSprites(List<int> spritePairs)
    {
        for (int i = spritePairs.Count - 1; i >= 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = spritePairs[randomIndex];
            spritePairs[randomIndex] = spritePairs[i];
            spritePairs[i] = temp;
        }
    }

    private List<int> PrepareSprites(int noOfIcons)
    {
        List<int> spritePairs = new();
        for (int i = 1; i <= noOfIcons; i++)
        {
            spritePairs.Add(i);
            spritePairs.Add(i);
        }
        ShuffleSprites(spritePairs);
        return spritePairs;
    }

    void CreateCards(int noOfIcons)
    {
        List<int> spritePairs = PrepareSprites(noOfIcons);
        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform);
            card.Initialize(this, i, 0, spritePairs[i], false);
            cards.Add(card);
        }
    }

    public void CardSelected(Card card)
    {
        if (card.GetIsRevealed() || card.GetId() == firstSelectedCardId) return;

        card.Show();
        if (firstSelectedCardId == -1)
        {
            firstSelectedCardId = card.GetId();
            SaveGame();
        }
        else if (secondSelectedCardId == -1)
        {
            turns++;
            menuController.UpdateTurns(turns);
            secondSelectedCardId = card.GetId();
            StartCoroutine(CheckMatching(firstSelectedCardId, secondSelectedCardId));
            firstSelectedCardId = -1;
            secondSelectedCardId = -1;
        }
    }

    IEnumerator CheckMatching(int cardA, int cardB)
    {
        yield return new WaitForSeconds(0.3f);

        if (cards[cardA].GetSpriteId() != cards[cardB].GetSpriteId())
        {
            cards[cardA].Hide();
            cards[cardB].Hide();
            SaveGame();
        }
        else
        {
            SaveGame();
            CheckGameOver();
        }
        
    }

    void CheckGameOver()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            if (!cards[i].GetIsRevealed()) return;
        }
        ClearBoard();
        PlayerPrefs.DeleteKey("save");
        PlayerPrefs.Save();
        menuController.GameOver(turns);
    }
}
