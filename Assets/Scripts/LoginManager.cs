using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("LoginPanel")]
    public Button LoginBTN;
    [Header("LoginPanel")]
    public InputField IdInputField;
    public InputField PasswordInputField;
    public Button LoginAccessBTN;
    public Button CreateAccountBTN;
    [Header("CreateAccountPanel")]
    public InputField NewIdInputField;
    public InputField NewPasswordInputField;
    [Header("LobbyUIButton")]
    public Button[] UIButtons;

    private string LoginUrl;

    void Start()
    {
        LoginBTN.onClick.AddListener(() => LoginBTNClicked());
        LoginAccessBTN.onClick.AddListener(() => LoginAccessBTNClicked());
        CreateAccountBTN.onClick.AddListener(() => CreateAccountBTNClicked());
    }

    public void LoginBTNClicked()
    {
        SetActiveUIBTNs(false);

        GameObject LoginPanel = GameObject.Find("Canvas").transform.Find("LoginPanel").gameObject;
        if (LoginPanel != null)
        {
            LoginPanel.SetActive(true);
        }
        else
        {
            SetActiveUIBTNs(true);
        }
    }

    public void LoginAccessBTNClicked()
    {
        StartCoroutine(LoginAccessCo());
    }

    IEnumerator LoginAccessCo()
    {
        Debug.Log(IdInputField.text);
        Debug.Log(PasswordInputField.text);
        yield return null;
    }

    public void CreateAccountBTNClicked()
    {

    }

    private void SetActiveUIBTNs(bool setBoolean)
    {
        for (int i = 0; i < UIButtons.Length; i++)
        {
            UIButtons[i].gameObject.SetActive(setBoolean);
        }
    }
}