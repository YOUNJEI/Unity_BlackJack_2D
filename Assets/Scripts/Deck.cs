using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private static Deck m_instance;
    public static Deck instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<Deck>();
            return m_instance;
        }
    }

    public Card cardPrefab;

    private Card[] cards;
    private static string[] symbols = { "Heart", "Dia", "Clover", "Spade" };
    private const int cardCount = 53;
    private int curIdx = 0;
    private bool isCollecting;  // 덱으로 카드가 회수 진행중인지 확인할 변수

    public int GetCurIdx() { return (curIdx); }
    public void SetIsCollecting(bool boolean) { isCollecting = boolean; }
    public bool GetIsCollecting() { return (isCollecting); }

    // Start is called before the first frame update
    void Start()
    {
        m_instance = FindObjectOfType<Deck>();

        cards = new Card[cardCount];

        for (int sym = 0; sym < 4; sym++)
        {
            for (int i = 1; i <= 13; i++)
            {
                cards[(sym * 13) + (i - 1)] = Instantiate<Card>(cardPrefab, transform.position, transform.rotation);
                cards[(sym * 13) + (i - 1)].Setup(i, symbols[sym]);
                cards[(sym * 13) + (i - 1)].ChangeImageToBack();
            }
        }
    }

    public Card Deal()
    {
        // 덱 새로 생성
        if (curIdx >= cards.Length - 1)
        {
            // Shuffle
            for (int i = 0; i < cards.Length; i++)
                cards[i].gameObject.SetActive(true);
            curIdx = 0;
        }
        curIdx++;
        return (cards[curIdx - 1]);
    }

    public void UsedCardCollect(Card card)
    {
        card.ChangeImageToBack();
        Vector3 target = transform.position;
        card.Move(target);
    }
}