using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderText = null;

    private void Update()
    {
        sliderText.text = System.Convert.ToInt32((GameObject.Find("Music Slider")
            .GetComponent<Slider>().value * 100)).ToString();
    }


}
