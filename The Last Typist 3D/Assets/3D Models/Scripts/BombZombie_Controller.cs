using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombZombie_Controller : MonoBehaviour
{
    public Animator anim;

    public void StartOrStopSlowWalkAnimation()
    {
        bool lastBool = anim.GetBool("isWalkingSlow");
        anim.SetBool("isWalkingSlow", !lastBool);
    }

    public void StartOrStopFastWalkAnimation()
    {
        bool lastBool = anim.GetBool("isWalkingFast");
        anim.SetBool("isWalkingFast", !lastBool);
    }

    public void TriggerAttackAnimation()
    {
        anim.SetTrigger("attack");
    }

    public void DeathAnimation()
    {
        anim.SetTrigger("die");
    }
}
