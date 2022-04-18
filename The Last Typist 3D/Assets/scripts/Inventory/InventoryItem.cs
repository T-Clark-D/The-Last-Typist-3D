using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }

    void OnPickup();

    void OnDrop();
    void OnUse();

    InventorySlot Slot { get; set; }
}


public class IInventoryEventArgs : EventArgs
{
    public IInventoryEventArgs (IInventoryItem item)
    {
        Item = item;
    }
    public IInventoryItem Item;
}
    public class inventory : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
