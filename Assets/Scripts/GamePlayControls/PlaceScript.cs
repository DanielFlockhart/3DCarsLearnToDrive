using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceScript : MonoBehaviour
{
    // Gameplay script to calculate places 
    int pop;
    public int[] positions;
    public int playerPos = 0;
    void Update()
    {
        pop = GameObject.FindGameObjectsWithTag("Opponent").Length;
        positions = new int[pop];
        getPlaces();
        sortPlaces();
        playerPos = findPlace(GameObject.FindGameObjectWithTag("Player").GetComponent<FitCheck>().currentGoal);
    }
    

    void getPlaces(){
        GameObject[] opponents = GameObject.FindGameObjectsWithTag("Opponent");
        for(int x = 0; x < positions.Length;x++){
            positions[x] = opponents[x].GetComponent<FitCheck>().currentGoal;
        }
    }
    void sortPlaces(){
        int swaps = 1;
        while (swaps != 0) // Bubble sort
        {
            swaps = 0;
            for (int i = 0; i < positions.Length - 1; i++)
            {
                if (positions[i + 1] > positions[i])
                {
                    int storedPos = positions[i];
                    positions[i] = positions[i + 1];
                    positions[i + 1] = storedPos;
                    swaps++;
                }
            }
        }
    }

    int findPlace(int goal){
        for(int x = 0; x < positions.Length;x++){
            if(positions[x] <= goal){
                return x + 1;
            }
        }
        return positions.Length + 1;
    }
}   

