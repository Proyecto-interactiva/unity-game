using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;
    public delegate void UpdateInventory();
    public event UpdateInventory Updated;

    public Inventory()
    {
        itemList = new List<Item>();
        Debug.Log("Inventario");
    }

    public void AddItem(Item item)
    {
        Debug.Log("Item added");
        itemList.Add(item);
        Updated?.Invoke();
    }

    public void RemoveItem(Item item)
    {
        Debug.Log("Item dropped");
        item.DropItem();
        itemList.Remove(item);
        Updated?.Invoke();
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void Clear()
    {
        itemList.Clear();
        Updated?.Invoke();
    }
}
