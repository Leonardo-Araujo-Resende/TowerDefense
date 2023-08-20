using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    public int countWayPoints;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private Text playerLifeText;
    [SerializeField] private Text playerCoinText;
    public float playerLife;
    public float playerCoin;
    public static bool isPlaying = false;
    public static bool isPause = false;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject tutorial;
    public float tempoTutorial;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject winGame;
    [SerializeField] private GameObject pauseImage;
    [SerializeField] private GameObject lifeCoin;
    [SerializeField] private GameObject waves;

    
    void Start()
    {
        UpdateCoin();    
        UpdateName();
        UpdateLife();
        isPlaying = false;
        Time.timeScale = 1f;

    }

    void Update()
    {
        Pause();
    }

    public void Damage(float damage){
        playerLife -= (int)damage;
        UpdateLife();
        if(playerLife <= 0){
            Time.timeScale = 0f;
            gameOver.SetActive(true);
        }
    }


    public bool CanBuy(int coin){
        return (coin <= playerCoin);
    }
    public void EarnCoin(int coin){
        playerCoin += coin;
        UpdateCoin();
    }
    private void UpdateLife(){
        playerLifeText.text = "" + playerLife;
    }

    private void UpdateCoin(){
        playerCoinText.text = ""+playerCoin;

    }

    private void UpdateName(){
        int aux2 = 1;
        foreach(Transform aux in wayPoints){
            aux.gameObject.name = "WayPoint " + aux2.ToString();
            aux2 ++;
        }
    }

    public void BuyCoin(int price){
        playerCoin -= price;
        UpdateCoin();
    }

    public void StartGame(){
        menu.SetActive(false);
        Time.timeScale = 1f;
        StartTutorial();
        
    }

    public void StartTutorial(){
        
        lifeCoin.SetActive(true);
        waves.SetActive(true);
        tutorial.SetActive(true);
        
        
    }

    public void ReallyStartGame(){
        isPlaying = true;
        isPause = false;
    }

    public void RestartScene(){
        SceneManager.LoadScene("TowerDefense");
    }

    private void Pause(){
        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1f && isPlaying){
            Time.timeScale = 0f;
            pauseImage.SetActive(true);
            isPause = true;
        }
        else if( Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0f && isPlaying){
            Time.timeScale = 1f;
            pauseImage.SetActive(false);
            isPause = false;
        }
    }

    public void Quit(){
        Application.Quit();
    }

    public void WinMenu(){
        winGame.SetActive(true);
    }

    public Transform[] GetWayPoints(){
        return wayPoints;
    }

    public void EndTutorial(){
        tutorial.SetActive(false);
        ReallyStartGame();
    }
}
