using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyPath : MonoBehaviour
{
  
    public Transform Startpos;
    public float speed;
    public GameObject Enemy;
    public Rigidbody rb;
    

    private void Start()
    {
        Enemy.transform.position = Startpos.position;
        
        rb  =  Enemy.GetComponent<Rigidbody>();
    }
    private void Update()
    {
       if (Enemy.tag == "Horizontal")
        {
            rb.AddForce(Vector2.right * speed * Time.deltaTime, ForceMode.Force);
            
        }
       if (Enemy.tag == "Vertical")
        {
            rb.AddForce(-Vector3.forward * speed * Time.deltaTime, ForceMode.Force);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Rotater")
        {
            rb.velocity = Vector3.zero;
            speed = speed * -1;
            Enemy.transform.Rotate(0, 180, 0);
        }
       
    }
    /*
    public void FixedUpdate()
    {
        if (Enemy.tag == "Vertical")
        {
            if (rb.velocity.z > 0)
            {
                Enemy.transform.Rotate(0,180,0);
            }
            if(rb.velocity.z < 0)
            {
                Enemy.transform.Rotate(0,0,0);
            }
        }
        if (Enemy.tag == "Horizontal")
        {
            if(rb.velocity.x <0)
            {
                Enemy.transform.Rotate(0, 0, 0);
            }
            if(rb.velocity.x > 0)
            {
                Enemy.transform.Rotate(0, 180, 0);
            }
        }
    

    
    }
    */
}
