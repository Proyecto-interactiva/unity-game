using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public void LogIn()
    {
        SceneManager.LoadScene("LogIn");
        FindObjectOfType<AudioManager>().Play("Text");
    }

    public void Register()
    {
        SceneManager.LoadScene("Register");
        FindObjectOfType<AudioManager>().Play("Text");
    }

}
