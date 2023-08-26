using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public delegate void PowerupEscapedHandler(PowerupController powerup);
public class PowerupController : Shape, IKillable
{
    public event PowerupEscapedHandler PowerupEscaped;
    public int powerUpID;
    
    public event Action<int, int> PowerupKilled; //works idetically to the method above with the delget thing but this one is self contained
   
    //public Sprite[] sprites = new Sprite[3];


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Name = "Powerup";
        //SwitchSprite(sprites[UnityEngine.Random.Range(0, 3)]);
        
    
    }

    // Update is called once per frame
    void Update()
    {
        MovePowerup();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PowerupKilled != null)
        {
            PowerupKilled(10, powerUpID);
            if (collision.gameObject.tag != "Indistructable")
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

    private void MovePowerup()
    {
        transform.Translate(Vector2.down * Time.deltaTime, Space.World);

        float bottom = transform.position.y - halfHeight;

        if(bottom <= -gameSceneController.screenBounds.y)
        {
            //outputHandler("Enemy ar bottom");
            //Destroy(gameObject);
            //gameSceneController.KillObject(this);
            PowerupEscaped?.Invoke(this);
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
        Debug.LogFormat("{0} output by PowerupController", output);
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
