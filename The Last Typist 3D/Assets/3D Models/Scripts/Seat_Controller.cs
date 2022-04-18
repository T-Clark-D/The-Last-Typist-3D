using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seat_Controller : MonoBehaviour
{
    public Animator anim;

    public void RaiseSeat()
    {
        anim.SetBool("isSeatRaised", true);
    }

    public void LowerSeat()
    {
        anim.SetBool("isSeatRaised", false);
    }
}
