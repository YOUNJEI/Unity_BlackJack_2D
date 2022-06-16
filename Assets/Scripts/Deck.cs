using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card cardPrefab;
    public static Deck m_instance;

    private Card[] cards;
    private static string[] symbols = { "Heart", "Dia", "Clover", "Spade" };
    private const int cardCount = 53;
    private int curIdx = 0;

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
        if (curIdx >= cards.Length)
        {
            
        }
        curIdx++;
        return (cards[curIdx - 1]);
    }
}