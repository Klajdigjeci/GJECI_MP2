using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EggBehavior : MonoBehaviour
{
    public float damage = 25f;
    public float speed = 40f;

    private GameController gmController = null;

    void OnBecameInvisible()
    {
        Destroy(gameObject);
        gmController.EggDestroyed();
    }

    void Start()
    {   
        gmController = FindObjectOfType <GameController>();
    }

    void Update()
    {
        transform.position += transform.up * (speed * Time.smoothDeltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        if(collision.gameObject.tag == "Enemy")
        {
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            Debug.Log("Here x Plane: OnTriggerEnter2D");
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }    

}
