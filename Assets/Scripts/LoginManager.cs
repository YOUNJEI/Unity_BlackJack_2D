using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("LoginPanel")]
    public InputField IdInputField;
    public InputField PasswordInputField;
    public Button LoginBTN;
    public Button CreateAccountBTN;
    [Header("CreateAccountPanel")]
    public InputField NewIdInputField;
    public InputField NewPasswordInputField;

    private string LoginUrl;

    void Start()
    {
        LoginBTN.onClick.AddListener(() => LoginBTNClicked());
        CreateAccountBTN.onClick.AddListener(() => CreateAccountBTNClicked());
    }

    public void LoginBTNClicked()
    {
        StartCoroutine(LoginCo());
    }

    IEnumerator LoginCo()
    {
        Debug.Log(IdInputField.text);
        Debug.Log(PasswordInputField.text);
        yield return null;
    }

    public void CreateAccountBTNClicked()
    {

    }
}