using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // UI 프리셋
    public Text handText;
    public Text playerInfoText;

    public bool isDealer = false;
    private string playerName;
    private List<int> playerScore;
    private List<Card> playerCards;
    private long money;
    private long betMoney;

    // 플레이어 객체 생성시
    // 플레이어 이름, 보유 현금, 배팅 금액 초기화
    // UI 업데이트
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
        playerCards[playerCards.Count - 1].Move(transform.GetChild(playerCards.Count - 1).transform);
        if (!isDealer)
            playerCards[playerCards.Count - 1].ChangeImageToOrigin();
        playerScore[0] += playerCards[playerCards.Count - 1].GetCardNum();

        if (!isDealer)
            UIManager.instance.UpdatePlayerHands(handText, playerScore[0]);
    }

    public void Hit()
    {
        GetCard();
    }

    public int GetScore()
    {
        return (playerScore[0]);
    }

    // 배팅
    public bool Betting(int betIndex)
    {
        long toBet;

        // 버튼에 따라 배팅금액 설정
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

        // UI 업데이트
        UIManager.instance.UpdatePlayerBet(handText, betMoney);
        UIManager.instance.UpdatePlayerInfo(playerInfoText, playerName, money);
        return (true);
    }

    // 배팅 초기화
    public void ResetBetting()
    {
        money += betMoney;
        betMoney = 0;

        // UI 업데이트
        UIManager.instance.UpdatePlayerBet(handText, betMoney);
        UIManager.instance.UpdatePlayerInfo(playerInfoText, playerName, money);
    }

    public long GetBetMoney()
    {
        return (betMoney);
    }

    public void DealerCardOpen(int cardIdx)
    {
        if (cardIdx < 0 || cardIdx > playerCards.Count - 1)
            return;
        playerCards[cardIdx].ChangeImageToOrigin();
    }
}