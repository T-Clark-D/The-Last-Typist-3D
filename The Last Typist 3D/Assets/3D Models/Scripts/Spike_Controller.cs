using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_Controller : MonoBehaviour
{
    public Animator[] spikeAnimators;

    public void TriggerSpikes()
    {
        foreach(Animator anim in spikeAnimators)
        {
            anim.SetTrigger("spikeTrigger");
        }
    }
}
