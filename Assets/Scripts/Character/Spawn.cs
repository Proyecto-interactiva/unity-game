using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Collider2D activator;
    private bool spawned = false;

    private void OnTriggerEnter2D(Collider2D entity)
    {
        if (activator == entity)
        {
            spawned = true;
            transform.Find("CharacterSprite").gameObject.SetActive(true);
            transform.Find("TalkButtonCanvas").gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D entity)
    {
        transform.Find("TalkButtonCanvas").gameObject.SetActive(false);
    }
}
