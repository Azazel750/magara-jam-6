using System;
using AOC;
using Enemy_States;
using Pathfinding;
using UnityEngine;

public class Enemy : Alive
{
    public Rigidbody rb;
    
    [HideInInspector] public Vector3 direction;
    private Transform target;
    IAstarAI ai;
    private StateManager _stateManager;

    private void Start()
    {
        _stateManager = new StateManager(this);
        _stateManager.Begin<IdleState>();
    }

    void OnEnable () {
        ai = GetComponent<IAstarAI>();
        target = GameManager.GetClosest<Human>(this).transform;
        
        if (ai != null) ai.onSearchPath += Update;
    }

    void OnDisable () {
        
    }

    /// <summary>Updates the AI's destination every frame</summary>
    void Update ()
    {
        if(direction.magnitude > 0.1f) rb.velocity = direction.normalized;
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
}
