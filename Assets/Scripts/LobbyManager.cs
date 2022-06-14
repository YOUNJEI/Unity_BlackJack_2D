using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    // 버튼 프리셋
    public Button BTNSinglePlay;
    public Button BTNMultiPlay;
    public Button BTNExitGame;

    // 버튼 리스너 등록
    void Start()
    {
        BTNSinglePlay.onClick.AddListener(() => SinglePlayClicked());
        BTNMultiPlay.onClick.AddListener(() => MultiPlayClicked());
        BTNExitGame.onClick.AddListener(() => ExitGameClicked());
    }

    private void SinglePlayClicked()
    {
        SceneManager.LoadScene("Table");
    }

    private void MultiPlayClicked()
    {

    }

    private void ExitGameClicked()
    {
        Application.Quit();
    }
}
