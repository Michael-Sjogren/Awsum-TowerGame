using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeController : MonoBehaviour {

    void Update ()
    {
    }

    public void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
    }
}
