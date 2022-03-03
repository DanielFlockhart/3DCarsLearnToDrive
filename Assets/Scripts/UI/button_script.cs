using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class button_script : MonoBehaviour{  

    // Get objects of animators, prefabs, game managers and other ui features
    public Animator animator;
    public Animation animation;
    public UIControls uicontroller;
    private float timeScaleStored = 0;
    private GameManager manager;

    public GameObject graph;

    public GameObject saveChoice;

    public Text warning;

    int[] layers;

    public int pointer = 0;


    // Use this for initialization
    void Start(){
        manager = FindObjectOfType<GameManager>();
    }

    // Start next generation
    public void next_generation(){
        manager.GetComponent<GameManager>().timer = 100000000000;
        uicontroller = FindObjectOfType<UIControls>();
    }
    // Save the weights, biases, and preferences to a file
    public void save_weights(){
        saveChoice.SetActive(true);
        manager.GetComponent<rw_script>().Save();
    }
    // Load the weights, biases, and preferences from a file
    public void load_weights(){
        manager.GetComponent<rw_script>().Load();
    }
    // Increase the training speed
    public void increase_speed(){
        Time.timeScale += 0.2f;
    }
    // Decrease the training speed
    public void decrease_speed(){
        Time.timeScale -= 0.2f;
    }
    // Reset the training scene
    public void reset(){
        manager.clear();
        manager.spawn("init");
    }
    // Open the gameplay page
    public void play_ais(){
        SceneManager.LoadScene("GamePlay");
    }
    // Open the set up page
    public void load_setup(){
        SceneManager.LoadScene("Setup");
    }
    // open the training page
    public void load_training(){

        /*Add Input Checks Here*/
        if (setup.isSetUp == true){
            warning.text = "";
            StartCoroutine(load_scene(0.2f,"Main"));
        } else {
            warning.text = "One of your inputs is invalid! Please ensure all of the fields are filled out correctly as integers.";
        }
        
    }
    // Load the scene
    IEnumerator load_scene(float time,string scene){
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scene);
    }
    
    // Finish the training process
    public void finish_training(){
        SceneManager.LoadScene("Menu");
    }
    // Show all of the graph
    public void show_all_graph(){
        graph.GetComponent<graph_script>().plot(0,manager.generation);
    }
    // Show the next 50 items in the graph
    public void next_50_graph(){
        
        if (pointer > manager.generation-50){
            pointer = 0;
        }
        if(manager.generation > 50){
            graph.GetComponent<graph_script>().plot(pointer,pointer+50);
            pointer+=50;
        }
    }
}