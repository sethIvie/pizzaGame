using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HUDController : MonoBehaviour
{
    public Text scoreText;
    public Text pauseButtonText;

    public GameObject titleButton;
    public GameObject quitButton;

    public void Start()
    {
        titleButton.SetActive(false);
        quitButton.SetActive(false);
    }

    public void PauseButtonClicked()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            pauseButtonText.text = "Resume";
            titleButton.SetActive(true);
            quitButton.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            pauseButtonText.text = "Pause";
            titleButton.SetActive(false);
            quitButton.SetActive(false);
        }
    }
  
}
