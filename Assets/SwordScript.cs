using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public Player player;
    private void OnCollisionEnter(Collision hit)
    {
        Debug.Log("Yooo");
        if (player.isFighting && hit.collider.tag == "Enemy")
        {
            var enemy = hit.collider.GetComponentInParent<Enemy>();
            enemy.Damage(50, -hit.GetContact(0).normal);
        }
    }
}
