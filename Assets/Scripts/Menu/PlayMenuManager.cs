using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenuManager : MonoBehaviour
{
    GameManager gameManager;
    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void Play()
    {
        StartCoroutine(gameManager.getSave(saveLoadSuccess, saveLoadError));
    }

    public void saveLoadSuccess(Save save)
    {
        // Do something with save. Probably use it to store what's being made and stuff.
        SceneManager.LoadScene("MainScene");
    }

    public void saveLoadError()
    {
        StartCoroutine(gameManager.newSave(saveLoadSuccess, newSaveError));
    }

    public void newSaveError()
    {
        // Something to do if the save is not found example, try again.
    }
}
