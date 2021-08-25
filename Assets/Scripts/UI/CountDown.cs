using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text countdown;

    public GameObject player;
    private string[] queue = {"Ready?","3","2","1","go"};

    public float timer = 0f;

    private int index = 0;

    bool loadedCars = false;

    void Start()
    {
        setText(queue[index]);
        player.GetComponent<CarController>().isPlayer = false;
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
            startRace();
        }
    }
    public void setText(string text){
        countdown.text = text;
    }
    public void startRace () {
        player.GetComponent<CarController>().isPlayer = true;
        if (!loadedCars){
            GetComponent<rw_script>().RaceLoading(GameObject.FindGameObjectWithTag("Ai").GetComponent<AiController>().layers);
            loadedCars = true;
        }
    }
}
