using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTD : Bullet
{

    public EnemyTD followEnemy;


    void Update()
    {
        if(followEnemy == null) Destroy(this.gameObject);
        if(followEnemy != null)transform.position = Vector3.MoveTowards(transform.position, followEnemy.transform.position, speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "EnemyTD" && other.gameObject.Equals(followEnemy.gameObject)){
            other.GetComponent<EnemyTD>().Damage(damage);
            IncreaseDamageDealed(damage);
            Destroy(this.gameObject);
        }
    }
    
}


