using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControls : MonoBehaviour
{
    public Text tt_text,gen_text,bf_text,af_text,ts_text,gl_text,mr_text;
    private GameManager manager;
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        tt_text.text = "Time Trained : " + manager.training_time;
        gen_text.text = "Generation : " + manager.generation;
        bf_text.text = "Best Fitness : " + manager.bestScore;
        af_text.text = "Average Fitness : " + manager.averageFitness;
        ts_text.text = "Time Scale : " + Time.timeScale;
        gl_text.text = "Generation Length : " + manager.genTime;
        mr_text.text = "Mutation Rate : " + manager.mutRate;
    }
}
