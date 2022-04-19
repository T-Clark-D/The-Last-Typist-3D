using System;
using UnityEngine;

[Serializable]
public class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    
    public virtual string Name
    {
        get
        {
            return "_base_item";
        }
    }

    public Sprite _Image;

    public virtual Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public bool _isSelected;

    public virtual bool isSelected
    {
        get
        {
            return _isSelected;
        }
        set
        {
            isSelected = _isSelected;
        }
    }

    public int _itemAmount;

    public virtual int itemAmount
    {
        get
        {
            return _itemAmount;
        }
        set
        {
            itemAmount = _itemAmount;
        }
    }

    public InventorySlot _slot;

    public virtual InventorySlot Slot
    {
        get
        {
            return _slot;
        }
        set
        {
            Slot = _slot;
        }
    }

    public virtual void OnPickup()
    {
        //TODO add logic what happens when metal is picked up
        gameObject.SetActive(false);
    }
    public virtual void OnDrop()
    {
        //TODO ... Move a logic like this to a base class or helper
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }

    public virtual void OnUse(bool onUse)
    {
        //TODO ... Move a logic like this to a base class or helper
    }
}
