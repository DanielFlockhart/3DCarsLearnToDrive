using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button_script : MonoBehaviour
{
    public void next_generation(){
        FindObjectOfType<GameManager>().GetComponent<GameManager>().timer = 100000000000;
    }
    public void save_weights(){

    }
    public void load_weights(){

    }
    public void reset(){
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
