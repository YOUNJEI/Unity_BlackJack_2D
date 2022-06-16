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
    private int playerScore;
    private List<Card> playerCards;
    private long money;
    private long betMoney;
    private int sortLayerIdx;
    private bool isBurst;
    private int hasAce;

    public void SetIsBurst(bool boolean)
    {
        isBurst = boolean;
    }

    public bool GetIsBurst()
    {
        return (isBurst);
    }

    public void GameInitPlayer()
    {
        playerScore = 0;
        playerCards.Clear();
        sortLayerIdx = 1;
        isBurst = false;
        hasAce = 0;
    }

    // 플레이어 객체 생성시
    // 플레이어 이름, 보유 현금, 배팅 금액 초기화
    // UI 업데이트
    void Start()
    {
        playerName = "void";
        playerScore = 0;

        playerCards = new List<Card>();
        
        money = 1000000;
        betMoney = 0;
        sortLayerIdx = 1;
        isBurst = false;
        hasAce = 0;
        handText.gameObject.SetActive(false);
        UIManager.instance.UpdatePlayerInfo(playerInfoText, playerName, money);
    }

    public void GetCard()
    {
        playerCards.Add(Deck.m_instance.Deal());

        // 스프라이트가 겹쳐져도 이미지가 잘 반영될수 있게
        // 각 스프라이트의 sortingOrder 를 조정
        SpriteRenderer spriteRenderer = playerCards[playerCards.Count - 1].GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = sortLayerIdx++;
            spriteRenderer = null;
        }
        spriteRenderer = playerCards[playerCards.Count - 1].transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = sortLayerIdx++;
            spriteRenderer = null;
        }
        // 카드 이동효과
        Vector3 target = transform.GetChild(0).position     // 카드가 놓일 기본 위치
            + new Vector3(0.5f * (playerCards.Count - 1), 0, -0.5f * (playerCards.Count - 1));
        playerCards[playerCards.Count - 1].Move(target);
        
        if (!isDealer)
            playerCards[playerCards.Count - 1].ChangeImageToOrigin();

        int toAddScore;
        switch (playerCards[playerCards.Count - 1].GetCardNum())
        {
            // J, Q, K
            case 11:
            case 12:
            case 13:
                toAddScore = 10;
                break;

            // ACE
            case 1:
                toAddScore = 11;
                hasAce++;
                break;

            default:
                toAddScore = playerCards[playerCards.Count - 1].GetCardNum();
                break;
        }
        playerScore += toAddScore;
        if (!isDealer)
            UIManager.instance.UpdatePlayerHands(handText, playerScore);

        if (playerScore > 21)
        {
            if (hasAce > 0)
            {
                while (hasAce > 0 && playerScore > 21)
                {
                    playerScore -= 10;
                    hasAce--;
                }
                if (playerScore <= 21)
                {
                    UIManager.instance.UpdatePlayerHands(handText, playerScore);
                    return;
                }
            }
            // Action() 파산에 대한
            isBurst = true;
        }
    }

    public void Hit()
    {
        GetCard();
    }

    public int GetScore()
    {
        return (playerScore);
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