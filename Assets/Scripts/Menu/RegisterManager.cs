using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RegisterManager : MonoBehaviour
{
    public GameObject username;
    public GameObject email;
    public GameObject password;
    private TMP_InputField usernameInputField;
    private TMP_InputField emailInputField;
    private TMP_InputField passwordInputField;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        usernameInputField = username.GetComponent<TMP_InputField>();
        emailInputField = email.GetComponent<TMP_InputField>();
        passwordInputField = password.GetComponent<TMP_InputField>();
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Register()
    {
        // do the registration
        if (gameManager.Register(usernameInputField.text, emailInputField.text, passwordInputField.text))
        {
            SceneManager.LoadScene("Play Menu");
        }
        else
        {
            //Do something to alert that something is wrong
            usernameInputField.text = "";
            emailInputField.text = "";
            passwordInputField.text = "";
        }
    }
}
