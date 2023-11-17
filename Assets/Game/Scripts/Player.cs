using System;
using UnityEngine;

public class Player : Human
{
    public static Player Instance { get; private set; }

    public static Vector3 position => Instance.transform.position;
    

    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
}