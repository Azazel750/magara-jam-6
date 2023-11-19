using System;
using System.Collections;
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
    public Transform target;
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
    
    public Animator animator;
    private void Die()
    {
        StartCoroutine(DieCoroutine());
    }

    private IEnumerator DieCoroutine()
    {
        animator.Play("Death");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    void OnEnable () 
    {
        ai = GetComponent<IAstarAI>();
        if(!target) target = GameManager.GetClosest<Human>(this).transform;
        
        if (ai != null) ai.onSearchPath += FixedUpdate;
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
    void FixedUpdate ()
    {
        if(direction.magnitude > 0.1f) rb.velocity = direction.normalized * speed;
        direction = Vector3.zero;
    }

    public void MakeForward(Vector3 currentWaypointVector)
    {
        direction = currentWaypointVector;
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
