using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    public Slider slider;
    public float staminaChange;
    
    
    public void SetMaxStamina(int maxStamina)
    {
        slider.maxValue = maxStamina;
        slider.value = maxStamina;
    }
    
    public void SetCurrentStamina(int currentStamina)
    {
        slider.value = currentStamina;
    }

    public void DecreaseStamina()
    {
        slider.value -= staminaChange * Time.deltaTime; 
    }

    public void IncreaseStamina()
    {
        slider.value += staminaChange * Time.deltaTime;
    }
}
