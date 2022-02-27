using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overfitting : MonoBehaviour
{
    public GameObject[] maps;

    public Vector3[] startpoints;
    public int currentMap = 0;

    public bool isSwitch = false;
    // Course pointer is incremented, and the next map is loaded
    // All objects are hidden and the new map is shown.
    public void switchMap(){
        FindObjectOfType<GameManager>().clear();
        FindObjectOfType<GameManager>().place();
        hideMaps();
        currentMap++;
        if(currentMap >= maps.Length){
            currentMap = 0;
        }
        maps[currentMap].SetActive(true);
    }
    // All maps are hidden
    public void hideMaps(){
        foreach(GameObject map in maps){
            map.SetActive(false);
        }
    }
    void Update(){
        // if the switch condition has been set to positve, switch the map
        if(isSwitch){
            switchMap();
            isSwitch = false;
        }
    }
    
}
