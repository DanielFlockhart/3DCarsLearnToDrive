using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button_script : MonoBehaviour
{
    public UIControls uicontroller;
    private float timeScaleStored = 0;
    private GameManager manager;

    public GameObject graph;

    int[] layers;

    public int pointer = 0;

    void Start(){
        manager = FindObjectOfType<GameManager>();
    }

    public void next_generation(){
        manager.GetComponent<GameManager>().timer = 100000000000;
        uicontroller = FindObjectOfType<UIControls>();
    }
    public void save_weights(){
        manager.GetComponent<rw_script>().Save();
    }
    
    public void load_weights(){
        layers = GameObject.FindGameObjectWithTag("Ai").GetComponent<AiController>().layers;
        manager.GetComponent<rw_script>().Load(layers);
    }
    public void load_settings(){
        timeScaleStored = Time.timeScale;
        Time.timeScale = 0;
        uicontroller.load_settings();
    }
    public void close_settings(){
        Time.timeScale = timeScaleStored;
        uicontroller.close_settings();
    }

    public void increase_speed(){
        Time.timeScale += 0.2f;
    }
    public void decrease_speed(){
        Time.timeScale -= 0.2f;
    }
    public void reset(){
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
    public void play_ais(){
        SceneManager.LoadScene("Play");
    }
    public void load_setup(){
        SceneManager.LoadScene("Setup");
    }

    public void load_training(){
        SceneManager.LoadScene("Main");
    }
    public void finish_training(){
        SceneManager.LoadScene("Results");
    }
    public void show_all_graph(){
        graph.GetComponent<graph_script>().plot(0,manager.generation);
    }
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