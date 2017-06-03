using UnityEngine;
using System.Collections;

public class Enemy1 : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    public int speed = -3;
    public GameObject explosion;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
     //   Debug.Log("Enemy:Update" + rigidbody2D.velocity);
     //   Debug.Log("Time.deltaTime" + Time.deltaTime);
        rigidbody2D.velocity = new Vector2( speed, rigidbody2D.velocity.y);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Enemy:col.tag=" + col.tag);
        if (col.tag == "Bullet")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }

}