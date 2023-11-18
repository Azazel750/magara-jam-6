using System;
using DG.Tweening;
using UnityEngine;

public class GameCamera : Singleton<GameCamera>
{
    public Transform target;

    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - target.position;
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
        transform.position = target.position + offset;
    }

    private void LateUpdate()
    {
        
    }
}