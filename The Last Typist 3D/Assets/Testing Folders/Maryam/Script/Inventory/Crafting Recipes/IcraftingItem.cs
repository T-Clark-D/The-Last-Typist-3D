using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IcraftingItem 
{
    void ConsumeItem(string itemName, InventoryItemBase item);
    void ProduceItem(string itemName , InventoryItemBase item);
    int itemCount(string itemName, InventoryItemBase item);
}
