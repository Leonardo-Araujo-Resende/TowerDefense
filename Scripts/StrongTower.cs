using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StrongTower : Tower
{

    private int level = 0;
    [Header("UPGRADES")]
    [SerializeField] private int[] precoUpgrade;
    [SerializeField] private float[] upgradeDano;
    [SerializeField] private float[] upgradeRange;
    [SerializeField] private float[] upgradeCadencia;

    [Header("CANVAS")]
    [SerializeField] private Text currentDamage;
    [SerializeField] private Text upgradeDamageText;
    [SerializeField] private Text currentRange;
    [SerializeField] private Text upgradeRangeText;
    [SerializeField] private Text currentCadencia;
    [SerializeField] private Text upgradeCadenciaText;
    [SerializeField] private Text precoUpgradeText;
    [SerializeField] private Text danoDado;

    
    void Start(){
        SuperStart();
        AtualizaCanvas();
    }


    void Update()
    {
        Animation();
        Rotation();
        Shooting();
        if(Input.GetMouseButtonDown(1)) AtivarDesativarCanvas(false);
        AtualizaDanoDado();
    }

    private void Shooting(){
        timer += Time.deltaTime;

        if(enemyList.Count >= 1 && timer > timeBetweenFire ){
            timer = 0;
            GameObject aux = Instantiate(bullet, transform.position, Quaternion.identity);
            BombStrongTower aux2 = aux.GetComponent<BombStrongTower>();
            aux2.damage = damage;
            aux2.SetTowerParent(GetComponent<Tower>());
            
            if(enemyList[0] != null) aux2.SetExplosionPosition(enemyList[0].transform.position);
            if(enemyList.Count >= 2) AtualizaOrdemEnemys();
            
        }
    }

    public void Upgrade(){

        if(level < precoUpgrade.Length && gameManager.CanBuy(precoUpgrade[level])){
            
            gameManager.BuyCoin(precoUpgrade[level]);

            damage += upgradeDano[level];
            shootingRange += upgradeRange[level];
            timeBetweenFire -= upgradeCadencia[level];

            level ++;

        }

        AtualizaCanvas();
        UpdateShootingRange();
    }

    private void AtualizaCanvas(){

        if(level < precoUpgrade.Length){ // Se ainda n tiver no nivel máximo

            currentDamage.text = damage.ToString();
            upgradeDamageText.text = "+" + upgradeDano[level].ToString();

            currentRange.text = shootingRange.ToString();
            upgradeRangeText.text = "+" + upgradeRange[level].ToString();

            currentCadencia.text = timeBetweenFire.ToString();
            upgradeCadenciaText.text = "-" + upgradeCadencia[level].ToString();

            precoUpgradeText.text = precoUpgrade[level].ToString();

        }else{ // Se nivel máximo

            currentDamage.text = damage.ToString();
            upgradeDamageText.text = "MAX";

            currentRange.text = shootingRange.ToString();
            upgradeRangeText.text = "MAX";

            currentCadencia.text = timeBetweenFire.ToString();
            upgradeCadenciaText.text = "MAX";

            precoUpgradeText.text = "MAX";

        }
    }

    private void Animation(){
        if(enemyList.Count != 0) PlayAnimation("TorreForteAnim");
        if(enemyList.Count == 0) StopAnimation();
    }

    private void AtualizaDanoDado(){
        danoDado.text = DamageDealed.ToString("F0");
    }
}
