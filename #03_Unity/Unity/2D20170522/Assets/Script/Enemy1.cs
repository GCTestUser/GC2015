using UnityEngine;
using System.Collections;
public class Enemy1 : MonoBehaviour
{
    new Rigidbody2D rigidbody2D;
    public int speed = -3;

    public GameObject explosion;

    public int attackPoint = 10;
    public Life life;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rigidbody2D.velocity = new Vector2(speed, rigidbody2D.velocity.y);
        Debug.Log("EnemyPos="+transform.position);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Bullet")
        {
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        //UnityChanとぶつかった時
        if (col.gameObject.tag == "UnityChan")
        {
            //LifeScriptのLifeDownメソッドを実行
            life.LifeDown(attackPoint);
        }
    }
}