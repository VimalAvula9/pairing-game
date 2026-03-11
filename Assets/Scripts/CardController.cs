using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardController : MonoBehaviour
{
    [SerializeField] Card cardPrefab;
    [SerializeField] Transform gridTransform;
    [SerializeField] public Sprite[] iconsCollection;

    private List<Card> cards = new List<Card>();
    private Card firstSelected;
    private Card secondSelected;

    public Sprite GetIcon(int id)
    {
        return iconsCollection[id];
    }
    void LoadNewGame(int rows, int columns)
    {
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

        CreateCards(noOfIcons);
    }

    void LoadSavedGame(List<CardData> savedCards)
    {
        for (int i = 0; i < savedCards.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform);
            card.Initialize(this, 0, savedCards[i].spriteId, savedCards[i].isRevealed);
            cards.Add(card);
        }
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
            card.Initialize(this, 0, spritePairs[i], false);
            cards.Add(card);
        }
    }

    public void CardSelected(Card card)
    {

        card.Show();

        if (firstSelected == null)
        {
            firstSelected = card;
            return;
        }

        if (secondSelected == null)
        {
            secondSelected = card;
            StartCoroutine(CheckMatching(firstSelected, secondSelected));
            firstSelected = null;
            secondSelected = null;
        }
    }

    IEnumerator CheckMatching(Card a, Card b)
    {
        yield return new WaitForSeconds(0.3f);

        if (a.GetSpriteId() != b.GetSpriteId())
        {
            a.Hide();
            b.Hide();
        }
    }

    void Start()
    {
        LoadNewGame(2, 3);
        // LoadSavedGame(cards);
    }
}
