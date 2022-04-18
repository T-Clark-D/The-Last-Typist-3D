using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IcraftingItem 
{
    void ConsumeItem(string itemName);
    void ProduceItem(string itemName);
    int itemCount(string itemName);
}
