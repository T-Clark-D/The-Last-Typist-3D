using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour,  IcraftingItem
{
    public static int SpikeTrapCount;
    public static int FleshBagCount;
    public static int BombTrapCount;
    public static String selectedObject_inventory = "";

    private const int SLOTS = 3;

    private List<InventorySlot> mSlots = new List<InventorySlot>();

    public event EventHandler<IInventoryEventArgs> ItemAdded;
    public event EventHandler<IInventoryEventArgs> ItemRemoved;
    public event EventHandler<IInventoryEventArgs> ItemUsed;

    public InventoryItemBase fleshBagsitem;
    public InventoryItemBase spikeTrapitem;
    public InventoryItemBase bombTrapitem;



    public Inventory()
    {
        for (int i = 0; i < SLOTS; i++)
        {
            mSlots.Add(new InventorySlot(i));

        }
        
    }

    
    internal void UseItem(IInventoryItem item, bool isSelected)
    {
        if(ItemUsed != null)
        {
            ItemUsed(this, new IInventoryEventArgs(item));
        }
        if (isSelected)
        {
            selectedObject_inventory = item.Name;
            GameHandler.selectedObject = item.Name;
        }
        else
        {
            selectedObject_inventory = "";
            GameHandler.selectedObject = "";

        }


        item.OnUse(true);
    }

    public bool ConsumeItem(string itemName , InventoryItemBase item)
    {
        bool isEnoughMaterial = false;
        if (itemName == "FleshBags")
        {
            if (mSlots[0].Remove(item))
            {
                isEnoughMaterial = true;
                if (ItemRemoved != null)
                {
                    ItemRemoved(this, new IInventoryEventArgs(item));
                }
                FleshBagCount = mSlots[0].Count;
            }
        }

        if (itemName == "SpikeTrap")
        {
            if (mSlots[1].Remove(item))
            {
                isEnoughMaterial = true;

                if (ItemRemoved != null)
                {
                    ItemRemoved(this, new IInventoryEventArgs(item));
                }
                SpikeTrapCount = mSlots[1].Count;

            }
        }

        if (itemName == "BombTrap")
        {
            if (mSlots[2].Remove(item))
            {
                isEnoughMaterial = true;

                if (ItemRemoved != null)
                {
                    ItemRemoved(this, new IInventoryEventArgs(item));
                }
                BombTrapCount = mSlots[2].Count;

            }
        }

        return isEnoughMaterial;

    }

    public void ProduceItem(string itemName , InventoryItemBase item)
    {
        if (itemName == "FleshBag")
        {
            mSlots[0].AddItem(item);
            if (ItemAdded != null)
            {
                ItemAdded(this, new IInventoryEventArgs(item));
            }
            FleshBagCount = mSlots[0].Count;
        }

        if (itemName == "SpikeTrap")
        {
            mSlots[1].AddItem(item);
            if (ItemAdded != null)
            {
                ItemAdded(this, new IInventoryEventArgs(item));
            }
            SpikeTrapCount = mSlots[1].Count;
        }

        if (itemName == "BombTrap")
        {
            mSlots[2].AddItem(item);
            if (ItemAdded != null)
            {
                ItemAdded(this, new IInventoryEventArgs(item));
            }
            BombTrapCount = mSlots[2].Count;
        }

    }

    public int itemCount(string itemName, InventoryItemBase item)
    {
        if (itemName == "FleshBags")
        {
            return mSlots[0].Count;
        }

        if (itemName == "SpikeTrap")
        {
            return mSlots[1].Count;
        }

        if (itemName == "BombTrap")
        {
            return mSlots[2].Count;
        }

        return 0;
    }


// not used for now
    // Start is called before the first frame update
    void Start()
    {

    }

    private InventorySlot FindStackableSlot(InventoryItemBase item)
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.IsStackable(item))
            {
                return slot;
            }

            return null;
        }
        return null;
    }

    private InventorySlot FindNextEmptySlot()
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.IsEmpty)
            {
                return slot;
            }

            return null;
        }
        return null;
    }
    public void AddItem(InventoryItemBase item)
    {
        if (item.name == "FleshBags")
        {
            mSlots[0].AddItem(item);
            FleshBagCount = mSlots[0].Count;
        }

        if (item.name == "SpikeTrap")
        {
            mSlots[1].AddItem(item);
            SpikeTrapCount = mSlots[1].Count;
        }

        if (item.name == "BombTrap")
        {
            mSlots[2].AddItem(item);
            BombTrapCount = mSlots[2].Count;
        }

        if (ItemAdded != null)
        {
            ItemAdded(this, new IInventoryEventArgs(item));
        }
    }


    public void RemoveItem(InventoryItemBase item)
    {
        foreach (InventorySlot slot in mSlots)
        {
            if (slot.Remove(item))
            {
                if (ItemRemoved != null)
                {
                    ItemRemoved(this, new IInventoryEventArgs(item));
                }
                break;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }


}
