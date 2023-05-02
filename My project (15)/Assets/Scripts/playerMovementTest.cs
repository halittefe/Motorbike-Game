using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovementTest : MonoBehaviour
{
    private Vector3 MoveForce;
    public MobileJoystick joystick;
    public float MoveSpeed = 50f;
    public float MaxSpeed = 15;
    public float drag = .98f;
    public float SteerAngle = 20f;
    public float Traction = 2f;
    public float allowDrift = 20f;
    public float driftFactor = 0.1f;
    private float startTime;
    private bool isDragging;

    GameManager GameManager;
    private void Update()
    {
        GameManager GameManager = FindObjectOfType<GameManager>();
        // Joystick Input
        Vector3 joystickInput = new Vector3(joystick.GetAxes().x, 0f, joystick.GetAxes().y);

        // Calculate angle between player forward and joystick input
        float angle = Vector3.SignedAngle(transform.forward, joystickInput.normalized, Vector3.up);

        // Rotate player based on angle
        if (joystickInput.normalized != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(joystickInput.normalized, Vector3.up);
            float rotationSpeed = Mathf.Clamp01(Mathf.Abs(angle) / allowDrift);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            GameManager.frontAnim.SetBool("isRotate", true);
            GameManager.backAnim.SetBool("isRotate", true);
        }
        else
        {
            GameManager.frontAnim.SetBool("isRotate", false);   
            GameManager.backAnim.SetBool("isRotate", false);
        }


        // Moving
        if (joystickInput.magnitude > 0.1f)
        {
            // If the joystick input is moving, start counting time
            if (!isDragging)
            {
                startTime = Time.time;
                isDragging = true;
            }

            float timeDragged = Time.time - startTime;
            float driftAmount = Mathf.Clamp01(driftFactor * timeDragged);

            MoveForce += transform.forward * MoveSpeed * Time.deltaTime;
            transform.position += MoveForce * Time.deltaTime;

            // Calculate the rotation amount based on the angle between the joystick input and player's forward direction
            float rotationAmount = Mathf.Clamp(angle / allowDrift, -1f, 1f);

            // Rotate the player around the y-axis
            transform.Rotate(Vector3.up * rotationAmount * SteerAngle * MoveForce.magnitude * Time.deltaTime, Space.World);

            // Apply drag to the MoveForce vector
            MoveForce *= drag;

            // Clamp the magnitude of the MoveForce vector
            MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

            // Apply traction to the MoveForce vector
            MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction * Time.deltaTime) * MoveForce.magnitude;

            // Apply drift force to the MoveForce vector
            MoveForce += transform.right * rotationAmount * driftAmount * MoveSpeed * Time.deltaTime;
        }
        else
        {
            // If the joystick input is not moving, stop counting time
            isDragging = false;
        }
    }
}
