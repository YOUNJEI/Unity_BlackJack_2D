using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    // 버튼 프리셋
    [Header("UI Buttons")]
    public Button BTNSinglePlay;
    public Button BTNMultiPlay;
    public Button BTNExitGame;

    [Header("MultiPlay")]
    public Text connectinInfoText;
    public Button BTNRetry;
    public Button BTNRetryCancel;
    public Button BTNEnter;
    private GameObject MultiPlayConnectionPanel;

    private string gameVersion = "1";

    private static LobbyManager m_instance;
    public static LobbyManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<LobbyManager>();
            return m_instance;
        }
    }

    // 버튼 리스너 등록
    void Start()
    {
        BTNSinglePlay.onClick.AddListener(() => SinglePlayClicked());
        BTNMultiPlay.onClick.AddListener(() => MultiPlayClicked());
        BTNExitGame.onClick.AddListener(() => ExitGameClicked());
        BTNRetry.onClick.AddListener(() => BTNRetryCancelClicked());
        BTNRetryCancel.onClick.AddListener(() => BTNRetryCancelClicked());
        BTNEnter.onClick.AddListener(() => BTNEnterClicked());

        MultiPlayConnectionPanel = GameObject.Find("Canvas").transform.Find("MultiPlayConnectionPanel").gameObject;
    }

    private void SinglePlayClicked()
    {
        SceneManager.LoadScene("Table");
    }

    private void MultiPlayClicked()
    {
        MultiPlayConnectionPanel.SetActive(true);
        connectinInfoText.text = "Requeset server connection...";
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        connectinInfoText.text = "OK";
        BTNEnter.gameObject.SetActive(true);
        BTNEnter.interactable = true;
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        BTNRetry.gameObject.SetActive(true);
        BTNRetryCancel.gameObject.SetActive(true);
        connectinInfoText.text = "Connection is failed, Retry ?";
    }

    private void BTNRetryClicked()
    {
        connectinInfoText.text = "Requeset server connection...";
        PhotonNetwork.ConnectUsingSettings();
    }

    private void BTNRetryCancelClicked()
    {
        BTNRetry.gameObject.SetActive(false);
        BTNRetryCancel.gameObject.SetActive(false);
        MultiPlayConnectionPanel.SetActive(false);
    }

    private void BTNEnterClicked()
    {
        BTNEnter.interactable = false;

        if (PhotonNetwork.IsConnected)
        {
            connectinInfoText.text = "Enter the room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            BTNEnter.gameObject.SetActive(false);
            BTNRetry.gameObject.SetActive(true);
            BTNRetryCancel.gameObject.SetActive(true);
            connectinInfoText.text = "Connection is failed, Retry ?";
        }
    }

    private void ExitGameClicked()
    {
        Application.Quit();
    }
}
