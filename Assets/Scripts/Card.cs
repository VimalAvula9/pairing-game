using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image icon;

    private int id;
    private int defaultSpriteId;
    private int spriteId;
    private bool isRevealed;
    private CardController cardController;

    public void Initialize(CardController cardController, int id, int defaultSpriteId, int spriteId, bool isRevealed)
    {
        this.cardController = cardController;
        this.id = id;
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
        cardController.CardSelected(this);
    }

    public int GetId()
    {
        return id;
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
        if (isRevealed) return;
        icon.sprite = cardController.GetIcon(spriteId);
        isRevealed = true;
    }

    public void Hide()
    {
        if (!isRevealed) return;
        icon.sprite = cardController.GetIcon(defaultSpriteId);
        isRevealed = false;
    }
}
