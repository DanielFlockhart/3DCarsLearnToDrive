  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using System.Linq;
using System.Globalization;
public class rw_script : MonoBehaviour
{
    public void Save()
    {
        GameManager manager = FindObjectOfType<GameManager>();
        GameObject[] ais = GameObject.FindGameObjectsWithTag("Ai");
        manager.GetComponent<GameManager>().storeData(ais);
        List<float[][]> weights = manager.GetComponent<GameManager>().weights;
        List<float[][]> biases = manager.GetComponent<GameManager>().biases;
        CreateTextSpecial(weights,"weights");
        CreateTextSpecial(biases,"biases");
    }
    
    public void Load()
    {
        /*
        GameManager manager = FindObjectOfType<GameManager>();
        manager.toLoadWeights = readTextFile(Application.dataPath + "/weights.text",false);
        manager.toLoadBiases = readTextFile(Application.dataPath + "/biases.text",true);
        manager.ToLoad = true;
        */
    }
    float[] readTextFile(string file_path,bool biases)
    {
        
        StreamReader inp_stm = new StreamReader(file_path);
        string inp_ln = "";
        while (!inp_stm.EndOfStream)
        {
            inp_ln = inp_stm.ReadLine();
        }
        string[] numb = inp_ln.Split(" "[0]);
        float[] numList = new float[numb.Length];
        for(int x = 1; x < numb.Length; x++)
        {
            numList[x-1] = float.Parse(numb[x]);
        }

        return numList;
    }
    

    void CreateTextSpecial(List<float[][]> inputList,string name)
    {
        string textToAppend = "[";
        for (int ai = 0; ai < inputList.Count;  ai++)
        {
            if(ai == 0){
                textToAppend +="[";
            } else {
                textToAppend += ",[";
            }
            for (int layer = 0; layer < inputList[ai].Length; layer++)
            {
                if(layer == 0){
                    textToAppend +="[";
                } else {
                    textToAppend += ",[";
                }
                for (int node = 0; node < inputList[ai][layer].Length; node++)
                {
                    if(node != 0){
                        textToAppend +=",";
                    } 
                    textToAppend += inputList[ai][layer][node];
                }
                textToAppend += "]";
            }
            textToAppend += "]";
        }
        textToAppend += "]";

        string path = Application.dataPath + "/model/"+name+".text";
        File.WriteAllText(path, textToAppend);
    }

}

