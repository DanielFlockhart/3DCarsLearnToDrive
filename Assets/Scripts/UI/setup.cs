using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setup : MonoBehaviour
{
    // The input box gameobjects
    public GameObject hLayers,hNodes,tScale,interObj;

    // Set default values for the hyperparameters for debugging when loading directly into training view.
    public static int hiddenLayers = 3;
    public static int hiddenNodes = 16;
    public static int timeScale = 2;
    public static int population = 160;
    public static int checkInterval = 500000;
    public static bool isSetUp = false;

    void Update()
    {
        // Initially the setup boolean is set to true. If any of the inputs are not valid, the setup boolean is set to false.
        isSetUp = true;
        // Each of these correspnds to the UI elements in the setup menu.
        population = FindObjectOfType<SetupButtons>().population;
        hiddenLayers = getInput(hLayers.GetComponent<InputField>().text);
        hiddenNodes = getInput(hNodes.GetComponent<InputField>().text);
        timeScale = getInput(tScale.GetComponent<InputField>().text);
        checkInterval = getInput(interObj.GetComponent<InputField>().text);
    }

    int getInput(string input){
        
        // If the input is not a boolean the isSetUp boolean is set to false.
        try {
            int val = int.Parse(input);
            //if the input is in the valid range, return the input.
            if( val > 0 && val < 10000) {
                return val;
            } else {
                isSetUp = false;
                return 0;
            }

        } catch {
            isSetUp = false;
            return 0;
        }
    }
}
