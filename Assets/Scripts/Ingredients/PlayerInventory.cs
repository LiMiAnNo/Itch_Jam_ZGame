using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<string> items = new List<string>();

    public void AddItem(string itemName)
    {
        if (!items.Contains(itemName))
        {
            items.Add(itemName);
            Debug.Log($"Added to inventory: {itemName}");
        }
    }

    public bool HasItem(string itemName)
    {
        return items.Contains(itemName);
    }

    public List<string> GetItems()
    {
        return items;
    }
}
