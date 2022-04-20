using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinder_Controller : MonoBehaviour
{
    public Animator anim;

    public void ToggleBlades()
    {
        bool lastBool = anim.GetBool("isBladesSpinning");
        anim.SetBool("isBladesSpinning", !lastBool);
    }
}
