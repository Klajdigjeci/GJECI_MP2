using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float health = 100f;
    private GameController gmController = null;

    void Start()
    {
        gmController = FindObjectOfType <GameController>();
        gameObject.tag = "Enemy";
    }

    void Update()
    {
        
    }
    private void UpdateColor(float amount)
    {
        float decreaseAmount = amount / 100f;
        SpriteRenderer sprit = GetComponent<SpriteRenderer>();
        
        Color col = sprit.color;
        float delta = decreaseAmount;
        col.r -= delta;
        col.a -= delta;
        sprit.color = col;
        Debug.Log("Plane: Color = " + col);

        if (col.a <= 0.0f)
        {
            Sprite tex = Resources.Load<Sprite>("Textures/Egg"); 
            sprit.sprite = tex;
            sprit.color = Color.white;
        }
    }
    
    public void TakeDamage(float amount)
    {
         
        health -= amount;
        Debug.Log("Enemy Health: " + health);
         
        if (health <= 0)
        {
            Die();
        }
        else
        {
            UpdateColor(amount);
        }
    }
    void Die()
    {
        Destroy(gameObject);
        gmController.EnemyDestroyed();
    }
     
}
