using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootGun : Tower
{   
    private int level = 0;
    [Header("UPGRADES")]
    [SerializeField] private int[] precoUpgrade;
    [SerializeField] private float[] upgradeDano;
    [SerializeField] private float[] upgradeRange;
    [SerializeField] private float[] upgradeCadencia;
    [Header("CONFIGS")]
    [SerializeField] private Transform[] bulletTransform;

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


    //Se possui metodo Start() nao chama o do pai
    void Update()
    {
        Rotation();
        Shooting();
        if(Input.GetMouseButtonDown(1)) AtivarDesativarCanvas(false);
        AtualizaDanoDado();
    }


     private void Shooting(){

        timer += Time.deltaTime;

        if(enemyList.Count >= 1 && timer > timeBetweenFire ){
            timer = 0;

            //Cria balas
            GameObject aux1 = Instantiate(bullet, bulletTransform[0].position, Quaternion.identity);
            GameObject aux2 = Instantiate(bullet, bulletTransform[1].position, Quaternion.identity);
            GameObject aux3 = Instantiate(bullet, bulletTransform[2].position, Quaternion.identity);
            
           //Cria auxiliares
            BulletTD aux11 = aux1.GetComponent<BulletTD>();
            BulletTD aux22 = aux2.GetComponent<BulletTD>();
            BulletTD aux33 = aux3.GetComponent<BulletTD>();

            aux11.damage = damage;
            aux22.damage = damage;
            aux33.damage = damage;

            //Atribue inimigo para seguir os tiros
            aux11.followEnemy = enemyList[0];
            aux22.followEnemy = enemyList[0];
            aux33.followEnemy = enemyList[0];

            aux11.SetTowerParent(GetComponent<Tower>());
            aux22.SetTowerParent(GetComponent<Tower>());
            aux33.SetTowerParent(GetComponent<Tower>());


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

    private void AtualizaDanoDado(){
        danoDado.text = DamageDealed.ToString("F0");
    }

}
