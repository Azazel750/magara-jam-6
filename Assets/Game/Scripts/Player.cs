using System;
using UnityEngine;

public class Player : Human
{
    public new Rigidbody rigidbody;
    public float speed = 2;
    public float dashMultiplier = 2;
    public float moveDirectionSmoothTime = 1;
    public float fightTimeSeconds = 0.5f;
    private float timeLeftFight = 0;
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
    public bool isFighting;

    private void Update()
    {
        timeLeftFight += Time.deltaTime;
        if (timeLeftFight >= fightTimeSeconds)
        {
            FightMode(false);
        }

        if (Input.GetMouseButtonDown(0)) FightMode(true);
        
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        inputVectorNormalized = Vector3.SmoothDamp(inputVectorNormalized, 
            new Vector3(horizontal, 0, vertical).normalized, ref moveVelocity, moveDirectionSmoothTime);
        
        if(inputVectorNormalized.magnitude > 0.1f) transform.rotation = Quaternion.Lerp(transform.rotation, 
            Quaternion.LookRotation(rigidbody.velocity), 
            Time.deltaTime * 15);
        
        if(Input.GetKeyDown(KeyCode.C)) rigidbody.AddForce(inputVectorNormalized * (speed * dashMultiplier), ForceMode.Impulse);
    }

    public void FightMode(bool state)
    {
        isFighting = state;
        timeLeftFight = 0;
    }

    private void FixedUpdate()
    {
        Move(inputVectorNormalized);
    }

    private void Move(Vector3 direction)
    {
        
        rigidbody.velocity = Vector3.Lerp(rigidbody.velocity, direction * speed, Time.fixedDeltaTime * velocitySmoothTime);
    }

    public void Fight(Collider other)
    {
        Fight(other.GetComponentInParent<Enemy>());
    }

    private void Fight(Enemy enemy)
    {
        enemy.Kill();
    }

    public void OnStageNextTrigger(Collider other)
    {
        StageManager.Instance.Next();
    }
    
    public void OnStageBackTrigger(Collider other)
    {
        StageManager.Instance.Back();
    }
}
