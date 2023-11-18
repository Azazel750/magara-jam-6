using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image healthbar;
    public float maxHealth;
    private void Start()
    {
        Player.Instance.OnHealthChanged += (lastHealth, newHealth) => 
        {
            Debug.Log("Health: " + newHealth);
            healthbar.fillAmount = Mathf.Clamp(newHealth / maxHealth, 0, 1);
        
        };
    }

}
