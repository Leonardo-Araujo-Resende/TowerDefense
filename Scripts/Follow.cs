using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject follow;    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.transform.position + new Vector3(0,0.8f,0);    
    }

    
}
