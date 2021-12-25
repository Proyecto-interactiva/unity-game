using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talk : MonoBehaviour
{

    GameManager gameManager;
    public GameObject messageDisplay;
    // Start is called before the first frame update
    public int characterId;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        //Get next messages
        StartCoroutine(gameManager.NextMessage(characterId, ManageMessages, ManageError));
        
    }

    public void ManageMessages(MessagesResponse response)
    {
        //show message in message display
        messageDisplay.SetActive(true);
        messageDisplay.GetComponent<messagesDisplay>().ShowMessages(response.dialogs);
        // spawn items in the world
    }

    public void ManageError()
    {
        Debug.Log("Messages failed to load");
        StartCoroutine(gameManager.NextMessage(characterId, ManageMessages, ManageError));
    }
}
