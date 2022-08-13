  
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
        // If there is ais in the environment, assign the game manager
        if (GameObject.FindGameObjectsWithTag("Ai").Length != 0){
            manager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }
    }
    // Save the weights, biases, and preferences to a file
    public void Save()
    {
        // WEIGHTS AND BIASES ARE NOT RIGHT LENGTH
        manager.storeData();
        List<float[][]> weights = manager.weights;
        List<float[][]> biases = manager.biases;
        
        preferences(manager.state,labels);
        CreateTextSpecial(weights,"weights");
        CreateTextSpecial(biases,"biases");

    }
    

    
    // Function used for loading weights and biases from a file
    public async void Load()
    {
        // Convert a string into a string array
        string[] newlayers= getPreference(8).ToString().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        int[] layers = new int[newlayers.Length];
        for(int x=0; x<newlayers.Length; x++){
            layers[x] = int.Parse(newlayers[x]);
        }
        // Convert a string to List of strings
        List<string> raw = new List<string>(getPreference(0).Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        int size = int.Parse(raw[0].ToString());

        if ((System.IO.File.Exists(Application.dataPath + "/model/biases.txt") && System.IO.File.Exists(Application.dataPath + "/model/weights.txt")) == false){
            // If there is no model, break
            print("No model found");
            return;
        }

        // Assign weights and biases
        List<float[][]> weights = readTextFile(Application.dataPath + "/model/weights.txt",layers,false,size);
        List<float[][]> biases = readTextFile(Application.dataPath + "/model/biases.txt",layers,true,size);

        if(SceneManager.GetActiveScene().name == "GamePlay"){
            // Assign weights and biases
            ModelController modelController = FindObjectOfType<ModelController>();
            modelController.load_weights(weights,biases,layers);
            return;
        }

        string[] passing = get_data(Application.dataPath + "/model/preferences.txt").Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        float[] converted = new float[passing.Length];

        for(int x =0;x<passing.Length;x++){
            converted[x] = float.Parse(passing[x]);
        }
        manager.graph.GetComponent<graph_script>().reset_values();
        manager.setState(converted);
        manager.reload_weights(weights,biases,layers);
        
    }
    // Get the raw text from the file store.
    string get_data(string file_path){
        StreamReader inp_stm = new StreamReader(file_path);
        string inp="";
        while (!inp_stm.EndOfStream)
        {
            inp += inp_stm.ReadLine();
        }
        return inp;
    }
    // Split the raw text into a list of strings
    List<float[][]> readTextFile(string file_path,int[] layers,bool biases,int popSize)
    {
        // ----------------------------------------------------------------------------------------------------
        float[][][] output = new float[popSize][][];
        string inp=get_data(file_path);
        
        List<string> s = new List<string>(inp.Split(new string[] { "]" }, StringSplitOptions.RemoveEmptyEntries));
        
        for(int ai = 0; ai < popSize;ai++){
            output[ai] = new float[layers.Length-1][];
            for(int layer = 0; layer < layers.Length-1;layer++){
                // HERE
                int length = !biases ? layers[layer] * layers[layer+1] : layers[layer+1];
                output[ai][layer] = new float[length];
            }
        }
        int divisor = layers.Length-1;
        for(int item = 0; item < s.Count;item++){
            int ai = (int) Mathf.Floor(item / divisor); // THE ISSUE WAS HERE ARGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG
            output[ai][item % divisor] = uncompress_string(s[item]);
        }
        List<float[][]> weights = new List<float[][]>();

        // ----------------------------------------------------------------------------------------------------
        for(int x = 0; x < output.Length;x++){
            weights.Add(output[x]);
        }
        return weights;
    }
    // Split the strings into raw floats which are then assigned to lists
    float[] uncompress_string(string data){
        List<string> raw = new List<string>(data.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
        
        float[] cleaned = new float[raw.Count];
        for(int x = 0; x < raw.Count;x++){
            raw[x] = raw[x].Replace("[", "");
            cleaned[x] = float.Parse(raw[x]);
        }
        return cleaned;
    }
    
    // Create the text that will then be saved to a file
    // Code is ineffecient, but it works
    // If you are reading this, ingore this function, accept the fact it WORKS and move on with your life
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
    // Get data from preference file
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
    // Create data that will then be uploaded to preferences file
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

    // Get the layers for the neural network
    public int[] getLayers(){
        return FindObjectOfType<AiController>().layers;
    }
}




