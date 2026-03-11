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
        isRevealed = true;
        StartCoroutine(Flip(cardController.GetIcon(spriteId)));
    }

    public void Hide()
    {
        if (!isRevealed) return;
        isRevealed = false;
        StartCoroutine(Flip(cardController.GetIcon(defaultSpriteId)));
    }

    IEnumerator Flip(Sprite newSprite)
    {
        float duration = 0.15f;
        float elapsed = 0f;

        Quaternion startRotation = transform.rotation;
        Quaternion midRotation = Quaternion.Euler(0, 90, 0);
        Quaternion endRotation = Quaternion.Euler(0, 0, 0);

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, midRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = midRotation;
        icon.sprite = newSprite;

        elapsed = 0f;

        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Lerp(midRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
    }
}
