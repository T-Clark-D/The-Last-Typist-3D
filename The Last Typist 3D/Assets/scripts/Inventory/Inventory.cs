using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum slotNames
{
    FleshBag,
    SpikeTrap,
    BombTrap,    
}
public class Inventory : MonoBehaviour,  IcraftingItem
{

    private const int SLOTS = 4;

    private List<InventorySlot> mSlots = new List<InventorySlot>();
    private String selectedObject = "";

    public event EventHandler<IInventoryEventArgs> ItemAdded;
    public event EventHandler<IInventoryEventArgs> ItemRemoved;
    public event EventHandler<IInventoryEventArgs> ItemUsed;



    public Inventory()
    {
        for (int i = 0; i < SLOTS; i++)
        {
            mSlots.Add(new InventorySlot(i));

        }
        //IInventoryItem fleshbag = gameObject.AddComponent<FleshBags>();
        //mSlots[0].AddItem(gameObject.AddComponent<FleshBags>());
        //mSlots[1].AddItem(gameObject.AddComponent<SpikeTrap>());
        //mSlots[2].AddItem(gameObject.AddComponent<BombTrap>());
    }

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
        if(item.name == "FleshBags")
        {
            mSlots[0].AddItem(item);
        }
        
        if (item.name == "SpikeTrap")
        {
            mSlots[1].AddItem(item);
        }

        if (item.name == "BombTrap")
        {
            mSlots[2].AddItem(item);
        }

        if (ItemAdded != null)
        {
            ItemAdded(this, new IInventoryEventArgs(item));
        }

        //InventorySlot freeSlot = FindNextEmptySlot();
        //if (mSlots.Count < SLOTS)
        //{
        //    Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
        //    if (freeSlot == null)
        //    {
        //        freeSlot = FindNextEmptySlot();
        //    }

        //    if (freeSlot != null)
        //    {
        //        freeSlot.AddItem(item);

        //        if (ItemAdded != null)
        //        {
        //            ItemAdded(this, new IInventoryEventArgs(item));
        //        }
        //    }
        //}
    }
    
    internal void UseItem(IInventoryItem item, bool isSelected)
    {
        if(ItemUsed != null)
        {
            ItemUsed(this, new IInventoryEventArgs(item));
        }
        if (isSelected)
            selectedObject = item.Name;
        else
            selectedObject = "";

        item.OnUse(true);
    }

    public void RemoveItem (InventoryItemBase item)
    {
        foreach ( InventorySlot slot in mSlots)
        {
            if (slot.Remove(item))
            {
                if(ItemRemoved != null)
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

    public void ConsumeItem(string itemName)
    {
        
        if (itemName == "FleshBags")
        {
            if (mSlots[0].Remove(mSlots[0].Item))
            {
                if (ItemRemoved != null)
                {
                    ItemRemoved(this, new IInventoryEventArgs(mSlots[0].Item));
                }
                
            }
        }

        if (itemName == "SpikeTrap")
        {
            if (mSlots[1].Remove(mSlots[1].Item))
            {
                if (ItemRemoved != null)
                {
                    ItemRemoved(this, new IInventoryEventArgs(mSlots[0].Item));
                }

            }
        }

        if (itemName == "BombTrap")
        {
            if (mSlots[2].Remove(mSlots[2].Item))
            {
                if (ItemRemoved != null)
                {
                    ItemRemoved(this, new IInventoryEventArgs(mSlots[0].Item));
                }

            }
        }

    }

    public void ProduceItem(string itemName)
    {
        if (itemName == "FleshBags")
        {
            mSlots[0].AddItem(mSlots[0].Item);
            if (ItemAdded != null)
            {
                ItemAdded(this, new IInventoryEventArgs(mSlots[0].Item));
            }
        }

        if (itemName == "SpikeTrap")
        {
            mSlots[1].AddItem(mSlots[1].Item);
            if (ItemAdded != null)
            {
                ItemAdded(this, new IInventoryEventArgs(mSlots[1].Item));
            }
        }

        if (itemName == "BombTrap")
        {
            mSlots[2].AddItem(mSlots[2].Item);
            if (ItemAdded != null)
            {
                ItemAdded(this, new IInventoryEventArgs(mSlots[2].Item));
            }
        }

    }

    public int itemCount(string itemName)
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
}
