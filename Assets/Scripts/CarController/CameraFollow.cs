using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Camera Script to follow car

    // Stores offset of camera
    [SerializeField] private Vector3 offset;

    // Stores the car to follow
    [SerializeField] private Transform target;

    // Stores the translation speed of the camera
    [SerializeField] private float translateSpeed;

    // Stores the rotation speed of the camera
    [SerializeField] private float rotationSpeed;
    
    // Called Every frame
    private void FixedUpdate()
    {
        // Translates the camera to the car
        HandleTranslation();
        HandleRotation();
    }

    // Handle the position of the camera in world space
    private void HandleTranslation() {
        var targetPosition = target.TransformPoint(offset);
        // Gradually move towards the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition,translateSpeed*Time.deltaTime);
    }


    // Handle the way the camera is facing in world space
    private void HandleRotation() {
        // Gets direction and way to rotate
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        // Gradually move towards the target position
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
