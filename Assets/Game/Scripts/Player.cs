using System;
using UnityEngine;

public class Player : Human
{
    public new Rigidbody rigidbody;
    public float speed = 2;
    public float dashMultiplier = 2;
    public float moveDirectionSmoothTime = 1;
    private Vector3 inputVectorNormalized;
    public static Player Instance { get; private set; }

    public static Vector3 position => Instance.transform.position;
    
    public float velocitySmoothTime;

    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private Vector3 moveVelocity;

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        inputVectorNormalized = Vector3.SmoothDamp(inputVectorNormalized, 
            new Vector3(horizontal, 0, vertical).normalized, ref moveVelocity, moveDirectionSmoothTime);
        
        if(Input.GetKeyDown(KeyCode.C)) rigidbody.AddForce(inputVectorNormalized * speed * dashMultiplier, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Move(inputVectorNormalized);
    }

    private void Move(Vector3 direction)
    {
        
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, direction * speed, Time.fixedDeltaTime * velocitySmoothTime);
    }
}
