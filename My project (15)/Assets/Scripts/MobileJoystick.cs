using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileJoystick : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform joystickContour;
    [SerializeField] private RectTransform joystick;
    [Header("Settings")]
    [SerializeField] private float moveFactor;
    private Vector3 clickedPosition;
    private Vector3 move;
    private bool controlling;

    private void Start()
    {
        HideJoystick();
    }

    private void Update()
    {
        if (controlling)
        {
            ControlJoystick();
        }
    }

    public void ClickedOnDragZoneCallback()
    {
        clickedPosition = Input.mousePosition;
        joystickContour.position = clickedPosition;
        joystick.position = clickedPosition;
        //Shows the joystick
        ShowJoystick();
        controlling = true;
    }

    private void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 uDirection = (currentPosition - clickedPosition);

        float moveMagnitude = uDirection.magnitude * (moveFactor / Screen.width);
        moveMagnitude = Mathf.Min(moveMagnitude, joystickContour.rect.width / 2);
        move = uDirection.normalized * moveMagnitude;

        Vector3 targetJoystickPosition = clickedPosition + move;
        joystick.position = targetJoystickPosition;

        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }

    private void ShowJoystick()
    {
        joystickContour.gameObject.SetActive(true);
        //reset move vector
        move = Vector3.zero;
    }

    private void HideJoystick()
    {
        controlling = false;
        joystickContour.gameObject.SetActive(false);
        move = Vector3.zero;
    }

    public Vector3 GetAxes()
    {
        return move;
    }
}
