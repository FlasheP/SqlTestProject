using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public InputField RegisterUsernameInput;
    public InputField RegisterPasswordInput;
    public InputField RegisterPasswordConfirmInput;

    public Button LoginBtn;
    public Button RegisterBtn;
    public Button RegisterCancelBtn;
    public Button RegisterSubmitBtn;

    public GameObject LoginMenu;
    public GameObject RegisterMenu;
    public GameObject Tabs;

    void Start()
    {
        LoginBtn.onClick.AddListener(() => StartCoroutine(StartLogin()));
        RegisterSubmitBtn.onClick.AddListener(() => OnClickRegisterSubmitBtn());
        RegisterBtn.onClick.AddListener(() => RegisterMenu.SetActive(true));
        RegisterCancelBtn.onClick.AddListener(() => OnClickRegisterCancel());
    }
    public IEnumerator StartLogin()
    {
        yield return StartCoroutine(GameManager.Instance.web.Login(UsernameInput.text, PasswordInput.text));
        if (GameManager.Instance.playerInfo.PlayerID != "0")
        {
            LoginMenu.SetActive(false);
            Tabs.SetActive(true);
        }
    }
    public void OnClickRegisterSubmitBtn()
    {
        string errorMessage = "";
        bool isValid()
        {
            if (RegisterPasswordInput.text != RegisterPasswordConfirmInput.text)
            {
                errorMessage = "비밀번호의 입력이 잘못되었습니다. 다시 입력해주세요";
                return false;
            }
            else if (RegisterUsernameInput.text == "" || RegisterPasswordInput.text == "" || RegisterPasswordConfirmInput.text == "")
            {
                errorMessage = "입력하지 않은 항목이 있습니다. 다시 입력해주세요";
                return false;
            }
            else
                return true;
        }
        if (!isValid())
        {
            Debug.Log(errorMessage);
            return;
        }
        else
        {
            StartCoroutine(GameManager.Instance.web.RegisterUser(RegisterUsernameInput.text, RegisterPasswordConfirmInput.text));
            OnClickRegisterCancel();
        }
    }
    public void OnClickRegisterCancel()
    {
        RegisterUsernameInput.text = "";
        RegisterPasswordInput.text = "";
        RegisterPasswordConfirmInput.text = "";
        RegisterMenu.SetActive(false);
    }
}
