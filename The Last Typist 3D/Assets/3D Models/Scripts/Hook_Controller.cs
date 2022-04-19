using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook_Controller : MonoBehaviour
{
    public Animator anim;

    public void RaiseOrLowerHook()
    {
        bool lastBool = anim.GetBool("isRaised");
        anim.SetBool("isRaised", !lastBool);
    }
}
