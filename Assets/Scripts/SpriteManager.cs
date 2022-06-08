using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] sprites;
    public Card cardPrefab;

    public Sprite GetSprite(int idx)
    {
        if (idx < 0 && idx > sprites.Length - 1)
            return (null);
        return (sprites[idx]);
    }

    private void Start()
    {
        
        Card newCard = Instantiate<Card>(cardPrefab, transform.position, transform.rotation);
        newCard.SetCardNum(13, "Debug");
    }
}
