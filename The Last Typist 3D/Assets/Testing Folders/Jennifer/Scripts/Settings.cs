using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static float Volume;

    // Start is called before the first frame update
    private void Start()
    {
        Volume = 0;
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        Debug.Log("Volume in UI:" + volume);
        Volume = volume;
    }
}
