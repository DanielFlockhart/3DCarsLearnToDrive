using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupButtons : MonoBehaviour
{
    public int population;

    public Text popText;
    public void Update(){
        popText.text = "Batch Size : " + population;
    }
    public void incPopulation(){
        population+=16;
    }
    public void decPopulation(){
        population-=16;
        if(population <= 0){
            population = 16;
        }
    }
}
