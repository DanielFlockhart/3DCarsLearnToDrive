using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject car;
    public int populationSize = 80;
    public float genTime = 10;
    public float timer;
    public float bestScore = -100;
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (isOver()) {
            timer = 0;
            clear();
            spawn();
        
        }
    }

    void spawn() {
        for (int x = 0; x < populationSize; x++) {
            Instantiate(car);
        }
    }
    bool isOver() {
        if (GameObject.FindGameObjectsWithTag("Ai").Length == 0 || timer > genTime)
        {
            return true;
        }
        else {
            return false;
        }
    }

    void clear() {
        GameObject[] ais = GameObject.FindGameObjectsWithTag("Ai");
        foreach (GameObject ai in ais) {
            Destroy(ai);
        }
    }
}

