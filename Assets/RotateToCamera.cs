using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private Transform _transform;

    private void Awake()
    {
        _transform = GameCamera.Instance.transform;
    }

    void LateUpdate()
    {
        transform.LookAt(_transform);
    }
}
