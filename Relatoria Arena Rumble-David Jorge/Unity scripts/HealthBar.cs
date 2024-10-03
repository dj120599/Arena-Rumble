using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient grad;
    public Image fill;

    public void SetMaxHealth(int Health)
    {
        slider.maxValue = Health;
        slider.value = Health;
        grad.Evaluate(1f);
    }
    // Start is called before the first frame update
    public void SetHealth(int Health)
    {
        // iguala o valor do slider ao valor da vida
        slider.value = Health;

        fill.color = grad.Evaluate(slider.normalizedValue);
    }

}
