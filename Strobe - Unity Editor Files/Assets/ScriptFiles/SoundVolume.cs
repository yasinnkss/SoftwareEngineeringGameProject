using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{
    public AudioSource vol;
    public Slider volCTRL;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vol.volume = volCTRL.value;
    }
}
