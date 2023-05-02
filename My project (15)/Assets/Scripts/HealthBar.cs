using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject healthSlider;
    public Transform Player;

    void LateUpdate()
    {
        transform.position = Player.position + new Vector3(0, 2.5f, 0);
        transform.rotation = Quaternion.identity;
    }

}
