using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string jwt;
    private string userName;
    private string gameName = "Test5";
    private int stageId = 0;
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
                var json = www.downloadHandler.text;
                var playerData = JsonUtility.FromJson<PlayerData>(json);
                userName = playerData.name;
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

    public IEnumerator NextMessage(int characterId, Action<MessagesResponse> CallBackSuccess, Action CallbackError)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{generalUri}/game/messages?username={userName}&gameName={gameName}&stageId={stageId}&character={characterId}"))
        {
            Debug.Log("Next messages requested");
            www.SetRequestHeader("Authorization", $"Bearer {jwt}");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error");
                Debug.Log(www.error);

                CallbackError();
            }
            else
            {
                Debug.Log("Messages received!");
                var json = www.downloadHandler.text;
                var response = JsonUtility.FromJson<MessagesResponse>(json);
                Debug.Log(response);
                CallBackSuccess(response);
            }
        }
    }

    public IEnumerator newSave(Action<Save> CallbackSuccess, Action CallbackError)
    {
        WWWForm form = new WWWForm();
        form.AddField("gameName", gameName);
        form.AddField("userName", userName);
        using (UnityWebRequest www = UnityWebRequest.Post($"{generalUri}/game/newEmpty", form))
        {
            Debug.Log("Creating new save");
            www.SetRequestHeader("Authorization", $"Bearer {jwt}");
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
                var save = JsonUtility.FromJson<Save>(json);
                CallbackSuccess(save);

            }
        }
    }

    public IEnumerator getSave(Action<Save> CallbackSuccess, Action CallbackError)
    {
        using (UnityWebRequest www = UnityWebRequest.Get($"{generalUri}/game?userName={userName}"))
        {
            Debug.Log("Creating new save");
            www.SetRequestHeader("Authorization", $"Bearer {jwt}");
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error");
                Debug.Log(www.error);
                CallbackError();
            }
            else
            {
                Debug.Log("Save found!");
                var json = www.downloadHandler.text;
                var save = JsonUtility.FromJson<Save>(json);
                CallbackSuccess(save);
            }
        }
    }

}