using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overfitting : MonoBehaviour
{
    public GameObject[] maps;

    public Vector3[] startpoints;
    public int currentMap = 0;
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
    public void hideMaps(){
        foreach(GameObject map in maps){
            map.SetActive(false);
        }
    }
}
