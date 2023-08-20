using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyTD : MonoBehaviour
{
    private Transform[] wayPoint;
    public int index = 0;
    public float speed;
    private float life;
    public int maxLife;
    public Image lifeBar;
    private GameManager gameManager;
    private EnemySpawner enemySpawner;
    public int coinReward;
    public float rotationSpeed;
    private bool isDying = false;
    private bool isSlow = false;
    public bool isBoss;
    public bool isParado;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Color colorInSlow;
     
    // Start is called before the first frame update
    void Start()
    {   
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpawner = GameObject.Find("GameManager").GetComponent<EnemySpawner>();
        

        int aux = enemySpawner.GetCurrentWave();
        maxLife += aux;
        life = maxLife;
        
        wayPoint = gameManager.GetWayPoints();
    }

    // Update is called once per frame
    void Update()
    {

        if(index <= wayPoint.Length-1 && !isParado)transform.position = Vector3.MoveTowards(transform.position, wayPoint[index].position, speed * Time.deltaTime);

        if(index <= wayPoint.Length-1 && transform.position ==  wayPoint[index].position){
            if(index+1 < wayPoint.Length)index = (index + 1);
        }

        if(transform.position ==  wayPoint[wayPoint.Length-1].position){
            
            gameManager.Damage(life);
            gameManager.EarnCoin(coinReward);
            Death();

        }



    }

    public void Damage(float damage){
        
        life -= damage;
        UpdateLifeBar();
        if(life <= 0 && !isDying){
            isDying = true;
            gameManager.EarnCoin(coinReward);
            Death();
        }
    }

    private void UpdateLifeBar(){
        lifeBar.fillAmount = life*1f / maxLife*1f;
    }
    

    public Transform GetWayPoint(int index){
        return wayPoint[index];
    }

    public void Slow(float porcentagemSlow, float tempoSlow){
        if(!isSlow)StartCoroutine(SlowRoutine(porcentagemSlow, tempoSlow));
    }
    private IEnumerator SlowRoutine(float porcentagemSlow, float tempoSlow){

        isSlow = true;
        Color saveColor = sprite.color;
        sprite.color = colorInSlow;
        float aux = speed;
        speed = speed * (1 - porcentagemSlow);

        yield return new WaitForSeconds(tempoSlow);
        
        speed = aux;
        isSlow = false;
        sprite.color = saveColor;

    }

    private void Death(){
        if(isBoss) gameManager.WinMenu();
        Destroy(this.gameObject);
    }




}
