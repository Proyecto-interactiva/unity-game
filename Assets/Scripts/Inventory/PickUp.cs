using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private GameObject info;
    private Collider2D Picker;

    private void OnTriggerEnter2D(Collider2D player)
    {
        info = this.gameObject.transform.GetChild(0).gameObject;
        info.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        info = this.gameObject.transform.GetChild(0).gameObject;
        info.SetActive(false);
    }

    public void PickItem()
    {

    }
}
