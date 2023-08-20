using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseCollider : MonoBehaviour
{

    private OneTowerPerPlacement towerColided;
    public Text damageText;
    public Text upDamageText;
    public Text rangeText;
    public Text upRangeText;
    public Text timeFireText;
    public Text upTimeFireText;
    public GameManager gameManager;
    public Text custoText;
    private bool inColision;


    void Update()
    {
        Position();
        if(Input.GetMouseButtonDown(0))ShootingRange();
        if(Input.GetMouseButtonDown(1)) ZeroCanvas();
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "TowerTDplacement"){
            inColision = true;
            towerColided = other.GetComponent<OneTowerPerPlacement>();
        }
    }
    

    private void OnTriggerExit2D(Collider2D other){
        if (other.tag == "TowerTDplacement"){
            inColision = false;
            other.GetComponent<OneTowerPerPlacement>().DesativarShootingRangeRender();
        }
    }

    void ZeroCanvas(){
        // if(towerColided != null) towerColided.GetComponent<OneTowerPerPlacement>().DesativarShootingRangeRender();
        // towerColided.GetComponentInParent<Tower>().AtivarDesativarCanvas(false);
        // towerColided = null;
        
    }

    void ShootingRange(){
        if(towerColided != null && inColision){
            towerColided.AtivarShootingRangeRender();
            towerColided.GetComponentInParent<Tower>().AtivarDesativarCanvas(true);
        }
    }

    void Position(){
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);
    }

    
}
