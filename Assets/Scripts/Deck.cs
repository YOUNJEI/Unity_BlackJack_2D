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
    private const int cardCount = 52;
    private int curIdx = 0;
    private int toCollect;  // 게임 종료 후 회수 해야하는 카드 수

    public int GetCurIdx() { return (curIdx); }
    public void SetToCollect(int n) { toCollect = n; }
    public int GetToCollect() { return (toCollect); }

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
        toCollect = 0;
        Shuffle();
    }

    private void Shuffle()
    {
        System.Random random = new System.Random();

        for (int i = 0; i < cards.Length; i++)
        {
            int j = random.Next(i, cards.Length);

            Card temp = cards[i];
            cards[i] = cards[j];
            cards[j] = temp;
        }
    }

    public Card Deal()
    {
        // 덱 새로 생성
        if (curIdx >= cards.Length)
        {
            // Shuffle
            for (int i = 0; i < cards.Length; i++)
                cards[i].gameObject.SetActive(true);
            Shuffle();
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