using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
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
    public Button[] UIButtons;

    private GameObject LoginPanel;
    private GameObject CreateAccountPanel;

    private string LoginUrl;

    void Start()
    {
        LoginBTN.onClick.AddListener(() => LoginBTNClicked());
        LoginAccessBTN.onClick.AddListener(() => LoginAccessBTNClicked());
        CreateAccountBTN.onClick.AddListener(() => CreateAccountBTNClicked());
        OKBTN.onClick.AddListener(() => OKBTNClicked());
        CancelBTN.onClick.AddListener(() => CancelBTNClicked());

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
                LoginPanel.SetActive(false);
                SetActiveUIBTNs(true);
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