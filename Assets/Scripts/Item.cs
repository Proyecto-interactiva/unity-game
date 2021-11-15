using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    private GameObject info;
    [SerializeField] private PlayerLogic picker;

    public enum ItemType
    {
        Book1,
        Book2,
        Book3,
        Book4,
        Book5
    }

    public ItemType itemType;
    public string content;

    public TMP_Text messageDisplay;


    private void Start()
    {
        messageDisplay.text = content;
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        info = this.gameObject.transform.Find("Info").gameObject;
        info.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        info = this.gameObject.transform.Find("Info").gameObject;
        info.SetActive(false);
    }

    public void PickItem()
    {
        picker.inventory.AddItem(this);
        this.gameObject.SetActive(false);
    }

    public void DropItem()
    {
        this.transform.position = picker.transform.position;
        this.gameObject.SetActive(true);
        
    }

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.Book1: return ItemAssets.Instance.book1;
            case ItemType.Book2: return ItemAssets.Instance.book2;
            case ItemType.Book3: return ItemAssets.Instance.book3;
            case ItemType.Book4: return ItemAssets.Instance.book4;
            case ItemType.Book5: return ItemAssets.Instance.book5;
        }
    }

}
