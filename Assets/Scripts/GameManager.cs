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

    private bool trigger;   // UI 비활성화용 변수

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

        trigger = false;
    }

    private void DealClicked()
    {
        StartCoroutine(Deal());
    }

    private IEnumerator Deal()
    {
        // 배팅금액이 0원 이하
        if (player.GetBetMoney() <= 0)
        {
            Debug.Log("플레이어 배팅금액이 0원 이하입니다.");
            yield break;
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
        BTNStand.interactable = false;
        BTNHit.interactable = false;

        // 새로운 게임 시작 전. 초기화 작업 진행
        player.GameInitPlayer();
        dealer.GameInitPlayer();

        // 게임 시작
        // 플레이어와 딜러에게 카드 2장씩 배분
        StartCoroutine(StartGame());
        yield return (new WaitForSeconds(3f));
        BTNStand.interactable = true;
        if (player.GetScore() < 21)
            BTNHit.interactable = true;
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
        BTNStand.interactable = false;
        BTNHit.interactable = false;
        StartCoroutine(CardOpen());
    }

    private void HitClicked()
    {
        StartCoroutine(Hit());
    }

    private IEnumerator Hit()
    {
        if (player.GetScore() >= 21)
            yield break;
        BTNHit.interactable = false;
        player.GetCard();
        yield return (new WaitForSeconds(0.7f));
        if (player.GetScore() < 21)
            BTNHit.interactable = true;
    }

    // 배팅
    private void BetClicked(int btnIndex)
    {
        player.Betting(btnIndex);
    }

    private IEnumerator CardOpen()
    {
        // 뒷면의 카드 공개 및 점수 UI 표시
        dealer.DealerCardOpen(1);
        UIManager.instance.UpdatePlayerHands(dealer.handText, dealer.GetScore());
        yield return (new WaitForSeconds(0.7f));

        // 딜러 추가 카드
        dealer.SetIsDealer(false);
        while (dealer.GetScore() < 17)
        {
            dealer.GetCard();
            yield return (new WaitForSeconds(0.7f));
        }
        dealer.SetIsDealer(true);
        yield return (new WaitForSeconds(1.5f));

        if (player.GetIsBurst() == true)
        {
            player.SetBetMoney(0);
            Debug.Log("플레이어 파산");
        }
        else if (dealer.GetIsBurst() == true)
        {
            player.SetMoney((long)(player.GetMoney() + player.GetBetMoney() * 2.5f));
            player.SetBetMoney(0);
            Debug.Log("딜러 파산");
        }
        else if (dealer.GetScore() > player.GetScore())
        {
            player.SetBetMoney(0);
            Debug.Log("플레이어 패배");
        }
        else if (dealer.GetScore() == player.GetScore())
        {
            player.SetMoney(player.GetMoney() + player.GetBetMoney());
            player.SetBetMoney(0);
            Debug.Log("무승부");
        }
        else
        {
            player.SetMoney((long)(player.GetMoney() + player.GetBetMoney() * 2.5f));
            player.SetBetMoney(0);
            Debug.Log("플레이어 승리");
        }
        player.CardReturn();
        dealer.CardReturn();
        BTNStand.gameObject.SetActive(false);
        BTNHit.gameObject.SetActive(false);
        BTNStand.interactable = true;
        BTNHit.interactable = true;
        

        BTNDeal.gameObject.SetActive(true);
        BTNReset.gameObject.SetActive(true);
        BTNDeal.interactable = false;
        BTNReset.interactable = false;
        for (int i = 0; i < BTNBet.Length; i++)
        {
            BTNBet[i].gameObject.SetActive(true);
            BTNBet[i].interactable = false;
        }
        Deck.instance.SetToCollect(player.GetCardCount() + dealer.GetCardCount());
        trigger = true;
        player.handText.gameObject.SetActive(false);
        UIManager.instance.UpdatePlayerInfo(player.playerInfoText, player.GetPlayerName(), player.GetMoney());
        dealer.handText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (trigger)
        {
            if (Deck.instance.GetToCollect() == 0)
            {
                BTNDeal.interactable = true;
                BTNReset.interactable = true;
                for (int i = 0; i < BTNBet.Length; i++)
                    BTNBet[i].interactable = true;
                trigger = false;
            }
        }
    }
}
