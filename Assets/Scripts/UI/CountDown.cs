using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    // Stores the object of the countdown timer.
    public Text countdown;
    public GameObject player;

    // Stores the time left for the countdown.
    private string[] queue = {"Ready?","3","2","1","go"};

    public float timer = 0f;
    private int index = 0;

    // Use this for initialization if the cars have to be spawned.
    bool loadedCars = false;

    void Start()
    {
        // Sets the countdown timer to the first element in the queue.
        setText(queue[index]);
        // Sets first car in the scene to be a player controlled car.
        player.GetComponent<CarController>().isPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // If the timer is greater than the time for the current element in the queue, the next element in the queue is set to the countdown timer.
        if(timer > 1 && !(index > 4)){
            countdown.gameObject.SetActive(true);
            setText(queue[index]);
            index++;
            timer = 0;
        } 
        // If the timer has reached the end of the queue the race starts and the cars are spawned in.
        if(timer > 1){
            countdown.gameObject.SetActive(false);
            startRace();
        }
    }
    // Function to set countdown timer text
    public void setText(string text){
        countdown.text = text;
    }
    // Function to start the race
    public void startRace () {
        player.GetComponent<CarController>().isPlayer = true;
        if (!loadedCars){
            // Loads the cars
            FindObjectOfType<rw_script>().GetComponent<rw_script>().Load();
            loadedCars = true;
        }
    }
}
