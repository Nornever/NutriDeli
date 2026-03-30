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

    [Header("Acceleration Settings")]
    public float accelerationSpeed = 6f;
    public float decelerationSpeed = 8f;

    [Header("Boost Text")]
    public Text boostText;

    [Header("Speedometer Settings")]
    public float displayedSpeed = 0f;  // Speed the needle shows
    public float speedLerpRate = 5f;   // How fast the needle follows the speed

    private WheelControl[] wheels;
    private Rigidbody rigidBody;

    private float boostTimeRemaining = 0f;
    private float boostDuration = 5f;

    private float currentThrottle = 0f;
    private bool isDecayingVelocity = false; // Prevent acceleration during decay

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        // Adjust center of mass
        Vector3 centerOfMass = rigidBody.centerOfMass;
        centerOfMass.y += centreOfGravityOffset;
        rigidBody.centerOfMass = centerOfMass;

        wheels = GetComponentsInChildren<WheelControl>();
    }

    void Update()
    {
        // Smooth displayed speed for speedometer
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        displayedSpeed = Mathf.Lerp(displayedSpeed, forwardSpeed, speedLerpRate * Time.deltaTime);

        // Boost countdown UI
        if (boostTimeRemaining > 0f)
        {
            if (boostText != null)
                boostText.text = "Boost: " + Mathf.Ceil(boostTimeRemaining) + "s";
        }
        else
        {
            if (boostText != null)
                boostText.text = "";
        }

        // Reset car
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ResetCar();
        }
    }

    void FixedUpdate()
    {
        float hInput = 0f;
        float targetThrottle = 0f;

        // --- Input (New Input System) ---
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
            targetThrottle = 1f;
        else if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
            targetThrottle = -1f;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            hInput = 1f;
        else if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            hInput = -1f;

        // --- Smooth acceleration ---
        float accelRate = (Mathf.Abs(targetThrottle) > 0.1f) ? accelerationSpeed : decelerationSpeed;
        currentThrottle = Mathf.Lerp(currentThrottle, targetThrottle, accelRate * Time.fixedDeltaTime);

        // --- Speed calculations ---
        float forwardSpeed = Vector3.Dot(transform.forward, rigidBody.linearVelocity);
        float speedFactor = Mathf.InverseLerp(0, maxSpeed, Mathf.Abs(forwardSpeed));

        float currentMotorTorque = Mathf.Lerp(motorTorque, 0, speedFactor);
        float currentSteerRange = Mathf.Lerp(steeringRange, steeringRangeAtMaxSpeed, speedFactor);

        bool isAccelerating = (currentThrottle > 0 && forwardSpeed >= -0.1f) ||
                              (currentThrottle < 0 && forwardSpeed <= 0.1f);

        foreach (var wheel in wheels)
        {
            // Steering
            if (wheel.steerable)
                wheel.WheelCollider.steerAngle = hInput * currentSteerRange;

            // Acceleration / braking
            if (isAccelerating && !isDecayingVelocity) // <-- Prevent acceleration during decay
            {
                if (wheel.motorized)
                    wheel.WheelCollider.motorTorque = currentThrottle * currentMotorTorque;

                wheel.WheelCollider.brakeTorque = 0f;
            }
            else
            {
                wheel.WheelCollider.motorTorque = 0f;
                wheel.WheelCollider.brakeTorque = Mathf.Abs(currentThrottle) * brakeTorque;
            }

            // Extra brake with SPACE
            if (Keyboard.current.spaceKey.isPressed)
            {
                wheel.WheelCollider.brakeTorque = brakeTorque * 2f;
            }
        }
    }

    // ---- BOOST FUNCTIONS ----
    public void ActivateSpeedBoost(float duration)
    {
        boostDuration = duration;
        boostTimeRemaining = duration;

        // Stop only the boost coroutine to avoid interfering with other coroutines
        StopCoroutine("SpeedBoostCoroutine");
        StartCoroutine(SpeedBoostCoroutine());
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        float originalMaxSpeed = maxSpeed;
        float originalMotorTorque = motorTorque;

        // Apply boost
        maxSpeed *= 5f;
        motorTorque *= 2f;

        // Countdown while boost active
        while (boostTimeRemaining > 0f)
        {
            boostTimeRemaining -= Time.deltaTime;
            yield return null;
        }

        // Prevent acceleration during velocity decay
        isDecayingVelocity = true;

        // Gradually decay velocity over 1 second
        float decelDuration = 1f;
        Vector3 startVelocity = rigidBody.linearVelocity;
        Vector3 targetVelocity = transform.forward * Mathf.Clamp(Vector3.Dot(transform.forward, rigidBody.linearVelocity), -originalMaxSpeed, originalMaxSpeed);

        float t = 0f;
        while (t < decelDuration)
        {
            rigidBody.linearVelocity = Vector3.Lerp(startVelocity, targetVelocity, t / decelDuration);
            t += Time.deltaTime;
            yield return null;
        }

        rigidBody.linearVelocity = targetVelocity;
        isDecayingVelocity = false; // Allow acceleration again

        // Revert car properties
        maxSpeed = originalMaxSpeed;
        motorTorque = originalMotorTorque;
        boostTimeRemaining = 0f;
    }

    private void ResetCar()
    {
        rigidBody.linearVelocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
    }
}