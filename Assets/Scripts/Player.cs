using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Text handText;
    public Text playerInfoText;

    private string playerName;
    private List<int> playerScore;
    private List<Card> playerCards;
    private long money;
    private long betMoney;

    void Start()
    {
        playerName = "void";
        playerScore = new List<int>();
        playerScore.Add(0);

        playerCards = new List<Card>();
        
        money = 1000000;
        betMoney = 0;
        handText.gameObject.SetActive(false);
        UIManager.instance.UpdatePlayerInfo(playerInfoText, playerName, money);
    }

    public void GetCard()
    {
        playerCards.Add(Deck.m_instance.Deal());
        playerScore[0] += playerCards[playerCards.Count - 1].GetCardNum();
    }

    public void Hit()
    {
        GetCard();
    }

    public int GetScore()
    {
        return (playerScore[0]);
    }

    public bool Betting(int betIndex)
    {
        long toBet;

        switch (betIndex)
        {
            case 0:
                toBet = 1000;
                break;

            case 1:
                toBet = 10000;
                break;

            case 2:
                toBet = 100000;
                break;

            case 3:
                toBet = 1000000;
                break;

            default:
                toBet = 0;
                break;
        }

        if (money < toBet)
            return (false);
        money -= toBet;
        betMoney += toBet;
        UIManager.instance.UpdatePlayerBet(handText, betMoney);
        UIManager.instance.UpdatePlayerInfo(playerInfoText, playerName, money);
        return (true);
    }

    public void ResetBetting()
    {
        money += betMoney;
        betMoney = 0;
        UIManager.instance.UpdatePlayerBet(handText, betMoney);
        UIManager.instance.UpdatePlayerInfo(playerInfoText, playerName, money);
    }
}