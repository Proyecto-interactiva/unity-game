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
    private string gameName = "Test9";
    private int stageId = 0;
    private string generalUri = "https://fractal-interactiva.herokuapp.com/api";
    public GameObject bookPrefab;

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
        using (UnityWebRequest www = UnityWebRequest.Get($"{generalUri}/game/messages?userName={userName}&gameName={gameName}&stageId={stageId}&character={characterId}"))
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

    public IEnumerator PostAnswer(WWWForm form, int characterId, Action<FeedbackResponse> CallBackSuccess, Action CallbackError)
    {
        using (UnityWebRequest www = UnityWebRequest.Post($"{generalUri}/game/answer?userName={userName}&gameName={gameName}&stageId={stageId}&character={characterId}", form))
        {
            Debug.Log("Posting answers");
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
                Debug.Log("Answers posted!");
                var json = www.downloadHandler.text;
                var response = JsonUtility.FromJson<FeedbackResponse>(json);
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

    public void SpawnBooks(List<string> answers)
    {
        int x = -6;
        int type = 1;
        foreach (string answer in answers)
        {
            SpawnBook(answer, x, 9, type);
            x+= 3;
            type++;
            if (type > 5) type = 1;
        }
    }

    private void SpawnBook(string answer, float x, float y, int type)
    {
        GameObject newBook = Instantiate(bookPrefab, new Vector3(x, y, 0), Quaternion.identity);
        Item item = newBook.GetComponent<Item>();
        switch (type)
        {
            case 1:
                item.itemType = Item.ItemType.Book1;
                break;
            case 2:
                item.itemType = Item.ItemType.Book2;
                break;
            case 3:
                item.itemType = Item.ItemType.Book3;
                break;
            case 4:
                item.itemType = Item.ItemType.Book4;
                break;
            case 5:
                item.itemType = Item.ItemType.Book5;
                break;
            default:
                item.itemType = Item.ItemType.Book1;
                break;
        }
        newBook.GetComponent<Item>().content = answer;
    }
}