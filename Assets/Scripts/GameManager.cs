using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject car;
    public int populationSize = 80;
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
            Instantiate(car);
        }
    }
}
