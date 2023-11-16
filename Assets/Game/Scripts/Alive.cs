using System;
using UnityEngine;

public class Alive : MonoBehaviour
{
    public virtual void Awake()
    {
        GameManager.AddAlive(this);
    }
}