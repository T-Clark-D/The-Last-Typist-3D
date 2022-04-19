using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "SpikeTrap";
        }
    }


    public override Sprite Image
    {
        get
        {
            return _Image;
        }
    }
    public override void OnUse(bool onUse)
    {
        //ToDo : do smth with the object....
        _isSelected = onUse;
        base.OnUse(onUse);
    }
}
