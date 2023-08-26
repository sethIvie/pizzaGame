using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using JetBrains.Annotations;
using UnityEditor.UIElements;

public delegate void TextOutputHandler(string text); // can represent any method with the same signature ie void --- (string)

public class GameSceneController : MonoBehaviour
{
    [Header("Player Settings")]
    [Range(5, 20)]
    public float playerSpeed;
    public GameObject[] hearts = new GameObject[3];
    private int playerHealth = 3;
    public UnityEvent death;
    public UnityEvent mushedup;
    public UnityEvent olivedup;
    public UnityEvent pineappledup;

    [Header("Screen Settings")]
    [Space]

    public Vector3 screenBounds;
    public float secInterval = 2f;

    [Header("Prefab")]
    [Space]
    [SerializeField]
    private EnemyController enemyPrefab; //This allows you to display private variable inside the unity editor (so you can attach them to something there)
    [SerializeField]
    private PowerupController[] powerupPrefabs = new PowerupController[3];

    //public AudioSource song;

    private HUDController hUDController;
    
    
    public static int totalPoints;
    PlayerController player;
    private SceneChanger sceneChanger;
    bool hehe = false;
    // Start is called before the first frame update
    void Start()
    {
        hUDController = FindObjectOfType<HUDController>();
        totalPoints = 0;
        playerSpeed = 10;
        screenBounds = GetScreenBounds();
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnPowerups());
        player = FindObjectOfType<PlayerController>();

        //song = GetComponent<AudioSource>();
        //song.Play();
        
        secInterval = secInterval - ((secInterval/4)* DataIndex.difficulty);
        Debug.Log("sec in" + secInterval);
        
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerController player = FindObjectOfType<PlayerController>(); <- this statement would be innefficient, lets move as much as we can to outside the update method

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.SetColor(Color.red);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            player.SetColor(Color.yellow);
        }
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            hehe = true;
            Debug.Log(";)");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            float horizonPosition = Random.Range(-screenBounds.x + 1, screenBounds.x - 1);
            Vector2 spawnPosition = new Vector2(horizonPosition, screenBounds.y);

            PowerupController powerup = Instantiate(powerupPrefabs[UnityEngine.Random.Range(0, 3)], spawnPosition, Quaternion.identity);

            powerup.PowerupEscaped += PowerupAtBottom;
            powerup.PowerupKilled += PowerupKilled;
        }
    }
    private IEnumerator SpawnEnemies()
    {
     
        WaitForSeconds wait = new WaitForSeconds(secInterval);
        WaitForSeconds speed3 = new WaitForSeconds(secInterval / 4f);
        WaitForSeconds speed2 = new WaitForSeconds(secInterval / 3f);
        WaitForSeconds speed1 = new WaitForSeconds(secInterval / 2f);

        while (true)
        {
            float horizonPosition = Random.Range(-screenBounds.x +1, screenBounds.x-1);
            Vector2 spawnPosition = new Vector2(horizonPosition, screenBounds.y);

            EnemyController enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            enemy.EnemyEscaped += EnemyAtBottom;
            enemy.EnemyKilled += EnemyKilled;

            if (hehe)
            {
                wait = new WaitForSeconds(0f);
            }
            else if (totalPoints >= 1000)
            {
                wait = speed3;
                Debug.Log(secInterval / 4f);
            } else if (totalPoints >= 500)
            {
                wait = speed2;
                Debug.Log(secInterval / 3f);
            }
            else if (totalPoints >= 100)
            {
                wait = speed1;
                Debug.Log(secInterval / 2f);
            }
            yield return wait;
        }

    }
    private IEnumerator SpawnPowerups()
    {
        WaitForSeconds wait = new WaitForSeconds(seconds: secInterval * 10 * Random.Range(1,3));
        yield return wait;
        while (true)
        {
            float horizonPosition = Random.Range(-screenBounds.x + 1, screenBounds.x - 1);
            Vector2 spawnPosition = new Vector2(horizonPosition, screenBounds.y);

            PowerupController powerup = Instantiate(powerupPrefabs[UnityEngine.Random.Range(0, 3)], spawnPosition, Quaternion.identity);

            powerup.PowerupEscaped += PowerupAtBottom;
            powerup.PowerupKilled += PowerupKilled;

            yield return wait;
        }
    }

    private void EnemyKilled(int pointValue)
    {
        totalPoints += pointValue;
        hUDController.scoreText.text = totalPoints.ToString();
    }
    private void PowerupKilled(int pointValue, int powerupID)
    {   
        switch(powerupID)
        {
            case 0:
                mushedup.Invoke();
                break;
            case 1:
                olivedup.Invoke();
                break;
            case 2:
                pineappledup.Invoke();
                break;

        }
        
    }

    private void EnemyAtBottom(EnemyController enemy)
    {
        Destroy(enemy.gameObject);
        Hurt();
        Debug.Log("Enemy Escaped");
    }
    private void PowerupAtBottom(PowerupController powerup)
    {
        Destroy(powerup.gameObject);
        Debug.Log("Powerup Escaped");
    }

    private Vector3 GetScreenBounds()
    {
        Camera mainCamera = Camera.main;
        Vector3 screenVector = new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z);

        return mainCamera.ScreenToWorldPoint(screenVector);
    }

    public void KillObject(IKillable killable)
    {
        killable.Kill();
    }

    public void OutputText(string output)
    {
        Debug.LogFormat("{0} output by GameSceneController", output);
    }
    public void Hurt()
    {
        if (!(playerHealth < 0)) { 

        playerHealth -= 1;
        //Debug.Log(playerHealth);
        //hearts[playerHealth].GetComponent<Renderer>().enabled = false;
        hearts[playerHealth].GetComponent<Image>().color = Color.black;
        //hearts[playerHealth].GetComponent<MeshRenderer>().enabled = false;
        if (playerHealth <= 0)
        {
            //song.Pause();
            Debug.Log("scenechange?");
            death.Invoke();
        }
        }
    }
    public int GetPoints()
    {
        return totalPoints;
    }
}
