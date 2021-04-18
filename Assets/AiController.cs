using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    private CarController carControls;
    private Brain brain;
    private int[] layers;

    private int inputs = 12;
    private int outputs = 3;

    private Vector3[] rays;
    [SerializeField] float[] distances;
    
    [SerializeField] float forwardVal;
    [SerializeField] float leftVal;
    [SerializeField] bool breakVal;
    // Start is called before the first frame update
    void Start()
    {
        int hiddenNodes = (2/3 * inputs) + outputs;
        layers = new int[] { inputs, hiddenNodes, hiddenNodes, outputs};
        brain = GetComponent<Brain>();
        carControls = GetComponent<CarController>();
        brain.build(layers);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rays = getRays();
        //drawRays(rays);
        distances = getCollisions(rays);

        float breakforce = carControls.currentBreakForce;
        float steeringAngle = carControls.currentSteeringAngle;
        float[] inputs = new float[] { breakforce, steeringAngle, distances[0], distances[1], distances[2], distances[3], distances[4], distances[5], distances[6], distances[7], distances[8], distances[9]};
        float[] outputs = brain.getOutputs(inputs);
        forwardVal = outputs[0] > 0 ? 1 : -1;
        leftVal = outputs[1] > 0 ? 1 : -1;
        breakVal = outputs[2] > 0 ? true : false;
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
        print("here");
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
                        distanceList[ray] = hitRay[0].distance;
                        break;
                    }
                }
            }
            else {
                distanceList[ray] =Mathf.Infinity;
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
