using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void EnemyEscapedHandler(EnemyController enemy);
public class EnemyController : Shape, IKillable
{
    public event EnemyEscapedHandler EnemyEscaped;
    public GameObject BigBullet;
    public event Action<int> EnemyKilled; //works idetically to the method above with the delget thing but this one is self contained
   
    public Sprite[] sprites = new Sprite[3];


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Name = "Enemy";
        SwitchSprite(sprites[UnityEngine.Random.Range(0, 3)]);
        
    
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (EnemyKilled != null)
        {
            EnemyKilled(10);
            if(collision.gameObject.tag == "olive")
            {
                collision.gameObject.GetComponent<OliveController>().HitClone();
            }
            if (collision.gameObject.tag != "Indistructable")
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void MoveEnemy()
    {
        transform.Translate(Vector2.down * Time.deltaTime, Space.World);

        float bottom = transform.position.y - halfHeight;

        if(bottom <= -gameSceneController.screenBounds.y)
        {
            //outputHandler("Enemy ar bottom");
            //Destroy(gameObject);
            //gameSceneController.KillObject(this);
            EnemyEscaped?.Invoke(this);
            /* ^ is a simplification of 
             * if (EnemyEscaped != nell)
             * {
             * EnemyEscaped(this);
             * }
             */

        }
    }

    private void InternalOutputText(string output)
    {
        Debug.LogFormat("{0} output by EnemyController", output);
    }
    private void SwitchSprite(Sprite newSprite)
    {
        spriteRenderer.sprite = newSprite;
    }
    public void Kill()
    {
        Destroy(gameObject);
    }

    public string GetName()
    {
        return Name;
    }
}
