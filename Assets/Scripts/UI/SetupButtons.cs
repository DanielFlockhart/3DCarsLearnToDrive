using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupButtons : MonoBehaviour
{
    public int population;

    public Text popText;
    public void Update(){
        // Display the current chosen population size
        popText.text = "Batch Size : " + population;
    }
    public void incPopulation(){
        // Increments the population size by 16
        population+=16;
    }
    public void decPopulation(){
        // Decreases the population size by 16, if it is less than 16, it will be set to 16
        population-=16;
        if(population <= 0){
            population = 16;
        }
    }
}
