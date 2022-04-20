using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Metal";
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
        
    }
}
