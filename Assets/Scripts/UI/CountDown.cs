using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text countdown;
    private string[] queue = {"Ready?","3","2","1","go"};

    public float timer = 0f;

    private int index = 0;

    void Start()
    {
        print(index);
        setText(queue[index]);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1 && !(index > 4)){
            countdown.gameObject.SetActive(true);
            setText(queue[index]);
            index++;
            timer = 0;
        } 
        if(timer > 1){
            countdown.gameObject.SetActive(false);
        }
    }
    public void setText(string text){
        countdown.text = text;
    }
}
