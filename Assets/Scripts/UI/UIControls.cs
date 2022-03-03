using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{
    // UI Controls

    // Text objects that appear in the scene
    public Text tt_text,gen_text,bf_text,af_text,ts_text,gl_text,mr_text;
    public Slider mutationRate;
    public GameObject settings_page;
    private GameManager manager;

    // Maximum value the slider can take.
    public float maxMutation = 0.5f;
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        mutationRate.value = manager.mutRate / 0.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Update the text objects with the current values
        manager.mutRate = mutationRate.value * 0.5f;
        tt_text.text = "Time Trained : " + manager.training_time;
        gen_text.text = "Generation : " + manager.generation;
        bf_text.text = "Best Fitness : " + manager.bestScore;
        af_text.text = "Average Fitness : " + manager.averageFitness;
        ts_text.text = "Time Scale : " + Time.timeScale;
        gl_text.text = "Generation Length : " + manager.genTime;
        mr_text.text = "Mutation Rate : " + (manager.mutRate * 100) + "%";
        

    }
}
