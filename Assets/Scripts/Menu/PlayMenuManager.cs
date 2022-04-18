using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PlayMenuManager : MonoBehaviour
{
    GameManager gameManager;
    public GameObject code;
    private TMP_InputField codeInputField;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        codeInputField = code.GetComponent<TMP_InputField>();
    }
    public void Play()
    {
        FindObjectOfType<AudioManager>().Play("Text");
        StartCoroutine(gameManager.CheckGame(codeInputField.text, gameCheckSuccess, gameCheckError));
    }


    public void gameCheckSuccess()
    {
            StartCoroutine(gameManager.getSave(saveLoadSuccess, saveLoadError));
    }

    public void gameCheckError()
    {
        // something to aler the player that the code doesn't exist.
        Debug.Log("Game not found");
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
