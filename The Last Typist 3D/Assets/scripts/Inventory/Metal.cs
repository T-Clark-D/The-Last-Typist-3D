using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : InventoryItemBase
{
    public string Name
    {
        get
        {
            return "Metal";
        }
    }

    public override void OnUse()
    {
        //ToDo : do smth with the object....
        base.OnUse();
    }
}
