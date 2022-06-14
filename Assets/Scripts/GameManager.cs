using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 버튼 프리셋
    public Button BTNDeal;
    public Button BTNReset;
    public Button BTNStand;
    public Button BTNHit;
    public Button[] BTNBet;

    public Player player;
    public Player dealer;

    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<GameManager>();
            return m_instance;
        }
    }

    // 버튼 리스너 등록
    void Start()
    {
        BTNDeal.onClick.AddListener(() => DealClicked());
        BTNReset.onClick.AddListener(() => ResetClicked());
        BTNStand.onClick.AddListener(() => StandClicked());
        BTNHit.onClick.AddListener(() => HitClicked());

        BTNBet[0].onClick.AddListener(() => BetClicked(0));
        BTNBet[1].onClick.AddListener(() => BetClicked(1));
        BTNBet[2].onClick.AddListener(() => BetClicked(2));
        BTNBet[3].onClick.AddListener(() => BetClicked(3));

    }

    private void DealClicked()
    {
        throw new NotImplementedException();
    }

    // 배팅 초기화
    private void ResetClicked()
    {
        player.ResetBetting();
    }

    private void StandClicked()
    {

    }

    private void HitClicked()
    {

    }

    // 배팅
    private void BetClicked(int btnIndex)
    {
        player.Betting(btnIndex);
    }
}
