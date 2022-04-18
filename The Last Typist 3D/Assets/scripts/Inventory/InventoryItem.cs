using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryItem
{
    string Name { get; }
    Sprite Image { get; }

    int itemAmount { get; set; }

    bool isSelected { get; set; }

    void OnPickup();

    void OnDrop();
    void OnUse(bool onUse);

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
  
