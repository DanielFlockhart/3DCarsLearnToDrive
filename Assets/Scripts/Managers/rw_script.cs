  
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;
using System.Linq;
using System.Globalization;
using System;

/*
populationSize = 64;
generation;
training_time;
genTime = 0;
startTime = 15;
mutRate = 0.02f;
increment = 0.5f;
bestScore = -100;
*/
public class rw_script : MonoBehaviour
{
    public GameManager manager;
    
    string[] labels = {"populationSize","generation","training_time","genTime","startTime","mutRate","increment","bestScore"};
    void Start(){
        if (GameObject.FindGameObjectsWithTag("Ai").Length != 0){
            manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }
    }
    public void Save()
    {
        manager.storeData();
        List<float[][]> weights = manager.weights;
        List<float[][]> biases = manager.biases;
        preferences(manager.state,labels);
        CreateTextSpecial(weights,"weights");
        CreateTextSpecial(biases,"biases");

    }

    public void Save(string state,int checkpoint)
    {
        manager.storeData();
        List<float[][]> weights = manager.weights;
        List<float[][]> biases = manager.biases;
        CreateTextSpecial(weights,"weights","Checkpoint",checkpoint);
        CreateTextSpecial(biases,"biases","Checkpoint",checkpoint);
        preferences(manager.state,labels);
    }
    public void RaceLoading(int[] layers){
        // Convert a string to int
        int size = int.Parse(getPreference(0).ToString());
        ModelController modelController = FindObjectOfType<ModelController>();
        List<float[][]> weights = readTextFile(Application.dataPath + "/model/weights.txt",layers,false,size);
        List<float[][]> biases = readTextFile(Application.dataPath + "/model/biases.txt",layers,true,size);
        modelController.load_weights(weights,biases);
    }
    
    public void Load()
    {
        string[] newlayers= getPreference(8).ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        int[] layers = new int[newlayers.Length];
        for(int x=0; x<newlayers.Length; x++){
            layers[x] = int.Parse(newlayers[x]);
        }
        List<string> raw = new List<string>(getPreference(0).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        int size = int.Parse(raw[0].ToString());

        List<float[][]> weights = readTextFile(Application.dataPath + "/model/weights.txt",layers,false,size);
        List<float[][]> biases = readTextFile(Application.dataPath + "/model/biases.txt",layers,true,size);
        string[] passing = get_data(Application.dataPath + "/model/preferences.txt").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        float[] converted = new float[passing.Length];
        for(int x =0;x<passing.Length;x++){
            converted[x] = float.Parse(passing[x]);
        }
        manager.graph.GetComponent<graph_script>().reset_values();
        manager.setState(converted);
        manager.reload_weights(weights,biases,layers);
        
    }
    string get_data(string file_path){
        StreamReader inp_stm = new StreamReader(file_path);
        string inp="";
        while (!inp_stm.EndOfStream)
        {
            inp += inp_stm.ReadLine();
        }
        return inp;
    }
    List<float[][]> readTextFile(string file_path,int[] layers,bool biases,int popSize)
    {
        float[][][] output = new float[popSize][][];
        string inp=get_data(file_path);
        
        List<string> s = new List<string>(inp.Split(new string[] { "]" }, StringSplitOptions.RemoveEmptyEntries));
        
        for(int ai = 0; ai < popSize;ai++){
            output[ai] = new float[layers.Length-1][];
            for(int layer = 0; layer < layers.Length-1;layer++){
                int length = !biases ? layers[layer] * layers[layer+1] : layers[layer+1];
                output[ai][layer] = new float[length];
            }
        }
        int divisor = layers.Length-1;
        // im too tired for this but issue is here (below)
        //Temp fix for 4 layered network
        for(int item = 0; item < s.Count;item++){
            int ai = (int) Mathf.Floor(item / divisor);
            //print(ai + " " + (item % divisor));
            // iterating through too many ais
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
    

    void CreateTextSpecial(List<float[][]> inputList,string name,string state = "model",int checkpoint = -1)
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
        string path = "";
        if(state == "Checkpoint"){
            path = Application.dataPath + "/checkpoints/checkpoint"+checkpoint+"/"+name+".txt";
        } else {
            path = Application.dataPath + "/model/"+name+".txt";
        }
        
        File.WriteAllText(path, textToAppend);
    }

    string getPreference(int index){
        StreamReader inp_stm = new StreamReader(Application.dataPath + "/model/preferences.txt");
        string inp="";
        while (!inp_stm.EndOfStream)
        {
            inp += inp_stm.ReadLine() + "\n";
        }
        List<string> raw = new List<string>(inp.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));
        return raw[index];
    }
    void preferences(float[] data,string[] labels){
        string text = "";
        for(int x = 0; x < data.Length; x++){
            text += data[x]+",";
            text += "\n";
        }
        int[] layers = getLayers();
        for(int x = 0; x < layers.Length; x++){
            text += layers[x]+",";
        }
        File.WriteAllText(Application.dataPath + "/model/preferences.txt", text);
    }
    public int[] getLayers(){
        return FindObjectOfType<AiController>().layers;;
    }
}






