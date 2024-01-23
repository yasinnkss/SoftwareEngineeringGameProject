using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Gradient gradient;
    [SerializeField]
    public Slider slider;
    public Image fill;


    private void Start()
    {

    }

    //Update is called once per frame
    public void maxHealth(int playerHealth)
    {
        slider.maxValue = playerHealth;
        slider.value = playerHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetHealth(int playerHealth)
    {
        slider.value = playerHealth;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        playerHealth = Mathf.Clamp(playerHealth, 0, 100);
        GameObject.Find("HpAmount").GetComponent<TextMeshProUGUI>().text = playerHealth.ToString() + " / 100";
    }
}
