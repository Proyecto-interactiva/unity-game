using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private ApiConnection api = new ApiConnection();

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Menu");
    }


    public bool LogIn(string email, string password)
    {
        bool success = api.LogIn(email, password);
        Debug.Log(success);
        return success;
    }

    public bool Register(string name, string email, string password)
    {
        bool success = api.SignUp(name, email, password);
        Debug.Log(success);
        return success;
    }
}
