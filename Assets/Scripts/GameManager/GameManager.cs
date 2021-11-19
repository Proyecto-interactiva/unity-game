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


    public void LogIn()
    {
        api.LogIn("email@test.com", "123456");
    }
}
