using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingVolume : MonoBehaviour
{
    public Slider vol;
    // Start is called before the first frame update
    void Start()
    {
        vol.value = Settings.Volume;
    }
}
