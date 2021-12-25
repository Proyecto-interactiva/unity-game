using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class messagesDisplay : MonoBehaviour
{

    public TMP_Text messageDisplay;
    List<string> messages;
    int currentMessageIndex = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMessageIndex != -1)
        {
            messageDisplay.text = messages[currentMessageIndex];
        }
        else
        {
            messageDisplay.text = "";
        }
        
    }

    public void ShowMessages(List<string> messages)
    {
        this.messages = messages;
        Debug.Log(this.messages);
        Debug.Log(this.messages.Count);
        nextMessage();
    }

    public void nextMessage()
    {
        if (currentMessageIndex + 1 < messages.Count)
        {
            currentMessageIndex++;
        }
        else
        {
            HideDisplay();
        }
    }

    void HideDisplay()
    {
        currentMessageIndex = -1;
        messages.Clear();
        this.gameObject.SetActive(false);
    }


}
