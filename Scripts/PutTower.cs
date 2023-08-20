using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PutTower : MonoBehaviour
{
    private int towerToPut;
    public GameObject[] towersGameObject;
    private Tower[] tower;
    public GameObject towerImage;
    public GameManager gameManager;
    public GameObject shootingRangeRender;
    public CircleCollider2D colider;   
    public Color canPut;
    public Color cannotPut;
    private int qntColisoes;

    void Start()
    {
        tower = new Tower[towersGameObject.Length];
        for (int i = 0; i < towersGameObject.Length; i++) {tower[i] = towersGameObject[i].GetComponentInChildren<Tower>();}

        towerToPut = -1;
    }

    // Update is called once per frame
    void Update()
    {
        AtualizaTorre();
        PlaceTower();
        if (Input.GetMouseButtonDown(1)) DesligaImagemTorre();


    }

    private void AtualizaTorre(){
        if (Input.GetKeyDown(KeyCode.Alpha1))AtualizaTorreColocar(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))AtualizaTorreColocar(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))AtualizaTorreColocar(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))AtualizaTorreColocar(3);
    }

    private void AtualizaTorreColocar(int towerNum){

        towerImage.SetActive(true);
        towerImage.GetComponent<SpriteRenderer>().sprite = tower[towerNum].GetComponent<SpriteRenderer>().sprite; //Atualiza sprite torre
        towerImage.transform.localScale = new Vector3(tower[towerNum].transform.localScale.x,tower[towerNum].transform.localScale.y,towerNum); //Atualiza tamanho torre
        transform.localScale = new Vector3(tower[towerNum].transform.localScale.x,tower[towerNum].transform.localScale.y,towerNum); //Atualiza tamanho desse gameObject
        shootingRangeRender.transform.localScale = new Vector3(tower[towerNum].shootingRange*2/tower[towerNum].transform.localScale.x,tower[towerNum].shootingRange*2/tower[towerNum].transform.localScale.x,0); // Tem q dividir pelo localSlace x porque o tamanho varia de acordo com o tamanho do gameobject pai
        towerToPut = towerNum;


        colider.radius = tower[towerNum].transform.Find("TowerPlacement").GetComponent<CircleCollider2D>().radius;
        colider.enabled = false;
        colider.enabled = true;
    }

    private void PlaceTower(){

        if (Input.GetMouseButtonDown(0) && towerToPut != -1 && gameManager.CanBuy(tower[towerToPut].GetPrice()) && !GameManager.isPause && qntColisoes == 0){
            gameManager.BuyCoin(tower[towerToPut].GetPrice());
            Instantiate(towersGameObject[towerToPut], Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10), Quaternion.identity);
            DesligaImagemTorre();
        }

        
    }

    private void DesligaImagemTorre(){
        towerImage.SetActive(false);
        towerToPut = -1;
        colider.enabled = false;

    }

    private void OnTriggerEnter2D(Collider2D other){
        if((other.tag == "TowerTDplacement" || other.tag == "Path")){
            qntColisoes ++;
            if(qntColisoes != 0) shootingRangeRender.GetComponent<SpriteRenderer>().color = cannotPut;  
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if((other.tag == "TowerTDplacement" || other.tag == "Path")){
            qntColisoes --;
            if(qntColisoes == 0) shootingRangeRender.GetComponent<SpriteRenderer>().color = canPut;  
        }
    }

    public void AtualizaTorreColocar0(){
        AtualizaTorreColocar(0);
    }

    public void AtualizaTorreColocar1(){
        AtualizaTorreColocar(1);
    }

    public void AtualizaTorreColocar2(){
        AtualizaTorreColocar(2);
    }

    public void AtualizaTorreColocar3(){
        AtualizaTorreColocar(3);
    }

}
