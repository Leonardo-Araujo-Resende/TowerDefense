using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemySpawner : MonoBehaviour
{

    public Transform spawnPoint;
    public float timeBetweenSpawn;
    public GameObject[] enemyTD;
    public GameObject boss;
    private float timer;
    private int currentWave;
    public int[] enemyPerWave;
    public Text waveText;
    private bool canSpawn = false;
    public float timeBetweenWave;
    private int enemyCount;
    public GameManager gameManager;
    public WaveAnim waveAnim;
    private int lastEnemy;
    private bool bossSpawned;

    // Start is called before the first frame update
    void Start()
    {
        currentWave = 1;
        canSpawn = false;
        enemyCount = 0;
        timer = timeBetweenWave;
    }

    // Update is called once per frame
    void Update()
    {
        if(bossSpawned == false && GameManager.isPlaying == true)EnemySpawn();

    }

    private void EnemySpawn(){
        int aux;
        if(currentWave > enemyPerWave.Length){
            StartCoroutine(SpawnBoss());
            bossSpawned = true;
        }

        timer += Time.deltaTime;
        
        if(timer > timeBetweenWave) canSpawn = true;

        if(timer > timeBetweenSpawn && canSpawn == true){
            timer = 0;
            enemyCount ++;
            if(enemyCount == 1){
                waveAnim.Anim(currentWave);
                UpdateWaveText();

            }
            //Nao spawnar msm inimigo q o anterior
            while(true){
                aux = Random.Range(0, enemyTD.Length);
                if(aux != lastEnemy){
                    lastEnemy = aux;
                    break;
                }
            }
            Instantiate(enemyTD[aux], spawnPoint.position, Quaternion.identity);
        }

        if(currentWave-1 <= enemyPerWave.Length && currentWave-1 < enemyPerWave.Length && enemyCount == enemyPerWave[currentWave-1]){
            currentWave ++;
            enemyCount = 0;
            canSpawn = false;
        }

    }




    private void UpdateWaveText(){
        waveText.text = "Wave " + currentWave;
    }

    private IEnumerator SpawnBoss(){
        yield return new WaitForSeconds(5);
        Instantiate(boss, spawnPoint.position, Quaternion.identity);
    }

    public int GetCurrentWave(){
        return currentWave;
    }
}
