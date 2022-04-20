using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_Controller : MonoBehaviour
{
    public Animator anim;

    public void SwingTarget()
    {
        anim.SetTrigger("swingTarget");
    }
}
