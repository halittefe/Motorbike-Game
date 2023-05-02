using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildGas : MonoBehaviour
{
    public Image image;
    public Image BgImage;
    public float fillSpeed = 0.5f;
    public GameObject buildingPrefab;



    public float buildingScale = 0.7f;

    private float fillAmount = 0f;
    private bool buildingCreated = false;

    public int MarketBuildfee = 100;
    public int GasStationBuildFee = 100;
    private GameManager gameMng;

    public Transform SpawnPoint;


    private void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        gameMng = gameManager.GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FillImage());

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StopAllCoroutines();
            StartCoroutine(EmptyImage());
            buildingCreated = true;


        }
    }

    private IEnumerator FillImage()
    {
        while (fillAmount < 1f)
        {
            fillAmount += fillSpeed * Time.deltaTime;
            image.fillAmount = fillAmount;
            yield return null;
        }

        if (!buildingCreated)
        {
            GameObject newBuilding = Instantiate(buildingPrefab, transform.position, Quaternion.Euler(0f, 180f, 0f));
            newBuilding.transform.localScale = Vector3.one * buildingScale;
            buildingCreated = true;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = SpawnPoint.position;


            Destroy(BgImage.gameObject);
            gameMng.goldprice -= MarketBuildfee;
            gameMng.goldpricetext.text = gameMng.goldprice + "";
        }

    }

    private IEnumerator EmptyImage()
    {
        while (fillAmount > 0f)
        {
            fillAmount -= fillSpeed * Time.deltaTime;
            image.fillAmount = fillAmount;
            yield return null;
        }

        buildingCreated = false;
    }
}




