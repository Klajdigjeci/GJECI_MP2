using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class GreenUpBehavior : MonoBehaviour 
{
   public Text mGameControlText = null;
   private Rigidbody2D rb;
   public float arrowDamage = 100f;
   public Text mEnemyCountText = null;
   
   public float mPlanesTouched = 0;
   public float speed = 20f;
   public float currentSpeed;
   public float forwardAcceleration = 0.0000000000000005f;
   public float backwardAcceleration = -0.0000000000000005f;
   public float maxSpeed = 100f;
   public float mHeroRotateSpeed = 90f / 2f;

   public float eggRate = 0.2f;
   public int sortingOrder = 0;
   

   private float eggTimeStamp;
   private bool outOfScreen = false;

   private GameController mGameGameController = null;
   public bool mFollowMousePosition = true;

   void Start()
   {
      currentSpeed = speed;
      rb = GetComponent<Rigidbody2D>();
      rb.freezeRotation = true;
      rb.velocity = transform.up * speed;
      mGameGameController = FindObjectOfType <GameController>();
   }

   void Update()
   {
      if (Input.GetKeyDown(KeyCode.M))
      {
         mFollowMousePosition = !mFollowMousePosition;
         //UpdateGameControl();
      }

   

      Vector3 pos = transform.position;

      if (mFollowMousePosition)
      {
         pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         pos.z = 0f;
      }

      else
      {
         
         if(Input.GetKey(KeyCode.W))
         {
            currentSpeed += 0.5f;
         } 
         if(Input.GetKey(KeyCode.S))
         {
            currentSpeed -= 0.5f;
         }
         
        
         if (Input.GetKey(KeyCode.D))
         {
            Debug.Log("velocity: " + rb.velocity);
            transform.Rotate(transform.forward, -mHeroRotateSpeed * Time.smoothDeltaTime);
         }

         if (Input.GetKey(KeyCode.A))
         {
            transform.Rotate(transform.forward, mHeroRotateSpeed * Time.smoothDeltaTime);
         }
         rb.velocity = transform.up * currentSpeed;
      }
      transform.position = pos;
      if (outOfScreen == false && Time.time >= eggTimeStamp && Input.GetKey(KeyCode.Space))
      {
         GameObject e = Instantiate(Resources.Load("Prefabs/Egg") as GameObject);

         e.layer = LayerMask.NameToLayer("Default");
         e.transform.localPosition = transform.localPosition;
         e.transform.rotation = transform.rotation;

         e.GetComponent<Renderer> ().material.color = Color.black;
          Debug.Log("Spawn Eggs:" + e.transform.localPosition);
          eggTimeStamp = Time.time + eggRate;
          mGameGameController.IncreaseNumEggs();
      }
      
   }
   private void OnCollisionEnter2D(Collision2D collision)
   {
      GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
      if(enemy.tag == "Enemy")
      {
         EnemyBehavior enemyKill = collision.gameObject.GetComponent<EnemyBehavior>();
         Debug.Log("Here x Plane: OnCollisionEnter2D");
         mPlanesTouched++;
         mEnemyCountText.text = "Planes touched " + mPlanesTouched;
         enemyKill.TakeDamage(arrowDamage);
         
      }
      
   }

   void OnBecameInvisible()
   {
      outOfScreen = true;
      Debug.Log("Out of screen " + outOfScreen);
     
   }

   void OnBecameVisible()
   {
      outOfScreen = false;
      Debug.Log("Out of screen " + outOfScreen);
     
   }
   private void OnCollisionStay2D(Collision2D collision)
   {
      Debug.Log("Plane On CollisionStay");
   }

   // private void UpdateGameControl()
   // {
   //    if(mFollowMousePosition == true)
   //    {
   //       mGameControlText.text = "Hero Control Mode: Follow Mouse Position";
   //    }
   //    else
   //    {
   //       mGameControlText.text = "Hero Control Mode: Keyboard Mode";
   //    }
   // }

}


