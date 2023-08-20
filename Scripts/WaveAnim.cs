using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WaveAnim : MonoBehaviour
{

    public Text waveText;

    public void Anim(int currentWave){
        StartCoroutine(StringAnim(currentWave));
    }

    private IEnumerator StringAnim(int currentWave){
        waveText.text = "Wave " + currentWave;
        waveText.gameObject.SetActive(true);

        for(int i = 0; i < 50; i ++){
            yield return new WaitForSeconds(0.08f);
            waveText.fontSize ++;
        }

        waveText.fontSize = 10;
        waveText.gameObject.SetActive(false);
    }




}
