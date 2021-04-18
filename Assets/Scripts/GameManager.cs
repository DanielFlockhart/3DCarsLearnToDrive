using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject car;
    public int populationSize = 40;
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawn() {
        for (int x = 0; x < populationSize; x++) {
            GameObject ai = Instantiate(car);
            ai.transform.SetParent(transform.Find("Cars"), false);
        }
    }
}
