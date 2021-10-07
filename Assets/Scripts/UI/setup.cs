using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setup : MonoBehaviour
{
    public GameObject hLayers,hNodes,tScale,interObj;
    public static int hiddenLayers = 0;
    public static int hiddenNodes = 0;
    public static int timeScale = 1;
    public static int population = 16;
    public static int checkInterval = 50;
    public static bool isPreloading = false;
    public static bool isUnderfitting = true;

    void Update()
    {
        population = FindObjectOfType<SetupButtons>().population;
        hiddenLayers = getInput(hLayers.GetComponent<InputField>().text);
        hiddenNodes = getInput(hNodes.GetComponent<InputField>().text);
        timeScale = getInput(tScale.GetComponent<InputField>().text);
        checkInterval = getInput(interObj.GetComponent<InputField>().text);
    }

    int getInput(string input){
        try {
            return int.Parse(input);
        } catch {
            return 0;
        }
    }
}
