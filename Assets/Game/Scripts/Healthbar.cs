using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Image healthbar;
    public float health; 
    public float maxHealth;
    private void Start()
    {
        Player.Instance.OnHealthChanged += (lastHealth, newHealth) => 
        {
            health = lastHealth;
            healthbar.fillAmount = Mathf.Clamp(health / maxHealth, 0, 1);
        
        };
    }

}
