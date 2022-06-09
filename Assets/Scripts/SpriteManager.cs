using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite[] sprites;

    public Sprite GetSprite(int idx)
    {
        if (idx < 0 && idx > sprites.Length - 1)
            return (null);
        return (sprites[idx]);
    }
}
