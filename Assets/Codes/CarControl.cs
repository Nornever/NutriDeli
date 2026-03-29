using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;

public class CarControl : MonoBehaviour
{
    [Header("Car Properties")]
    public float motorTorque = 2000f;
    public float brakeTorque = 2000f;
    public float maxSpeed = 20f;
    public float steeringRange = 30f;
    public float steeringRangeAtMaxSpeed = 10f;
    public float centreOfGravityOffset = -1f;

    [Header("Boost Text")]
    public Text boostText; // Drag your UI Text here in inspector

    private WheelControl[] wheels;
    private Rigidbody rigidBody;

    private float boostTimeRemaining = 0f;
    private float boostDuration = 5f;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Adjust center of mass
        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;

        // Get all wheels
        wheels = GetComponentsInChildren<WheelControl>();
    }

    void Update()
    {
        // Update boost text countdown
        if (boostTimeRemaining > 0f)
        {
            boostTimeRemaining -= Time.deltaTime;
            if (boostText != null)
                boostText.text = "Boost: " + Mathf.Ceil(boostTimeRemaining) + "s";
        }
        else
        {
            if (boostText != null)
                boostText.text = "";
        }
    }

    void FixedUpdate()
    {
        // --- Keyboard input using New Input System ---
        float vInput = 0f;
        float hInput = 0f;

        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) vInput = 1f;
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) vInput = -1f;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) hInput = 1f;
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) hInput = -1f;

        // Forward speed along car's local X axis (your axes setup)
        float forwardSpeed = Vector3.Dot(transform.right, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = Mathf.Sign(vInput) == Mathf.Sign(forwardSpeed) || forwardSpeed == 0;

        foreach (var wheel in wheels)
        {
            if (wheel.steerable)
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;

            if (isAccelerating)
            {
                if (wheel.motorized)
                    wheel.WheelCollider.motorTorque = vInput * currentMotorTorque;
                wheel.WheelCollider.brakeTorque = 0f;
            }
            else
            {
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = Mathf.Abs(vInput) * brakeTorque;
            }
        }
    }

    private void ResetCar()
    {
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
    }

    // ---- BOOST FUNCTIONS ----
    public void ActivateSpeedBoost(float duration)
    {
        StopAllCoroutines();
        boostDuration = duration;
        boostTimeRemaining = duration;
        StartCoroutine(SpeedBoostCoroutine(duration));
    }

    private IEnumerator SpeedBoostCoroutine(float duration)
    {
        float originalMaxSpeed = maxSpeed;
        float originalMotorTorque = motorTorque;

        maxSpeed *= 5f;
        motorTorque *= 2f;

        yield return new WaitForSeconds(duration);

        maxSpeed = originalMaxSpeed;
        motorTorque = originalMotorTorque;
    }
}