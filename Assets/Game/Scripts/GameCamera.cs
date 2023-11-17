using DG.Tweening;
using UnityEngine;

public class GameCamera : Singleton<GameCamera>
{
    public Transform directTransform;
    public Transform nextSpawn, backSpawn;

    public void Move(Vector3 position, float duration)
    {
        directTransform.position = position;
        transform.DOMove(position, duration);
    }
}