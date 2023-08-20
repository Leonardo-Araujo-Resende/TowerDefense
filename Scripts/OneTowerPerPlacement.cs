using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTowerPerPlacement : MonoBehaviour
{

    public GameObject shootingRangeRender;

        public void AtivarShootingRangeRender(){
            shootingRangeRender.SetActive(true);
        }

        public void DesativarShootingRangeRender(){
            shootingRangeRender.SetActive(false);
        }
}
