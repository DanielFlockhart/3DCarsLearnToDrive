using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    private CarController carControls;
    private Brain brain;

    [SerializeField] int[] layers;

    [SerializeField] int inputs = 12;
    [SerializeField] int outputs = 3;

    private Vector3[] rays;
    private float[] distances;
    
    [SerializeField] float forwardVal;
    [SerializeField] float leftVal;
    [SerializeField] bool breakVal;



    [SerializeField] float[] input;
    [SerializeField] float[] output;

    // Start is called before the first frame update
    void Awake()
    {
        int hiddenNodes = Mathf.RoundToInt(inputs * (2 / 3)) + outputs;
        layers = new int[] { inputs, 16, 16, outputs};
        brain = GetComponent<Brain>();
        brain.layers = layers;
        carControls = GetComponent<CarController>();
        brain.build();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rays = getRays();
        //drawRays(rays);
        distances = getCollisions(rays);

        float breakforce = carControls.currentBreakForce/3000;
        float steeringAngle = carControls.currentSteeringAngle/30;
        input = new float[] { breakforce, steeringAngle, distances[0], distances[1], distances[2], distances[3], distances[4], distances[5], distances[6], distances[7], distances[8], distances[9]};
        output = brain.getOutputs(input);
        forwardVal = output[0] > 0 ? 1 : -1;
        leftVal = output[1] > 0 ? 1 : -1;
        breakVal = output[2] > 0 ? true : false;
        operateCar(forwardVal, leftVal, breakVal);

    }

    private void operateCar(float forwardVal, float leftVal, bool breakVal)
    {
        carControls.operate(forwardVal,leftVal,breakVal);
    }

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

    private void drawRays(Vector3[] rays) {
        foreach(Vector3 ray in rays) {
            Debug.DrawRay(transform.position + new Vector3(0f, 1f, 0f), ray * 1000, Color.green, Mathf.Infinity);
        }
    }

    private float[] getCollisions(Vector3[] rays) {
        float[] distanceList = new float[rays.Length];
        for(int ray = 0; ray < rays.Length;ray++) {
            // Inefficient as it has to check all collisions
            RaycastHit[] hitRay = Physics.RaycastAll(transform.position +new Vector3(0f, 1f, 0f), rays[ray] * 1000);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }
    }
}
