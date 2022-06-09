using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Card cardPrefab;

    private Card[] cards;
    private static string[] symbols = { "Heart", "Dia", "Clover", "Spade" };
    private const int cardCount = 53;
    // Start is called before the first frame update
    void Start()
    {
        cards = new Card[cardCount];

        for (int sym = 0; sym < 4; sym++)
        {
            for (int i = 1; i <= 13; i++)
            {
                cards[(sym * 13) + (i - 1)] = Instantiate<Card>(cardPrefab, transform.position, transform.rotation);
                cards[(sym * 13) + (i - 1)].Setup(i, symbols[sym]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
