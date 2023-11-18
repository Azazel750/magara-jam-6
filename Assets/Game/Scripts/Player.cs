using System;
using System.Linq;
using UnityEngine;

public class Player : Human
{
    public new Rigidbody rigidbody;
    public float speed = 2;
    public float dashMultiplier = 2;
    public float attackDashMultiplier = 2;
    public float moveDirectionSmoothTime = 1;
    public float fightTimeSeconds = 0.5f;
    private float timeLeftFight = 0;
    private Vector3 inputVectorNormalized;
    public static Player Instance { get; private set; }

    public static Vector3 position => Instance == null ? Vector3.zero : Instance.transform.position;
    
    public float velocitySmoothTime;

    public override void Awake()
    {
        base.Awake();
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private Vector3 moveVelocity;
    public bool isFighting;

    private void Update()
    {
        var closest = GameManager
            .alives
            .OrderBy(x => x is not null ? Vector3.Distance(x.transform.position, transform.position) : 9999);;

        var available = closest.ToList().Find(x => x is Enemy 
                                                   && Vector3.Distance(x.transform.position, transform.position) < 1);

        if (available)
        {
            
        }
        
        
        
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
        
        if(Input.GetKeyDown(KeyCode.Space)) DashTo(dashMultiplier);
    }

    public void DashTo(float multiplier)
    {
        rigidbody.AddForce(inputVectorNormalized * (speed * multiplier), ForceMode.Impulse);
    }

    public void FightMode(bool state)
    {
        isFighting = state;
        timeLeftFight = 0;
        if(state) DashTo(attackDashMultiplier);
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
