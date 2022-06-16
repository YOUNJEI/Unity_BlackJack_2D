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
        // 배팅금액이 0원 이하
        if (player.GetBetMoney() <= 0)
        {
            Debug.Log("플레이어 배팅금액이 0원 이하입니다.");
            return;
        }

        // 배팅 UI 비활성화
        {
            BTNDeal.gameObject.SetActive(false);
            BTNReset.gameObject.SetActive(false);
            for (int i = 0; i < BTNBet.Length; i++)
                BTNBet[i].gameObject.SetActive(false);
        }

        // 게임 UI (Stand버튼 & Hit버튼) 활성화
        BTNStand.gameObject.SetActive(true);
        BTNHit.gameObject.SetActive(true);

        // 버스트 체크 변수를 false로 초기화
        player.SetIsBurst(false);
        dealer.SetIsBurst(false);

        // 게임 시작
        // 플레이어와 딜러에게 카드 2장씩 배분
        StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        player.GetCard();
        yield return (new WaitForSeconds(0.7f));

        dealer.GetCard();
        yield return (new WaitForSeconds(0.7f));

        player.GetCard();
        yield return (new WaitForSeconds(0.7f));

        dealer.GetCard();
        yield return (new WaitForSeconds(0.7f));

        dealer.DealerCardOpen(0);
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
        StartCoroutine(PlayerHit());
    }

    private IEnumerator PlayerHit()
    {
        if (player.GetIsBurst() == true)
            yield break;
        player.GetCard();
        yield return (new WaitForSeconds(0.7f));
    }

    // 배팅
    private void BetClicked(int btnIndex)
    {
        player.Betting(btnIndex);
    }
}
