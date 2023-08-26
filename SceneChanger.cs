using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void StartButtonClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void AchevementsButtonClicked()
    {
        SceneManager.LoadScene("Achevements");
    }
    public void DeathScene()
    {
        SceneManager.LoadScene("Death");
    }
    public void TitleScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
    } 

}
