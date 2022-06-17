using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI매니저
public class UIManager : MonoBehaviour
{
    public GameObject winSprite;
    public GameObject loseSprite;
    public GameObject blackjackWin;

    private bool gameResultTrigger = false;
    private static UIManager m_instance;
    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<UIManager>();
            return m_instance;
        }
    }

    // 플레이어 정보 업데이트 메서드
    // 플레이어 이름, 보유 현금 UI 업데이트
    public void UpdatePlayerInfo(Text refText, string playerID, long money)
    {
        // 널가드
        if (refText == null || playerID == null)
            return;

        refText.text = "ID: " + playerID + "\n" + "money: " + money;
        refText.gameObject.SetActive(true);
    }

    // 플레이어 배팅금액 업데이트 메서드
    // 배팅금액 UI 업데이트
    public void UpdatePlayerBet(Text refText, long betMoney)
    {
        // 널가드
        if (refText == null)
            return;

        refText.text = "Bet: " + betMoney;
        refText.gameObject.SetActive(true);
    }

    public void UpdatePlayerHands(Text refText, int score)
    {
        // 널가드
        if (refText == null)
            return;

        refText.text = "Hand: " + score;
        refText.gameObject.SetActive(true);
    }

    public void UpdateGameResult(int gameResult)
    {
        switch (gameResult)
        {
            case 1:
                winSprite.SetActive(true);
                break;

            case 2:
                loseSprite.SetActive(true);
                break;

            case 3:
                blackjackWin.SetActive(true);
                break;

            default:
                return;
        }
        gameResultTrigger = true;
    }

    void Update()
    {
        if (gameResultTrigger)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameResultTrigger = false;
                winSprite.SetActive(false);
                loseSprite.SetActive(false);
                blackjackWin.SetActive(false);
            }
        }
    }
}