using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image icon;

    private int defaultSpriteId;
    private int spriteId;
    private bool isRevealed;
    private CardController cardController;

    public void Initialize(CardController cardController, int defaultSpriteId, int spriteId, bool isRevealed)
    {
        this.cardController = cardController;
        this.defaultSpriteId = defaultSpriteId;
        this.spriteId = spriteId;
        this.isRevealed = isRevealed;

        if (!isRevealed)
        {
            icon.sprite = cardController.iconsCollection[defaultSpriteId];
        }
        else
        {
            icon.sprite = cardController.iconsCollection[spriteId];
        }
    }
    public void OnCardClicked()
    {
        if (!isRevealed)
            cardController.CardSelected(this);
    }

    public bool GetIsRevealed()
    {
        return isRevealed;
    }

    public int GetSpriteId()
    {
        return spriteId;
    }

    public void Show()
    {
        icon.sprite = cardController.iconsCollection[spriteId];
        isRevealed = true;
    }

    public void Hide()
    {
        icon.sprite = cardController.iconsCollection[defaultSpriteId];
        isRevealed = false;
    }
}
