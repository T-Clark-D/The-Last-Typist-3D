using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemAmount
{
    public String itemName;

    [Range(1,99)]
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;
    public ResourceManager ResourceManager;

    public bool canCraft (Inventory inventory)
    {
        foreach( ItemAmount itemAmount in Materials)
        {
            if(ResourceManager.itemCount(itemAmount.itemName) < itemAmount.Amount)
            {
                return false;
            }
        }
        return true;
    }

    public void craft(Inventory inventory)
    {
        if (canCraft(inventory))
        {
            foreach (ItemAmount itemAmount in Materials)
            {
                for(int i=0; i < itemAmount.Amount; i++)
                {
                    ResourceManager.ConsumeItem(itemAmount.itemName);
                }
            }

            foreach (ItemAmount itemAmount in Results)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    inventory.ProduceItem(itemAmount.itemName);
                }
            }
        }
    }
}
