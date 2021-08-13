  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using System.Linq;
using System.Globalization;
using System;
public class rw_script : MonoBehaviour
{
    public GameManager manager;
    void Start(){
        manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();

    }
    public void Save()
    {
        manager.storeData();
        List<float[][]> weights = manager.weights;
        List<float[][]> biases = manager.biases;
        CreateTextSpecial(weights,"weights");
        CreateTextSpecial(biases,"biases");
    }
    
    public void Load(int[] layers)
    {
        
        List<float[][]> weights = readTextFile(Application.dataPath + "/model/weights.text",layers,false);
        List<float[][]> biases = readTextFile(Application.dataPath + "/model/biases.text",layers,true);
        manager.reload_weights(weights,biases);
        manager.graph.GetComponent<graph_script>().reset_values();
    }
    List<float[][]> readTextFile(string file_path,int[] layers,bool biases)
    {
        float[][][] output = new float[manager.populationSize][][];
        StreamReader inp_stm = new StreamReader(file_path);
        string inp="";
        while (!inp_stm.EndOfStream)
        {
            inp += inp_stm.ReadLine();
        }
        
        List<string> s = new List<string>(inp.Split(new string[] { "]" }, StringSplitOptions.RemoveEmptyEntries));
        for(int ai = 0; ai < manager.populationSize;ai++){
            output[ai] = new float[layers.Length-1][];
            for(int layer = 0; layer < layers.Length-1;layer++){
                int length = !biases ? layers[layer] * layers[layer+1] : layers[layer+1];
                output[ai][layer] = new float[length];
            }
        }
        // im too tired for this but issue is here (below)
        //Temp fix for 4 layered network

        for(int item = 0; item < s.Count;item++){
            int ai = (int) Mathf.Floor(item / 3);
            
            output[ai][item % 3] = uncompress_string(s[item]);
            
        }
        List<float[][]> weights = new List<float[][]>();
        for(int x = 0; x < output.Length;x++){
            weights.Add(output[x]);
        }
        return weights;
    }
    float[] uncompress_string(string data){
        List<string> raw = new List<string>(data.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        
        float[] cleaned = new float[raw.Count];
        for(int x = 0; x < raw.Count;x++){
            raw[x] = raw[x].Replace("[", "");
            cleaned[x] = float.Parse(raw[x]);
        }
        
        return cleaned;
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
    void preferences(){

    // UPLOAD other data to seperate txt - settings time scale generation training time etc
    }   

}




