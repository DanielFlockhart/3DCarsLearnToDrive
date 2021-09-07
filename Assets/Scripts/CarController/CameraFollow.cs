using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Camera Script to follow car
    // Will eventually be set up so you can click on a car and see from that perspective
    // Not currently Implemented

    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;
    
    private void FixedUpdate()
    {
        HandleTranslation();
        HandleRotation();
    }

    // Handle the position of the camera in world space
    private void HandleTranslation() {
        var targetPosition = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition,translateSpeed*Time.deltaTime);
    }


    // Handle the way the camera is facing in world space
    private void HandleRotation() {
        var direction = target.position - transform.position;
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
