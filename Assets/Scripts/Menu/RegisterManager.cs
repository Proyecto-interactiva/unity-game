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
    [Header("Error Message Settings")]
    public TMP_Text errorLabel;
    public string errorMessage = "";
    private TMP_InputField usernameInputField;
    private TMP_InputField emailInputField;
    private TMP_InputField passwordInputField;
    private string uri = "/sign/up";

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        usernameInputField = username.GetComponent<TMP_InputField>();
        emailInputField = email.GetComponent<TMP_InputField>();
        passwordInputField = password.GetComponent<TMP_InputField>();

        errorLabel.SetText(""); // Empty error field at start
    }

    public void Back()
    {
        FindObjectOfType<AudioManager>().Play("Text");
        SceneManager.LoadScene("Menu");
    }

    // Nuevo registro y accedo a menú de código ("Play Menu")
    public void RegisterAndPlay()
    {
        WWWForm form = Register();
        StartCoroutine(gameManager.PostForm(uri, form, SuccessRegisterFallBackPLAY, ErrorRegisterFallBack));
    }

    // Nuevo registro y vuelvo a menú inicial (Menu)
    public void RegisterAndExit()
    {
        WWWForm form = Register();
        StartCoroutine(gameManager.PostForm(uri, form, SuccessRegisterFallBackEXIT, ErrorRegisterFallBack));
    }

    private WWWForm Register()
    {
        FindObjectOfType<AudioManager>().Play("Text");
        // do the registration
        WWWForm form = new WWWForm();
        form.AddField("name", usernameInputField.text);
        form.AddField("email", emailInputField.text);
        form.AddField("password", passwordInputField.text);
        Debug.Log("Executing Register post");
        return form;
    }

    private void SuccessRegisterFallBackPLAY()
    {
        FindObjectOfType<AudioManager>().Play("Open");
        SceneManager.LoadScene("Play Menu");
    }
    private void SuccessRegisterFallBackEXIT()
    {
        FindObjectOfType<AudioManager>().Play("Open");
        SceneManager.LoadScene("Menu");
    }

    private void ErrorRegisterFallBack()
    {
        FindObjectOfType<AudioManager>().Play("Close");
        usernameInputField.text = "";
        emailInputField.text = "";
        passwordInputField.text = "";

        // Error message
        errorLabel.SetText(errorMessage);
    }
}
