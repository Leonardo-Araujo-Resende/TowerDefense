using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombStrongTower : Bullet
{
    

    [Header("CONFIGS")]
    [SerializeField] private Vector3 explosionPosition;
    [SerializeField] private CircleCollider2D circleCollider2D;
    [SerializeField] private  GameObject explosion;
    private bool isStarted;
    
    


    // Update is called once per frame
    void Update()
    {   
        Moviment();
        

        if(transform.position == explosionPosition && !isStarted){
            StartCoroutine(DestroyRoutine());
            isStarted = true;
        }

    }


    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "EnemyTD"){
            other.GetComponent<EnemyTD>().Damage(damage);
            IncreaseDamageDealed(damage);
        }   
        
    }

    private IEnumerator DestroyRoutine(){
        circleCollider2D.enabled = true;
        Instantiate(explosion, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Destroy(this.gameObject);
        
    }

    private void Moviment(){
        transform.position = Vector3.MoveTowards(transform.position, explosionPosition, speed * Time.deltaTime);
    }

    public void SetExplosionPosition(Vector3 a){
        explosionPosition = a;
    }



}
