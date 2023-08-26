using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Shape //Deriving from shape instead of monobehaviour this will allow us to use methods defined in the shape script 
{
    public ProjectileController projectilePrefab;
    public ProjectileController bigProjectilePrefab;
    public ProjectileController mushroomPrefab;
    public ProjectileController olivePrefab;
    public ProjectileController pineapplePrefab;
    public Sprite[] sprites = new Sprite[4];
    [Range(1, 5)]
    public int chargeTime;
    private Stopwatch timer;
    private bool blink = false;
    private int powerup = 0;
    private int powerupUses = 0;

    // All variables used inside of a class must either be defined within the class itself 
    // or they can be from the class it is deriving from, but if the class contains a start method, the computer will not look at the start definitions in the parent class

    /*
    private void Awake()
    {
        Debug.Log(gameSceneController.screenBounds); - //results in error because Awake is initialized before start, and start is where the base is initialized
    }
    */

    protected override void Start()
    {
        base.Start();

        StartCoroutine(colorBlink());
        timer = new Stopwatch();
        SetColor(Color.yellow);
    }
    void Update()
    {
        MovePlayer();

        if (Input.GetKeyDown(KeyCode.Space)) { 
            timer.Start();
        }
        if(Input.GetKey(KeyCode.Space))
        {
            timer.Stop();
            if(timer.Elapsed.Seconds >= chargeTime)
            {
                 if (!blink)
                {
                    blink = true;
                    StartCoroutine(colorBlink());
                }
            }
            timer.Start();
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            timer.Stop();
            blink = false;
            if (timer.Elapsed.Seconds >= chargeTime)
            {
                FireBigProjectile();
            }
            else
            {
                FireProjectile();
            }
            timer = new Stopwatch();
        }
    }

    private void MovePlayer()
    {
        float horizonMovement = Input.GetAxis("Horizontal"); //by default Input.GetAxis is mapped to the arrow keys (-to+ with left-right)
        if (Mathf.Abs(horizonMovement) > Mathf.Epsilon) //tests if either left or right is pressed - abs(absolute value) because this position can be - or + then test against epsilon for any wierdness 0 cant get than
        {
            horizonMovement = horizonMovement * Time.deltaTime/*keeps movement smooth between frames*/ * gameSceneController.playerSpeed;
            horizonMovement += transform.position.x; // adds that value to previos position

            float right = gameSceneController.screenBounds.x - halfWidth;
            float left = -right;

            float limit =
                Mathf.Clamp(horizonMovement, left, right); //clamp traps horzion movement between a minimium and max value

            transform.position = new Vector2(limit, transform.position.y); //than tells player to move there
        }
    }

    private void FireProjectile()
    {
        Vector2 spawnPosition = transform.position;
        Vector2[] direction = { (Vector2.up + Vector2.left).normalized, Vector2.up, (Vector2.up + Vector2.right).normalized};

        switch (powerup)
        {
            case 0:
                ProjectileController projectile =
                    Instantiate(projectilePrefab, spawnPosition, Quaternion.identity); //creates an instant of an object based on a prefab, the Quaternion sets the rotation of the new object

                projectile.projectileSpeed = 5;
                projectile.projectileDirection = Vector2.up;
                break;
            case 1:
                ProjectileController[] mushrooms = new ProjectileController[3];
                
                for(int i = 0; i<mushrooms.Length;i++)
                {
                    mushrooms[i] = Instantiate(mushroomPrefab, spawnPosition, Quaternion.identity);
                    mushrooms[i].projectileSpeed = 5;
                    mushrooms[i].projectileDirection = direction[i];
                }
                powerupUses--;
                if (powerupUses == 0)
                {
                    removePowerup();
                }
                break;
            case 2:
                ProjectileController oliveShot = Instantiate(olivePrefab, spawnPosition, Quaternion.identity);
                oliveShot.projectileSpeed = 5;
                oliveShot.projectileDirection= Vector2.up;
                powerupUses--;
                if (powerupUses == 0)
                {
                    removePowerup();
                }
                break;
            case 3:
                ProjectileController pineappleShot = Instantiate(pineapplePrefab, spawnPosition, Quaternion.identity);
                pineappleShot.projectileSpeed = 0;
                pineappleShot.projectileDirection= Vector2.up;
                powerupUses--;
                if (powerupUses == 0)
                {
                    removePowerup();
                }
                break;
        }
        
    }
    private void FireBigProjectile()
    {
        Vector2 spawnPosition = transform.position;

        ProjectileController projectile =
            Instantiate(bigProjectilePrefab, spawnPosition, Quaternion.identity);

        projectile.projectileSpeed = 2;
        projectile.projectileDirection = Vector2.up;
    }
    public void mushroomPowerup()
    {
        powerup = 1;
        powerupUses = 3;
        spriteRenderer.sprite = sprites[1];
    }
    public void olivePowerup()
    {
        powerup = 2;
        powerupUses = 5;
        spriteRenderer.sprite = sprites[2];
    }
    public void pineapplePowerup()
    {
        powerup = 3;
        powerupUses = 1;
        spriteRenderer.sprite = sprites[3];
    }
    public void removePowerup()
    {
        powerup = 0;
        spriteRenderer.sprite = sprites[0];
    }
    private IEnumerator colorBlink()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
        bool red = false;
        while (blink)
        {
           
                if (red)
                {
                    SetColor(Color.yellow);
                    red = false;
                }
                else
                {
                    SetColor(Color.red);
                    red = true;
                }
            
            yield return wait;
        }
    }
}
