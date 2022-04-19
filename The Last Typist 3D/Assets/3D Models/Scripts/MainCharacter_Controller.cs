using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter_Controller : MonoBehaviour
{
    public Animator anim;

    public void SwitchMode()
    {
        bool lastBool = anim.GetBool("isShootingPosition");
        anim.SetBool("isShootingPosition", !lastBool);
    }

    public void ToggleShooting()
    {
        bool lastBool = anim.GetBool("isShooting");
        anim.SetBool("isShooting", !lastBool);
    }

    public void StartOrStopWalkAnimation()
    {
        bool lastBool = anim.GetBool("isWalking");
        anim.SetBool("isWalking", !lastBool);
    }

    public void ToggleSitting()
    {
        bool lastBool = anim.GetBool("isSitting");
        anim.SetBool("isSitting", !lastBool);
    }
}
