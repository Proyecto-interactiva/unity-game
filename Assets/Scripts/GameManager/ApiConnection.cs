using System.Collections;
using System.Collections.Generic;
using System.Net;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;



public class ApiConnection
{
    private string jwt;
    private string username;
    private string gameName = "game name";
    private string url = "https://fractal-interactiva.herokuapp.com";

    public void LogIn(string email, string password)
    {
        PlayerData playerData = new PlayerData();
        playerData.email = email;
        playerData.password = password;
        string json = JsonUtility.ToJson(playerData);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/api/sign/in-user");
        request.Method = "POST";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.ContentLength = bodyRaw.Length;
        request.ContentType = "application/json";
        Stream dataStream = request.GetRequestStream();
        dataStream.Write(bodyRaw, 0, bodyRaw.Length);
        dataStream.Close();
        WebResponse response = request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string JsonResponse = reader.ReadToEnd();

        Auth(email, password);
    }

    public void SignUp(string name, string email, string password)
    {
        PlayerData playerData = new PlayerData();
        playerData.name = name;
        playerData.email = email;
        playerData.password = password;
        string json = JsonUtility.ToJson(playerData);
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/api/sign/up");
        request.Method = "POST";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.ContentLength = bodyRaw.Length;
        request.ContentType = "application/json";
        Stream dataStream = request.GetRequestStream();
        dataStream.Write(bodyRaw, 0, bodyRaw.Length);
        dataStream.Close();
        WebResponse response = request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string JsonResponse = reader.ReadToEnd();

        Auth(email, password);
    }

    private void Auth(string email, string password)
    {
        PlayerData playerData = new PlayerData();
        playerData.email = email;
        playerData.password = password;
        string json = JsonUtility.ToJson(playerData);

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + "/api/auth");
        request.Method = "POST";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.ContentLength = bodyRaw.Length;
        request.ContentType = "application/json";
        Stream dataStream = request.GetRequestStream();
        dataStream.Write(bodyRaw, 0, bodyRaw.Length);
        dataStream.Close();
        WebResponse response = request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string JsonResponse = reader.ReadToEnd();
        Token info = JsonUtility.FromJson<Token>(JsonResponse);
        jwt = info.token;
        Debug.Log(jwt);
}

    public void newSave()
    {
        // post new save
    }

    public void getMessages(int stageId, int characterId)
    {
        // get messages

    }

}

public class MessagesResponse
{
    public List<string> dialogs;
    public bool quest;
    public List<Answer> answers;
}

public class Answer
{
    public string content;
    public bool correct;
    public int score; // may be float
}

public class PlayerData
{
    public string name;
    public string email;
    public string password;
}

public class Token
{
    public string token;
}


