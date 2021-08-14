using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBehaviour : MonoBehaviour
{
    public bool findDistance = true;
    public int rows = 10;
    public int columns = 10;
    public int scale = 1;
    public GameObject gridPrefab;
    public Vector3 leftBottomLOcation = new Vector3(0,0,0);

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
    void Update()
    {
        if(findDistance){
            SetDistance();
            SetPath();
            ColourPath(path);
            //ClearPath();
            findDistance = false;
        }
    }
    void GenerateGrid(){
        for(int i =0;i < columns;i++){
            for(int j = 0;j < rows;j++){
                GameObject obj = Instantiate(gridPrefab, new Vector3(leftBottomLOcation.x+scale*i,leftBottomLOcation.y,leftBottomLOcation.z+scale *j),Quaternion.identity);
                obj.transform.SetParent(gameObject.transform);
                obj.GetComponent<GridStat>().x = i;
                obj.GetComponent<GridStat>().y = j;
                gridArray[i,j] = obj;
            }
        }
    }
    void InitialSetUp(){
        foreach(GameObject obj in gridArray){
            obj.GetComponent<GridStat>().visited = -1;
        }

        gridArray[startX,startY].GetComponent<GridStat>().visited = 0;
    }

    bool TestDirection(int x, int y, int step, int direction){
        // int direction tells which case to use 1 is up, 2 is right, 3 is down, 4 is left
        switch(direction){
            case 4:
                if(x-1 > -1 && gridArray[x-1,y] && gridArray[x-1,y].GetComponent<GridStat>().visited==step)
                    return true;
                else
                    return false;
            case 3:
                if(y-1 > -1 && gridArray[x,y-1] && gridArray[x,y-1].GetComponent<GridStat>().visited==step)
                    return true;
                else
                    return false;
            case 2:
                if(x+1 < columns && gridArray[x+1,y] && gridArray[x+1,y].GetComponent<GridStat>().visited==step)
                    return true;
                else
                    return false;

            case 1:
                if(y+1 < rows && gridArray[x,y+1] && gridArray[x,y+1].GetComponent<GridStat>().visited==step)
                    return true;
                else
                    return false;
            
            
            
        }
        return false;
        
    }
    void SetVisited(int x, int y, int step){
        if(gridArray[x,y]){
            gridArray[x,y].GetComponent<GridStat>().visited = step;
        }
    }
    void SetDistance(){
        InitialSetUp();
        int x = startX;
        int y = startY;
        for(int step = 1; step<rows * columns;step++){
            foreach(GameObject obj in gridArray){
                if(obj&&obj.GetComponent<GridStat>().visited == step-1){
                    TestFourDirections(obj.GetComponent<GridStat>().x,obj.GetComponent<GridStat>().y,step);
                }
            }
        }
    }
    void TestFourDirections(int x,int y, int step){
        if(TestDirection(x,y,-1,1)){
            SetVisited(x,y+1,step);
        }
        if(TestDirection(x,y,-1,2)){
            SetVisited(x+1,y,step);
        }
        if(TestDirection(x,y,-1,3)){
            SetVisited(x,y-1,step);
        }
        if(TestDirection(x,y,-1,4)){
            SetVisited(x-1,y,step);
        }

    }
    void SetPath(){
        int step;
        int x = endX;
        int y = endY;
        List<GameObject> tempList = new List<GameObject>();
        path.Clear();
        if(gridArray[x,y] && gridArray[x,y].GetComponent<GridStat>().visited > 0){
            path.Add(gridArray[x,y]);
            step = gridArray[x,y].GetComponent<GridStat>().visited - 1;
        } else {
            print("Can't Reach Desired Location");
            return;
        }
        for(int i = step; step > -1; step--){
            if(TestDirection(x,y,step,1)){
                tempList.Add(gridArray[x,y+1]);
            }
            if(TestDirection(x,y,step,2)){
                tempList.Add(gridArray[x+1,y]);
            }
            if(TestDirection(x,y,step,3)){
                tempList.Add(gridArray[x,y-1]);
            }
            if(TestDirection(x,y,step,4)){
                tempList.Add(gridArray[x-1,y]);
            }
        }
        print(tempList.Count);
        GameObject tempObj = FindClosest(gridArray[endX,endY].transform,tempList);
        path.Add(tempObj);
        x=tempObj.GetComponent<GridStat>().x;
        y=tempObj.GetComponent<GridStat>().y;
        tempList.Clear();
    }
    GameObject FindClosest(Transform targetLocation, List<GameObject> list){
        float currentDistance = scale * rows * columns;
        int indexNumber = 0;
        for(int i=0; i <list.Count;i++){
            if(Vector3.Distance(targetLocation.position,list[i].transform.position) < currentDistance){
                currentDistance = Vector3.Distance(targetLocation.position, list[i].transform.position);
                indexNumber = i;
            }

        }
        return list[indexNumber];
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
}
