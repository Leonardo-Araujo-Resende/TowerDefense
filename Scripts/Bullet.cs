using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    private Tower towerParent;

    public void SetTowerParent(Tower a){
        towerParent = a;
    }

    public void IncreaseDamageDealed(float a){
        towerParent.IncreaseDamageDealed(a);
    }
    
}
