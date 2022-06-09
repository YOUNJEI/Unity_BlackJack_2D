using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ??? ??
// cardNum: ??? ??
// cardSymbol: ??? ??
public class Card : MonoBehaviour
{
    private int cardNum;
    private string cardSymbol;
    
    public int GetCardNum()
    {
        return (this.cardNum);
    }

    public string GetCardSymbol()
    {
        return (this.cardSymbol);
    }

    private void SetCardNum(int n)
    {
        this.cardNum = n;
    }

    private void SetCardSymbol(string symbol)
    {
        this.cardSymbol = symbol;
    }

    public void Setup(int n, string symbol)
    {
        SetCardNum(n);
        SetCardSymbol(symbol);

        // ????? ???? ?????? ???? ???? ??
        // ? ???? ??? ?? ??
        switch (symbol)
        {
            case "Heart":
                n += 0;
                break;

            case "Dia":
                n += 13;
                break;

            case "Clover":
                n += 26;
                break;

            case "Spade":
                n += 39;
                break;
        }
        // ?? ??? ????????? ???? ?? ??? ??
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite =
            GameObject.Find("SpriteManager").GetComponent<SpriteManager>().GetSprite(n);
    }

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
