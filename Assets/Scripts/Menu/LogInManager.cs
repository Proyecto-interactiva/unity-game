using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LogInManager : MonoBehaviour
{
    public GameObject email;
    public GameObject password;
    private TMP_InputField emailInputField;
    private TMP_InputField passwordInputField;
    GameManager gameManager;

    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        emailInputField = email.GetComponent<TMP_InputField>();
        passwordInputField = password.GetComponent<TMP_InputField>();
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LogIn()
    {
        // do the login
        if(gameManager.LogIn(emailInputField.text, passwordInputField.text))
        {
            SceneManager.LoadScene("Play Menu");
        }
        else
        {
            //Do something to alert that something is wrong
            emailInputField.text = "";
            passwordInputField.text = "";
        }
    }
}
