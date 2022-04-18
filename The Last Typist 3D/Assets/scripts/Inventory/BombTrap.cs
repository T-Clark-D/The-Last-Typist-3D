using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrap : InventoryItemBase
{

    public override string Name
    {
        get
        {
            return "BombTrap";
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
