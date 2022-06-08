using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ??????? ??? ????
// cardNum: ?? ??? ??
// ?? ????? ????? ??? ???? ??? ??
public class Card : MonoBehaviour
{
    private int cardNum;
    
    public int GetCardNum()
    {
        return (this.cardNum);
    }

    public void SetCardNum(int n, string symbol)
    {
        this.cardNum = n;

        // ????? ???? ???? ?? ?????? ???? ??
        // ??? ?? ???? ??
        switch (symbol)
        {
            case "Heart":
                n += 0;
                break;

            case "Dia":
                n += 0;
                break;

            case "Clover":
                n += 0;
                break;

            case "Spade":
                n += 0;
                break;
        }
        // ????? ?????? ?????? ??
        // ??? ?????? ??
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
