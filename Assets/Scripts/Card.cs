using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카드를 표현
// cardNum: 카드의 숫자
// cardSymbol: 카드의 문양
public class Card : MonoBehaviour
{
    private int cardNum;
    private string cardSymbol;

    // 움직이는 애니메이션 구현하기 위한 필드
    public float moveSpeed = 30f;
    private bool toMove;
    private Vector3 target;

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

        // 스프라이트 매니저에 등록되어있는 이미지를 불러오기 위해
        // 각 문양별로 인덱스 조정 작업
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
        // 자식 객체의 스프라이트렌더러를 변경하여 카드 이미지 표현
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite =
            GameObject.Find("SpriteManager").GetComponent<SpriteManager>().GetSprite(n);
    }

    public void ChangeImageToBack()
    {
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite =
            GameObject.Find("SpriteManager").GetComponent<SpriteManager>().GetSprite(0);
    }

    public void ChangeImageToOrigin()
    {
        int n = this.cardNum;

        switch (this.cardSymbol)
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
        // 자식 객체의 스프라이트렌더러를 변경하여 카드 이미지 표현
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite =
            GameObject.Find("SpriteManager").GetComponent<SpriteManager>().GetSprite(n);
    }

    public void Move(Vector3 targetInfo)
    {
        toMove = true;
        target = targetInfo;
    }

    void Start()
    {
        toMove = false;
    }

    void Update()
    {
        if (toMove)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            if (transform.position == target)
                toMove = false;
        }
    }
}
