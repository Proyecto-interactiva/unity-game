using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Collider2D activator;
    // private bool spawned = false; // GABO - no utilizada

    private void OnTriggerEnter2D(Collider2D entity)
    {
        if (activator == entity)
        {
            //spawned = true; // GABO - no utilizada
            transform.Find("CharacterSprite").gameObject.SetActive(true);
            transform.Find("InteractionCanvas").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Open");
        }
        
    }

    private void OnTriggerExit2D(Collider2D entity)
    {
        transform.Find("InteractionCanvas").gameObject.SetActive(false);
    }
}
