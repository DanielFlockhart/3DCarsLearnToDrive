using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBuilder : MonoBehaviour
{

    // Decided to cut City From Scope of Project
    //





    /*
    public bool findDistance = true;
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public GameObject gridPrefab;

    public GameObject buildings;
    public GameObject road;
    public Vector3 leftBottomLocation = new Vector3(0,0,0);

    public List<GameObject> path = new List<GameObject>();
    public GameObject[,] gridArray;
    public int startX = 0;
    public int startY = 0;
    public int endX = 5;
    public int endY = 5;

    // Start is called before the first frame update
    void Awake()
    {
        gridArray = new GameObject[columns,rows];
        if(gridPrefab){
            GenerateGrid();
        } else {
            print("Missing Grid Prefab, Please Assign");
        }
    }
    

    // Update is called once per frame
    void GenerateGrid(){
        for(int i =0;i < columns;i++){
            for(int j = 0;j < rows;j++){
                GameObject obj = Instantiate(getPiece(), new Vector3(leftBottomLocation.x+scale*i,leftBottomLocation.y,leftBottomLocation.z+scale *j),Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridInfo>().x = i;
                obj.GetComponent<GridInfo>().y = j;
                gridArray[i,j] = obj;
            }
        }
    }
    void ColourPath(List<GameObject> path){
        foreach(GameObject point in path){
            point.GetComponent<Renderer>().material.color = Color.green;
        }
    }
    void ClearPath(){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GridPiece");
        foreach(GameObject obj in objs){
            obj.GetComponent<Renderer>().material.color = Color.white;
        }
    }
    GameObject getPiece(){
        if(Random.Range(0,101) < 20){
            return buildings.transform.GetChild(Random.Range(0,buildings.transform.childCount)).gameObject;
        }
        return road;
    }

    */

}
