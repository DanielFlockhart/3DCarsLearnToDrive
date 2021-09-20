using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{
    // UI Controls
    public Text tt_text,gen_text,bf_text,af_text,ts_text,gl_text,mr_text;
    public Slider mutationRate;
    public GameObject settings_page;
    private GameManager manager;
    public float maxMutation = 0.5f;
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        mutationRate.value = manager.mutRate / 0.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        manager.mutRate = mutationRate.value * 0.5f;
        tt_text.text = "Time Trained : " + manager.training_time;
        gen_text.text = "Generation : " + manager.generation;
        bf_text.text = "Best Fitness : " + manager.bestScore;
        af_text.text = "Average Fitness : " + manager.averageFitness;
        ts_text.text = "Time Scale : " + Time.timeScale;
        gl_text.text = "Generation Length : " + manager.genTime;
        mr_text.text = "Mutation Rate : " + (manager.mutRate * 100) + "%";
        

    }
    public void load_settings(){
        settings_page.SetActive(true);
    }
    public void close_settings(){
        settings_page.SetActive(false);
    }
}
