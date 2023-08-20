using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Tower : MonoBehaviour
{   
    
    [SerializeField] protected GameObject canvas;
    [SerializeField] public GameObject bullet;
    [SerializeField] protected float timeBetweenFire;
    protected float timer = 0;
    protected CircleCollider2D rangeShootingCollider;
    public float shootingRange;
    [SerializeField] protected GameObject shootingRangeRender;
    [SerializeField] protected float damage;
    protected List<EnemyTD> enemyList = new List<EnemyTD>();
    [SerializeField] private int price;
    [SerializeField] private int sellPrice;
    protected GameManager gameManager;
    protected Animator animator;
    protected float DamageDealed;

    public void SuperStart(){
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rangeShootingCollider = GetComponent<CircleCollider2D>();
        UpdateShootingRange();
        AtualizaTransformCanvas();
        animator = GetComponent<Animator>();
    }
    
   //-----------------------------------------------------------------------------------------------------------------------------------------
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "EnemyTD"){
            enemyList.Add(other.GetComponent<EnemyTD>());
            enemyList = enemyList.OrderByDescending(enemy => enemy.index).ToList();
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "EnemyTD"){
            enemyList.Remove(other.GetComponent<EnemyTD>());
            enemyList = enemyList.OrderByDescending(enemy => enemy.index).ToList();
        }
    }


    //-----------------------------------------------------------------------------------------------------------------------------------------

    protected void Rotation(){
        if(enemyList.Count >= 1 && enemyList[0] != null){

            Vector3 rotation = -transform.position + enemyList[0].transform.position;
            float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotZ+90+180);

        }
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------

    protected void UpdateShootingRange(){
        float biggestScale = transform.localScale.x;
        if(biggestScale < transform.localScale.y) biggestScale = transform.localScale.y; // pega o maior x ou y

        rangeShootingCollider.radius = shootingRange / biggestScale; //Atualiza range
        shootingRangeRender.transform.localScale = new Vector3(rangeShootingCollider.radius*2f,rangeShootingCollider.radius*2f,0f);

    }

    
    //-----------------------------------------------------------------------------------------------------------------------------------------

    public int GetPrice(){
        return price;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------

    public void AtivarDesativarCanvas(bool a){
        canvas.SetActive(a);
    }

    public void AtualizaTransformCanvas(){
        if(transform.position.y > 1) canvas.transform.position = new Vector3(canvas.transform.position.x, canvas.transform.position.y - 3.5f, canvas.transform.position.z);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------

    public void Death(){
        gameManager.EarnCoin(sellPrice);
        AtivarDesativarCanvas(false);
        Destroy(GetComponentInParent<Transform>().gameObject);
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------

    protected void PlayAnimation(string anim){
        animator.enabled = true;
        animator.Play(anim);
    }

    protected void StopAnimation(){
        animator.enabled = false;
        
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------

    public void IncreaseDamageDealed(float a){
        DamageDealed += a;
    }

    //-----------------------------------------------------------------------------------------------------------------------------------------

    protected void AtualizaOrdemEnemys(){
        enemyList = enemyList.OrderByDescending(enemy => enemy.index).ToList();
    }

}


