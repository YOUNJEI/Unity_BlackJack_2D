using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UI매니저
public class UIManager : MonoBehaviour
{
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
        refText.text = "ID: " + playerID + "\n" + "money: " + money;
        refText.gameObject.SetActive(true);
    }

    // 플레이어 배팅금액 업데이트 메서드
    // 배팅금액 UI 업데이트
    public void UpdatePlayerBet(Text refText, long betMoney)
    {
        refText.text = "Bet: " + betMoney;
        refText.gameObject.SetActive(true);
    }
}
