using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private CharacterController cc;
    [SerializeField] private MobileJoystick joystick;

    [Header("Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float smoothTime = .05f;
    [SerializeField] private float gravity = 9.81f;

    private Vector3 velocity = Vector3.zero;
    private Quaternion originalRotation;
    private float currentVelocity;

    public GameObject onTeker;
    

    private void Start()
    {
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
        
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(joystick.GetAxes().x, 0, joystick.GetAxes().y);
        if (!cc.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
            cc.Move(velocity * Time.deltaTime);
        }
        if (movement.magnitude >= 0.2f)
        {
            movement *= moveSpeed;
            cc.Move(movement * Time.deltaTime);
        }
    }

    private void RotatePlayer()
    {
        Vector3 movement = new Vector3(joystick.GetAxes().x, 0, joystick.GetAxes().y);
        if (movement.magnitude >= 0.2f)
        {
            transform.rotation = Quaternion.Euler(0f, GetTargetRotationAngle(), 0f);
        }
    }

    private float GetTargetRotationAngle()
    {
        float angleX = joystick.GetAxes().x;
        float angleY = joystick.GetAxes().y;
        if (angleX == 0 && angleY == 0)
        {
            return transform.eulerAngles.y;
        }
        float targetAngle = Mathf.Atan2(angleX, angleY) * Mathf.Rad2Deg;
        return Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
    }
}
