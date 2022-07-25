using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    [SerializeField] private string ID;
    [SerializeField] private string NickName;
    [SerializeField] private long money;

    public string GetPlayerID() { return ID; }
    public string GetPlayerNickname() { return NickName; }
    public long GetPlayerMoney() { return money; }

    public static PlayerData CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerData>(jsonString);
    }
}
