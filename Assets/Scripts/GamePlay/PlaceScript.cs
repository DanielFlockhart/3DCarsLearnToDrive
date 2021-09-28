using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceScript : MonoBehaviour
{
    // Gameplay script to calculate places for mario kart style gameplay
    int pop;

    // Array of current goal positions
    public int[] positions;
    public int playerPos = 0;
    void Update()
    {
        // Get Population positions
        pop = GameObject.FindGameObjectsWithTag("Opponent").Length;
        positions = new int[pop];
        getPlaces();
        sortPlaces();

        // Compare player position to opponents and get place to display in the UI
        playerPos = findPlace(GameObject.FindGameObjectWithTag("Player").GetComponent<FitCheck>().currentGoal);
    }
    

    // Get full list of ai positions to sort into placements
    void getPlaces(){
        GameObject[] opponents = GameObject.FindGameObjectsWithTag("Opponent");
        for(int x = 0; x < positions.Length;x++){
            positions[x] = opponents[x].GetComponent<FitCheck>().currentGoal;
        }
    }
    /* Sort positions using bubble sort
    In code refactoring possibly use different sorting algorithm
    Have sorting algorithm in utils so I can just pass into there
    */
    void sortPlaces(){
        int swaps = 1;
        while (swaps != 0)
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

    // Find relative position of player or ai.
    int findPlace(int goal){
        for(int x = 0; x < positions.Length;x++){
            if(positions[x] <= goal){
                return x + 1;
            }
        }
        return positions.Length + 1;
    }
}   

