using System;
using AOC;
using Enemy_States;
using Pathfinding;
using TMPro;
using UnityEngine;

public class Enemy : Alive
{
    public float enemyDamage = 10;
    public Rigidbody rb;
    public float speed = 0.1f;
    [HideInInspector] public Vector3 direction;
    private Transform target;
    IAstarAI ai;
    private StateManager _stateManager;

    private void Start()
    {
        _stateManager = new StateManager(this);
        _stateManager.Begin<IdleState>();

        OnHealthChanged += (last, next) =>
        {
            Debug.Log("WOA");
            if (next <= 0) Die();
        };
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    void OnEnable () {
        ai = GetComponent<IAstarAI>();
        target = GameManager.GetClosest<Human>(this).transform;
        
        if (ai != null) ai.onSearchPath += Update;
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            PlayerCollision(other);
        }
    }

    private void PlayerCollision(Collision other)
    {
        if (other.collider.TryGetComponent<Player>(out var player))
        {
            player.Damage(enemyDamage);
        }
    }

    void OnDisable () {
        
    }

    /// <summary>Updates the AI's destination every frame</summary>
    void Update ()
    {
        if(direction.magnitude > 0.1f) rb.velocity = direction.normalized * speed;
        direction = Vector3.zero;

    }

    public void MakeForward(Vector3 currentWaypointVector)
    {
        direction = currentWaypointVector;
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
    
    public float damageForceMultiplier;

    public void Damage(int damage, Vector3 damageDirection)
    {
        var go = Instantiate(GameReferenceHolder.Instance.damageTextPrefab, 
            transform.position, 
            Quaternion.identity);
        go.GetComponentInChildren<TMP_Text>().text = damage.ToString();
        Health -= damage;
        rb.AddForce(damageDirection * damageForceMultiplier, ForceMode.Impulse);
    }
}
