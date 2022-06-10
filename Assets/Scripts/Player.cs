using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string playerName;
    private List<int> playerScore;
    private List<Card> playerCards;

    void Start()
    {
        playerScore = new List<int>();
        playerScore.Add(0);

        playerCards = new List<Card>();
    }

    public void GetCard()
    {
        playerCards.Add(Deck.m_instance.Deal());
        playerScore[0] += playerCards[playerCards.Count - 1].GetCardNum();
    }

    public void Hit()
    {
        GetCard();
    }

    public int GetScore()
    {
        return (playerScore[0]);
    }
}