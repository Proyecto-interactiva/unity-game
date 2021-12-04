using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public void LogIn()
    {
        SceneManager.LoadScene("LogIn");
    }

    public void Register()
    {
        SceneManager.LoadScene("Register");
    }

}
