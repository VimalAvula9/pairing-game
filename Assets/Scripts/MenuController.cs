using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject boardSelectionPanel;
    [SerializeField] private GameObject gamePanel;

    [Header("Text")]
    [SerializeField] private TMP_Text turnsText;

    [Header("Game Controller")]
    [SerializeField] private CardController cardController;

    void Start()
    {
        startPanel.SetActive(true);
        boardSelectionPanel.SetActive(false);
        gamePanel.SetActive(false);
    }

    public void OnNewGameClicked()
    {
        startPanel.SetActive(false);
        boardSelectionPanel.SetActive(true);
    }

    public void OnLoadGameClicked()
    {
        startPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void StartBoard(int boardType)
    {
        boardSelectionPanel.SetActive(false);
        gamePanel.SetActive(true);

        switch (boardType)
        {
            case 0:
                cardController.LoadNewGame(2, 2);
                break;

            case 1:
                cardController.LoadNewGame(2, 3);
                break;

            case 2:
                cardController.LoadNewGame(4, 4);
                break;

            case 3:
                cardController.LoadNewGame(5, 6);
                break;
        }
    }

    public void BackToMainMenu()
    {
        gamePanel.SetActive(false);
        boardSelectionPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    public void UpdateTurns(int turns)
    {
        turnsText.text = "Turns: " + turns;
    }
}
