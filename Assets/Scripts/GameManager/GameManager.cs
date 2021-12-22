using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string jwt;
    private string username;
    private string gameName = "Test1";
    private string stageId;
    private string generalUri = "https://fractal-interactiva.herokuapp.com/api";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene("Menu");
    }

    public IEnumerator PostForm(string specificUri, WWWForm form, Action FallbackSuccess, Action FallbackError)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(generalUri + specificUri, form))
        {
            Debug.Log("Post started");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error");
                Debug.Log(www.error);
                FallbackError();
            }
            else
            {
                Debug.Log("Form upload complete!");
                //Debug.Log(myObject);
                StartCoroutine(Auth(form, FallbackSuccess, FallbackError));

            }
        }
    }

    public IEnumerator Auth(WWWForm form, Action CallbackSuccess, Action CallbackError)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(generalUri + "/auth", form))
        {
            Debug.Log("Auth started");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error");
                Debug.Log(www.error);
                CallbackError();
            }
            else
            {
                
                var json = www.downloadHandler.text;
                var response = JsonUtility.FromJson<Token>(json);
                Debug.Log(response.token);
                Debug.Log("Auth Successful");

                jwt = response.token;
                CallbackSuccess();
            }
        }
    }

    public IEnumerator NextMessage(int characterId)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{generalUri}/game/messages?username={username}&gameName={gameName}&stageId={stageId}&character={characterId}"))
        {
            Debug.Log("Next messages requested");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error");
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Messages received!");
                var json = www.downloadHandler.text;
                var response = JsonUtility.FromJson<Token>(json);

            }
        }
    }

    public IEnumerator newSave(string userName)
    {
        WWWForm form = new WWWForm();
        form.AddField("gameName", gameName);
        form.AddField("usernName", userName);
        using (UnityWebRequest www = UnityWebRequest.Post($"{generalUri}/game/new", form))
        {
            Debug.Log("Creating new save");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error");
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("New save created!");
                var json = www.downloadHandler.text;
                var response = JsonUtility.FromJson<Token>(json);

            }
        }
    }

}