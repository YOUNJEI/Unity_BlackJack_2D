using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 카드 스프라이트 관리하기 위한 스프라이트 매니저
public class SpriteManager : MonoBehaviour
{
    // 카드 스프라이트 저장
    public Sprite[] sprites;

    public Sprite GetSprite(int idx)
    {
        if (idx < 0 && idx > sprites.Length - 1)
            return (null);
        return (sprites[idx]);
    }
}
