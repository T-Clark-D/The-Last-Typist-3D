using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Controller : MonoBehaviour
{
    public Animator anim;

    public void StartOrStopWalkAnimation1()
    {
        bool lastBool = anim.GetBool("isWalking1");
        anim.SetBool("isWalking1", !lastBool);
    }

    public void StartOrStopWalkAnimation2()
    {
        bool lastBool = anim.GetBool("isWalking2");
        anim.SetBool("isWalking2", !lastBool);
    }

    public void StartOrStopWalkAnimation3()
    {
        bool lastBool = anim.GetBool("isWalking3");
        anim.SetBool("isWalking3", !lastBool);
    }

    public void StartOrStopAttackAnimation()
    {
        bool lastBool = anim.GetBool("isAttacking");
        anim.SetBool("isAttacking", !lastBool);
    }

    public void JumpAnimation()
    {
        anim.SetTrigger("jump");
    }

    public void DeathAnimation()
    {
        anim.SetTrigger("die");
    }

    public void ReviveAnimation()
    {
        anim.SetTrigger("revive");
    }
}
