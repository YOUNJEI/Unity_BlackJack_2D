using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class DataManager : MonoBehaviour
{
    [Header("LoginPanel")]
    public InputField IdInputField;
    public InputField PasswordInputField;
    public Button LoginAccessBTN;
    public Button CreateAccountBTN;
    [Header("CreateAccountPanel")]
    public InputField NewIdInputField;
    public InputField NewPasswordInputField;
    public Button OKBTN;
    public Button CancelBTN;
    [Header("LobbyUIButton")]
    public Button LoginBTN;
    public Button LogoutBTN;
    public Button[] UIButtons;

    private GameObject LoginPanel;
    private GameObject CreateAccountPanel;

    private PlayerData playerdata;

    private static DataManager m_instance;
    public static DataManager instance
    {
        get
        {
            if (m_instance == null)
                m_instance = FindObjectOfType<DataManager>();
            return m_instance;
        }
    }

    void Start()
    {
        LoginBTN.onClick.AddListener(() => LoginBTNClicked());
        LoginAccessBTN.onClick.AddListener(() => LoginAccessBTNClicked());
        CreateAccountBTN.onClick.AddListener(() => CreateAccountBTNClicked());
        OKBTN.onClick.AddListener(() => OKBTNClicked());
        CancelBTN.onClick.AddListener(() => CancelBTNClicked());
        LogoutBTN.onClick.AddListener(() => LogoutBTNClicked());

        LoginPanel = GameObject.Find("Canvas").transform.Find("LoginPanel").gameObject;
        CreateAccountPanel = GameObject.Find("Canvas").transform.Find("CreateAccountPanel").gameObject;
    }

    public void LoginBTNClicked()
    {
        SetActiveUIBTNs(false);
        LoginPanel.SetActive(true);
    }

    public void LoginAccessBTNClicked()
    {
        string ID = IdInputField.text.ToString();
        string PASSWORD = PasswordInputField.text.ToString();

        if (IsValidatedID(ID) == LoginErrorCode.OK
            && IsValidatedPassword(PASSWORD) == LoginErrorCode.OK)
        {
            StartCoroutine(LoginAccess(ID, PASSWORD));
        }
    }

    public void CreateAccountBTNClicked()
    {
        LoginPanel.SetActive(false);
        CreateAccountPanel.SetActive(true);
    }

    public void OKBTNClicked()
    {
        string ID = NewIdInputField.text.ToString();
        string PASSWORD = NewPasswordInputField.text.ToString();

        if (IsValidatedID(ID) == LoginErrorCode.OK
            && IsValidatedPassword(PASSWORD) == LoginErrorCode.OK)
        {
            StartCoroutine(CreateAccount(ID, PASSWORD));
        }
    }

    public void CancelBTNClicked()
    {
        CreateAccountPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }

    public void LogoutBTNClicked()
    {
        playerdata = null;
        UIButtons[0].interactable = false;
        UIButtons[1].interactable = false;
        LogoutBTN.gameObject.SetActive(false);
        LoginBTN.gameObject.SetActive(true);
    }

    private void SetActiveUIBTNs(bool setBoolean)
    {
        LoginBTN.gameObject.SetActive(setBoolean);
        for (int i = 0; i < UIButtons.Length; i++)
        {
            UIButtons[i].gameObject.SetActive(setBoolean);
        }
    }

    private IEnumerator LoginAccess(string ID, string PASSWORD)
    {
        const string url = "https://kyj951211.cafe24.com/BJK/login.php";
        WWWForm form = new WWWForm();

        form.AddField("ID", ID);
        form.AddField("PASSWORD", PASSWORD);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                // JSON을 객체로 변환
                playerdata = PlayerData.CreateFromJSON(www.downloadHandler.text);

                // UI
                LoginPanel.SetActive(false);
                SetActiveUIBTNs(true);
                LoginBTN.gameObject.SetActive(false);
                LogoutBTN.transform.GetChild(0).gameObject.GetComponent<Text>().text
                    = "Welcome \"" + playerdata.GetPlayerNickname() + "\"\n"
                    + "Your money " + playerdata.GetPlayerMoney().ToString();
                LogoutBTN.gameObject.SetActive(true);
                UIButtons[0].interactable = true;
                UIButtons[1].interactable = true;
            }
        }
    }
    
    private IEnumerator CreateAccount(string ID, string PASSWORD)
    {
        const string url = "https://kyj951211.cafe24.com/BJK/create_account.php";
        WWWForm form = new WWWForm();

        form.AddField("ID", ID);
        form.AddField("PASSWORD", PASSWORD);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                Debug.Log(www.downloadHandler.text);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                CreateAccountPanel.SetActive(false);
                LoginPanel.SetActive(true);
            }
        }
    }

    private LoginErrorCode IsValidatedID(string ID)
    {
        if (ID.Length < 3)
            return LoginErrorCode.LENGTH_ERROR;
        return LoginErrorCode.OK;
    }

    private LoginErrorCode IsValidatedPassword(string password)
    {
        if (password.Length < 1)
            return LoginErrorCode.LENGTH_ERROR;
        return LoginErrorCode.OK;
    }
}