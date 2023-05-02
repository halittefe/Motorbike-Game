using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float smoothTime = 0.3f;
    public float maxDistance = 100f;
    public float holeSize = 2f;
    public Vector3 cameraOffset;
    public string buildingTag = "Building";


    private Camera cam;
    private Vector3 velocity = Vector3.zero;
    private bool isPlayerVisible;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        // Calculate the target position with the offset
        Vector3 targetPosition = player.transform.position + cameraOffset;

        // Move the camera to follow the target position smoothly
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // Check if the player is visible, and create a hole in the building if necessary
        Vector3 dir = player.transform.position - transform.position;
        RaycastHit[] hits = Physics.RaycastAll(transform.position, player.transform.position - transform.position, Vector3.Distance(transform.position, player.transform.position));
        foreach (RaycastHit hit in hits)
        {
            Renderer obstacleRenderer = hit.collider.GetComponent<Renderer>();
            if (obstacleRenderer != null)
            {
                if (hit.collider.CompareTag(buildingTag))
                {
                    Material buildingMaterial = obstacleRenderer.material;
                    Color buildingColor = buildingMaterial.color;
                    buildingColor.a = .2f;
                    buildingMaterial.color = buildingColor;
                }
                else
                {   //reset the values
                    obstacleRenderer.material.color = new Color(obstacleRenderer.material.color.r, obstacleRenderer.material.color.g, obstacleRenderer.material.color.b, 1.0f);
                }
            }
        }
    }
}
