using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    /* Main Controller of Ai
    Neural Network is controlled
    Inputs are controlled (Raycasts and Orientations atm) 
    Interaction with CarController Script */


    // Prefabs
    private CarController carControls;
    private Brain brain;

    // Other
    public string state;
    public bool isAlive = true;


    //Input Variables
    private Vector3[] rays;
    private float[] distances;
    

    // Output states
    [SerializeField] float forwardVal;
    [SerializeField] float leftVal;
    [SerializeField] bool breakVal;


    // Neural Network Variables
    [SerializeField] float[] input;
    [SerializeField] float[] output;

    [SerializeField] int[] layers;

    [SerializeField] int inputs = 12;
    [SerializeField] int outputs = 3;

    
    // Awake is used instead of start() as brain needs to be assigned before cars start moving
    void Awake()
    {
        // Calculate the amount of hidden nodes required (Depending on inputs and outputs total)
        int hiddenNodes = Mathf.RoundToInt(inputs * (2 / 3)) + outputs;
        layers = new int[] { inputs, 16, 16, outputs};
        brain = GetComponent<Brain>();
        brain.layers = layers;
        carControls = GetComponent<CarController>();
        
        
    }
    private void Start()
    {
        // If in initial spawn stages build brain with new weights etc
        if (state == "init")
        {
            brain.build();
        }
    }

    // All Physics should be contained in FixedUpdate for standardisation
    void FixedUpdate()
    {

        // Get inputs
        
        rays = getRays();
        //drawRays(rays);
        distances = getCollisions(rays);

        float breakforce = carControls.currentBreakForce / 3000;
        float steeringAngle = carControls.currentSteeringAngle / 30;
        input = new float[] { breakforce, steeringAngle, distances[0], distances[1], distances[2], distances[3], distances[4], distances[5], distances[6], distances[7], distances[8], distances[9] };
        output = brain.getOutputs(input);
        // Arg Max for binary input requirements
        forwardVal = output[0] > 0 ? 1 : -1;
        leftVal = output[1] > 0 ? 1 : -1;
        breakVal = output[2] > 0 ? true : false;
        if (isAlive)
        {
            operateCar(forwardVal, leftVal, breakVal);
        }
    }
    // Operate car using outputs from the neural network
    private void operateCar(float forwardVal, float leftVal, bool breakVal)
    {
        carControls.operate(forwardVal,leftVal,breakVal);
    }

    // Get input rays from car (Forward bias)
    private Vector3[] getRays() {
        //Forward
        Vector3 dir1 = transform.TransformDirection(Vector3.forward);

        //Backwards
        Vector3 dir2 = -dir1;

        //Right
        Vector3 dir3 = transform.TransformDirection(Vector2.right); 

        //Left
        Vector3 dir4 = -dir3;

        //Forward Right
        Vector3 dir5 = dir1 + dir3;

        //Forward Left
        Vector3 dir6 = dir1 + dir4;

        //Forward Right Right
        Vector3 dir7 = dir5 + dir3;

        //Forward Left Left
        Vector3 dir8 = dir6 + dir4;

        //Forward Rightish
        Vector3 dir9 = dir7 + dir6;

        //Forward Leftish
        Vector3 dir10 = dir8 + dir5;

       
        return new Vector3[] { dir1, dir2, dir3, dir4, dir5, dir6, dir7, dir8, dir9, dir10 };

    }

    // Draw rays to screen for debugging
    private void drawRays(Vector3[] rays) {
        foreach(Vector3 ray in rays) {
            Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), ray * 1000, Color.green, Mathf.Infinity);
        }
    }

    /* 
    Get all objects rays collide with and checks if collides with wall
    This is essentially a temporary fix as it is inefficient to check through all collisions.
    This use is a result of issues with layers
    */

    private float[] getCollisions(Vector3[] rays) {
        float[] distanceList = new float[rays.Length];
        for(int ray = 0; ray < rays.Length;ray++) {
            // Inefficient as it has to check all collisions
            RaycastHit[] hitRay = Physics.RaycastAll(transform.position +new Vector3(0f, 1f, 0f), rays[ray] * 1000);
            //if(gameObject.name == "car20"){
            //    Debug.DrawRay(transform.position,rays[ray],Color.green,1000);
            //}
            if (hitRay.Length != 0)
            {
                foreach (RaycastHit rayed in hitRay)
                {
                    if (rayed.collider.tag != "Ai")
                    {
                        distanceList[ray] = hitRay[0].distance/50;
                        break;
                    }
                }
            }
            else {
                distanceList[ray] =0;
            }
        }
        return distanceList;
    }

}
