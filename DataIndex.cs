using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class DataIndex : MonoBehaviour
{
    //public Text difficultyButtonText;
    public TextMeshProUGUI difficultyButtonText;
    public TextMeshProUGUI difficultyText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public int choice;
    public static float difficulty;
    private string[] difficultyTexts = { "Easy", "Medium", "Hard", "just no" };

    public int currentHighscore;
    void Start()
    {
        LoadGame();
        if (choice == 1)
        {
            DisplayScore();
        } else if (choice == 0)
        {
            DifficultyNameChange();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        //dropDown = dropDownObject.GetComponent<Dropdown>();
        //difficulty = dropDown.value;
        //Debug.Log(difficulty);
    }
    public void DifficultyChange()
    {
        if(difficulty >= 3)
        {
            difficulty = 0;
        } else
        {
            difficulty += 1;
        }
    }
    public void DifficultyNameChange()
    {
        int i = (int)difficulty;
        difficultyButtonText.text = difficultyTexts[i];
    }
    void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter(); //used to serialize and deserialize data
        FileStream file = File.Create(Application.persistentDataPath + "/MySaveData.dat"); //The persistent data path is C:\Users\[user]\AppData\LocalLow\[company name].
        SaveData data = new SaveData();
        data.highScore[(int)difficulty] = currentHighscore;
        bf.Serialize(file, data);
        file.Close();
        Debug.Log("Game data Saved!");
    }
    void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath
                       + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file =
                       File.Open(Application.persistentDataPath
                       + "/MySaveData.dat", FileMode.Open); //Opens save data
            SaveData data = (SaveData)bf.Deserialize(file); //Deserializes data
            file.Close();
            currentHighscore = data.highScore[(int)difficulty];
            Debug.Log("Game data loaded!");
        }
        else
            Debug.LogError("There is no save data!");
    }
    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
                      + "/MySaveData.dat"))
        {
            File.Delete(Application.persistentDataPath
                              + "/MySaveData.dat");
            currentHighscore = 0;
            Debug.Log("Data reset complete!");
        }
        else
            Debug.LogError("No save data to delete.");
    }
    public void ScoreSet ()
    {
        if (GameSceneController.totalPoints > currentHighscore)
        {
            currentHighscore = GameSceneController.totalPoints;
        }
        SaveGame();
        
    }
    public void DisplayScore()
    {
        LoadGame();
        int i = (int)difficulty;
        scoreText.text = "Score: " + GameSceneController.totalPoints;
        highscoreText.text = "Highscore: " + currentHighscore;
        difficultyText.text = difficultyTexts[i] + " mode";
        

    }

}
[Serializable]

class SaveData
{
    public int savedInt;
    public float savedFloat;
    public bool savedBool;
    public int[] highScore = new int[4];

}



