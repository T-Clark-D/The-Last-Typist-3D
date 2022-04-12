using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private const int SLOTS = 4;

    private List<InventorySlot> mSlots = new List<InventorySlot>();

    public event EventHandler<IInventoryEventArgs> ItemAdded;
    public event EventHandler<IInventoryEventArgs> ItemRemoved;
    public event EventHandler<IInventoryEventArgs> ItemUsed;

    public Inventory()
    {
        for (int i=0; i< SLOTS; i++)
        {
            mSlots.Add(new InventorySlot(i));
        }
    }

    private InventorySlot FindStackableSlot(IInventoryItem item)
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
    public void AddItem(IInventoryItem item)
    {
        InventorySlot freeSlot = FindNextEmptySlot();
        if (mSlots.Count < SLOTS)
        {
            Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
            if (freeSlot == null)
            {
                freeSlot = FindNextEmptySlot();
            }

            if (freeSlot != null)
            {
                freeSlot.AddItem(item);

                if (ItemAdded != null)
                {
                    ItemAdded(this, new IInventoryEventArgs(item));
                }
            }
        }
    }
    
    internal void UseItem(IInventoryItem item)
    {
        if(ItemUsed != null)
        {
            ItemUsed(this, new IInventoryEventArgs(item));
        }
        item.OnUse();
    }

    public void RemoveItem (IInventoryItem item)
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
