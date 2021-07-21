using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button_script : MonoBehaviour
{
    public UIControls uicontroller;
    private float timeScaleStored = 0;
    public void next_generation(){
        FindObjectOfType<GameManager>().GetComponent<GameManager>().timer = 100000000000;
        uicontroller = FindObjectOfType<UIControls>();
    }
    public void save_weights(){

    }
    public void load_weights(){

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
}
