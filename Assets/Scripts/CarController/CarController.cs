using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Main Physics controller of car

    public bool isPlayer = false;
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    private float horizontalInput;
    private float verticalInput;
    public float currentSteeringAngle;
    public float currentBreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider, backLeftWheelCollider, backRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform, backLeftWheelTransform, backRightWheelTransform;

    // Commented as only required for when a use chooses to pilot a car
    // Will eventaully add the ability to race against the ais

    private void FixedUpdate()
    {
        if(isPlayer){
            GetUserInput();
            HandleCarMotor();
            HandleCarSteering();
            UpdateCarWheels();
        }
        
    }

    // Use outputs of NN to act and change state of car
    public void operate(float forward,float left,bool breakVal) {
        //GetUserInput();
        horizontalInput = left;
        verticalInput = forward;
        isBreaking = breakVal;
        HandleCarMotor();
        HandleCarSteering();
        UpdateCarWheels();
    }

    // Change the acceleration and force the motor is providing
    private void HandleCarMotor() {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        ApplyBreak();

    }


    // Slow car down
    private void ApplyBreak()
    {
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        backRightWheelCollider.brakeTorque = currentBreakForce;
        backLeftWheelCollider.brakeTorque = currentBreakForce;
    }

    // Get user controls (WASD) + space for break
    private void GetUserInput() {
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    // Control direction of travel. - Issues with wheels going haywire as a result of binary outputs of network. Smoother?
    private void HandleCarSteering()
    {
        currentSteeringAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteeringAngle;
        frontRightWheelCollider.steerAngle = currentSteeringAngle;

    }

    // Update all of wheels simultaneously 
    private void UpdateCarWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    // Set wheel rotation and position according to world space
    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
