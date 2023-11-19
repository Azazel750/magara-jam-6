using System;
using System.Collections;
using AOC;
using UnityEngine;

public class GameCamera : Singleton<GameCamera>
{
    public Transform target;
    public StateManager stateManager;
    public Vector3 offset;
    
    
    public Transform dynamicTarget;
    public Vector3 dynamicTargetToPosition;
    

    private void Awake()
    {
        stateManager = new StateManager(this);
        offset = transform.position - target.position;
        stateManager.Begin<CameraMainState>();
        stateManager.NextState<CameraFollowState>();
    }

    /*public Transform directTransform;
    public Transform nextSpawn, backSpawn;*/

    public void Move(Vector3 position, float duration)
    {
        /*directTransform.position = position;
        transform.DOMove(position, duration);*/
    }

    private void FixedUpdate()
    {
        
    }

    private void LateUpdate()
    {
        
    }
}

public class CameraMainState : State
{
    public override void Awake()
    {
        
    }

    public override IEnumerator Start()
    {
        yield break;
    }

    public override void Update()
    {
        if (agent is GameCamera camera)
        {
            transform.position = camera.target.position + camera.offset;
        }
        
    }

    public override void Stop()
    {
        
    }
}

public class CameraFollowState : State
{
    public override void Awake()
    {
        
    }

    public override IEnumerator Start()
    {
        if (agent is GameCamera camera)
        {
            yield return new WaitUntil(() =>
            {
                transform.position = camera.dynamicTarget.position + camera.offset;
                return Vector3.Distance(camera.dynamicTargetToPosition, camera.dynamicTarget.position) < 3;
            });
            NextState<CameraMainState>();
        }

        yield break;
    }

    public override void Update()
    {
        
    }

    public override void Stop()
    {
        
    }
}