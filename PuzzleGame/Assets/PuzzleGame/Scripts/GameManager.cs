using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private GameObject endGameHUD;

    public static float CountDown;
    
    private static bool GameEnded;

    public static int PiecePlaced;

    private void Start()
    {
        GameEnded = false;
        CountDown = 60f;
        PiecePlaced = 0;
    }

    private void Update() 
    {
        if(!GameEnded)
        {
            CountDown -= Time.deltaTime;
        }

        countdownText.text = "Countdown :" + CountDown.ToString("0");

        if(PiecePlaced >= 9 || CountDown <= 0)
        {
            EndGame();
        }
    }

    public void EndGame()
    {
        endGameHUD.SetActive(true);
        GameEnded = true;
    }

    public void Restart()
    {
        endGameHUD.SetActive(false);
        GameEnded = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

   
}
