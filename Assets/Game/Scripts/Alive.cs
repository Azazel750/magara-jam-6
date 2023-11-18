using System;
using UnityEngine;

public class Alive : MonoBehaviour
{
    public float Health
    {
        get
        {
            return health;
        }
        set
        {
            OnHealthChanged?.Invoke(health, value);
            health = value;
        }
    }

    public event Action<float, float> OnHealthChanged;
    private float health = 100;
    public virtual void Awake()
    {
        GameManager.AddAlive(this);
    }
}