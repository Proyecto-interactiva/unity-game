using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManage : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
    }

    /// <summary>
    /// Menu Options
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void LogIn()
    {
        gameManager.LogIn();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
