using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //private bool isPaused = false;
    //private bool isBack = false;
    public int deadEnemyCounter;
    public int totalEnemyCount;
    public GameObject MainPanel;
    public GameObject PauseButton;
    public GameObject PausePanel;
    public GameObject LeaveButton;
    public GameObject ResumeButton; 
    public GameObject DragZone;
    public GameObject ShopPanel;
    public GameObject RestartPanel;
    public Button RestartButton;
    public Image HealthBarSprite;

    //public TextMeshProUGUI buybuttonText;
    public TextMeshProUGUI blueSlot;
    public TextMeshProUGUI greenSlot;
    public TextMeshProUGUI purpleSlot;
    public TextMeshProUGUI blueHatSlot;
    public TextMeshProUGUI greenHatSlot;
    public TextMeshProUGUI purpleHatSlot;
    public TextMeshProUGUI blueJacketSlot;
    public TextMeshProUGUI greenJacketSlot;
    public TextMeshProUGUI purpleJacketSlot;
    public TextMeshProUGUI goldpricetext;
    public TextMeshProUGUI gempricetext;

    private string blueSlotStatus = "Buy";
    private string greenSlotStatus = "Buy"; 
    private string purpleSlotStatus = "Buy";
    private string blueHatSlotStatus = "Buy";
    private string greenHatSlotStatus = "Buy";
    private string purpleHatSlotStatus = "Buy"; 
    private string blueJacketSlotStatus = "Buy";
    private string greenJacketSlotStatus = "Buy";
    private string purpleJacketSlotStatus = "Buy";
   
    public GameObject blueGun;
    public GameObject greenGun;
    public GameObject purpleGun;
    public GameObject blueHat;
    public GameObject greenHat;
    public GameObject purpleHat; 
    public GameObject blueJacket;
    public GameObject greenJacket;
    public GameObject purpleJacket;

    public Image Gunbluegold;
    public Image Gungreengold;
    public Image Gunpurplegem;
    public Image Hatbluegold;
    public Image Hatgreengold;
    public Image Hatpurplegem;
    public Image Jacketbluegold;
    public Image Jacketgreengold;
    public Image Jacketpurplegem;

    public int pricebluegun = 500;
    public int pricegreengun = 1000;
    public int pricepurplegun = 400;
    public int pricebluehat = 400;
    public int pricegreenhat = 800;
    public int pricepurplehat = 300;
    public int pricebluejacket = 600;
    public int pricegreenjacket = 1200;
    public int pricepurplejacket = 500;
    public int goldprice;
    public int gemprice;

    public bool isPistol = false;
    public bool isRevolver = false;
    public bool isSmg = false;

    public Animator frontAnim;
    public Animator backAnim;
    public static GameManager Instance { get; private set; }

    public void Awake()
    {
        goldprice = PlayerPrefs.GetInt("goldprice", goldprice);
        gemprice = PlayerPrefs.GetInt("gemprice", gemprice);
    }
    private void Start()
    {
        OyunBasladi();
        
    }

    public void Update()
    {
        goldpricetext.text = goldprice + "";
        gempricetext.text = gemprice + "";
        PlayerPrefs.SetInt("goldprice", goldprice);
        PlayerPrefs.SetInt("gemprice", gemprice);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        totalEnemyCount = enemies.Length;

        
    }

    public void AddGold(int amount)
    {
        goldprice += amount;
    }

    public void TogglePause()
    {
        Time.timeScale = 0f;
        PausePanel.SetActive(true);
        PauseButton.SetActive(false);
        DragZone.SetActive(false);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
        DragZone.SetActive(true);
    }

    void OyunBasladi()
    {
        PausePanel.SetActive(false);
        ShopPanel.SetActive(false);
        RestartPanel.SetActive(false);
        MainPanel.SetActive(true);

        blueSlotStatus = "Buy";
        greenSlotStatus = "Buy";
        purpleSlotStatus = "Buy";
        blueHatSlotStatus = "Buy";
        greenHatSlotStatus = "Buy";
        purpleHatSlotStatus = "Buy";
        blueJacketSlotStatus = "Buy";
        greenJacketSlotStatus = "Buy";
        purpleJacketSlotStatus = "Buy";
        //default gun set
        blueGun.SetActive(true);
        greenGun.SetActive(false);
        purpleGun.SetActive(false);
        blueHat.SetActive(false);
        greenHat.SetActive(false);
        purpleHat.SetActive(false);
        blueJacket.SetActive(false);
        greenJacket.SetActive(false);
        purpleJacket.SetActive(false);
    }

    /*public void SettingsEkran()
    {
        //isBack = !isBack;
        PausePanel.SetActive(false);
        SettingsPanel.SetActive(true);
        DragZone.SetActive(false);

    }*/
    public void PauseEkran()
    {
        PausePanel.SetActive(true);
        
        DragZone.SetActive(false);

    }

    public void Quit()
    {
        Application.Quit(); // Oyundan cikmak icin Application.Quit metodu kullanilir.
    }

    public void Exit()
    {
        ShopPanel.SetActive(false);
        Time.timeScale = 1f;
    }

   

    
   

    // FOR GUN
    public void OnClickBlueSlot()
    {
       

        if (blueSlotStatus == "Buy" && goldprice >= pricebluegun  )
        {
            
            goldprice -= pricebluegun;
            goldpricetext.text = goldprice + "";
            blueSlotStatus = "equip";
            blueSlot.text = "Equip";
            Gunbluegold.enabled = false;
            
        }
        else if (blueSlotStatus == "equip")
        {
            
            isPistol = true;
            isRevolver = false;
            isSmg = false;

            Debug.Log("Pistol");
            if (greenSlotStatus == "equipped")
            {
                blueSlotStatus = "equipped";
                blueSlot.text = "Equipped";
                greenSlotStatus = "equip";
                greenSlot.text = "Equip";

               
                blueGun.SetActive(true);
                greenGun.SetActive(false);
                purpleGun.SetActive(false);

                

            }
            else if (purpleSlotStatus == "equipped")
            {
                blueSlotStatus = "equipped";
                blueSlot.text = "Equipped";
                purpleSlotStatus = "equip";
                purpleSlot.text = "Equip";


                blueGun.SetActive(true);
                greenGun.SetActive(false);
                purpleGun.SetActive(false);

               
            }
            
           
            
            else
            {
               
                blueSlotStatus = "equipped";
                blueSlot.text = "Equipped";

                blueGun.SetActive(true);
                greenGun.SetActive(false);
                purpleGun.SetActive(false);

                

            }
        }
        else if (blueSlotStatus == "equipped")
        {
            blueSlot.text = "Equip";
            blueSlotStatus = "equip";

            isPistol = false;
            blueGun.SetActive(false);

            
            
        }
        
    }

    public void OnClickGreenSlot()
    {
        


        if (greenSlotStatus == "Buy" && goldprice >= pricegreengun)
        {
            goldprice -= pricegreengun;
            goldpricetext.text = goldprice + "";
            greenSlotStatus = "equip";
            greenSlot.text = "Equip";
            Gungreengold.enabled = false;

        }
        else if (greenSlotStatus == "equip")
        {

            isPistol = false;
            isRevolver = true;
            isSmg = false;
            Debug.Log("Revolver");

            if (blueSlotStatus == "equipped")
            {
                greenSlotStatus = "equipped";
                greenSlot.text = "Equipped";
                blueSlotStatus = "equip";
                blueSlot.text = "Equip";
                greenGun.SetActive(true);
                blueGun.SetActive(false);
                purpleGun.SetActive(false);

                
            }
            else if (purpleSlotStatus == "equipped")
            {
                greenSlotStatus = "equipped";
                greenSlot.text = "Equipped";
                purpleSlotStatus = "equip";
                purpleSlot.text = "Equip";
                
                greenGun.SetActive(true);
                blueGun.SetActive(false);
                purpleGun.SetActive(false);

                
            }
            else
            {
                greenSlotStatus = "equipped";
                greenSlot.text = "Equipped";
                greenGun.SetActive(true);
                blueGun.SetActive(false);
                purpleGun.SetActive(false);

               
            }
        }
        else if (greenSlotStatus == "equipped")
        {
            greenSlot.text = "Equip";
            greenSlotStatus = "equip";
            greenGun.SetActive(false);

            isRevolver = false;
        }
    }

    public void OnClickPurpleSlot()
    {
        


        if (purpleSlotStatus == "Buy" && gemprice >= pricepurplegun)
        {
            gemprice -= pricepurplegun;
            gempricetext.text = goldprice + "";
            purpleSlotStatus = "equip";
            purpleSlot.text = "Equip";
            Gunpurplegem.enabled = false;

        }
        else if (purpleSlotStatus == "equip")
        {

            isPistol = false;
            isRevolver = false;
            isSmg = true;
            Debug.Log("Uzi");
            if (blueSlotStatus == "equipped")
            {
                purpleSlotStatus = "equipped";
                purpleSlot.text = "Equipped";
                blueSlotStatus = "equip";
                blueSlot.text = "Equip";
                purpleGun.SetActive(true);
                blueGun.SetActive(false);
                greenGun.SetActive(false);


            }
            else if (greenSlotStatus == "equipped")
            {
                purpleSlotStatus = "equipped";
                purpleSlot.text = "Equipped";
                greenSlotStatus = "equip";
                greenSlot.text = "Equip";
                purpleGun.SetActive(true);
                blueGun.SetActive(false);
                greenGun.SetActive(false);

                

            }
            else
            {
                purpleSlotStatus = "equipped";
                purpleSlot.text = "Equipped";
                purpleGun.SetActive(true);
                blueGun.SetActive(false);
                greenGun.SetActive(false);

                
            }
        }
        else if (purpleSlotStatus == "equipped")
        {
            purpleSlot.text = "Equip";
            purpleSlotStatus = "equip";
            purpleGun.SetActive(false);

            isSmg = false;
        }
    }
    
    // FOR HAT
    public void OnClickBlueHatSlot()
    {
        if (blueHatSlotStatus == "Buy" && goldprice >= pricebluehat)
        {
            goldprice -= pricebluehat;
            goldpricetext.text = goldprice + "";
            blueHatSlotStatus = "equip";
            blueHatSlot.text = "Equip";
            Hatbluegold.enabled = false;

            
        }
        else if (blueHatSlotStatus == "equip")
        {
            if (greenHatSlotStatus == "equipped")
            {
                blueHatSlotStatus = "equipped";
                blueHatSlot.text = "Equipped";
                greenHatSlotStatus = "equip";
                greenHatSlot.text = "Equip";
               
                
                blueHat.SetActive(true);
                greenHat.SetActive(false);
                purpleHat.SetActive(false);
            }
            else if (purpleHatSlotStatus == "equipped")
            {
                blueHatSlotStatus = "equipped";
                blueHatSlot.text = "Equipped";
                purpleHatSlotStatus = "equip";
                purpleHatSlot.text = "Equip";
               
                
                blueHat.SetActive(true);
                greenHat.SetActive(false);
                purpleHat.SetActive(false);
            }

            else
            {
                blueHatSlotStatus = "equipped";
                blueHatSlot.text = "Equipped";
                
                
                blueHat.SetActive(true);
                greenHat.SetActive(false);
                purpleHat.SetActive(false);
            }
        }
        else if (blueHatSlotStatus == "equipped")
        {
            blueHatSlot.text = "Equip";
            blueHatSlotStatus = "equip";
            blueHat.SetActive(false);
            
        }
    }

    public void OnClickGreenHatSlot()
    {
        if (greenHatSlotStatus == "Buy" && goldprice >= pricegreenhat)
        {
            goldprice -= pricegreenhat;
            goldpricetext.text = goldprice + "";
            greenHatSlotStatus = "equip";
            greenHatSlot.text = "Equip";
            Hatgreengold.enabled = false;

        }
        else if (greenHatSlotStatus == "equip")
        {
            if (blueHatSlotStatus == "equipped")
            {
                greenHatSlotStatus = "equipped";
                greenHatSlot.text = "Equipped";
                blueHatSlotStatus = "equip";
                blueHatSlot.text = "Equip";
               
                blueHat.SetActive(false);
                greenHat.SetActive(true);
                purpleHat.SetActive(false);
            }
            else if (purpleHatSlotStatus == "equipped")
            {
                greenHatSlotStatus = "equipped";
                greenHatSlot.text = "Equipped";
                purpleHatSlotStatus = "equip";
                purpleHatSlot.text = "Equip";
                
                
                blueHat.SetActive(false);
                greenHat.SetActive(true);
                purpleHat.SetActive(false);
            }
            else
            {
                greenHatSlotStatus = "equipped";
                greenHatSlot.text = "Equipped";
                
                blueHat.SetActive(false);
                greenHat.SetActive(true);
                purpleHat.SetActive(false);
            }
        }
        else if (greenHatSlotStatus == "equipped")
        {
            greenHatSlot.text = "Equip";
            greenHatSlotStatus = "equip";
            greenHat.SetActive(false);
        }
    }

    public void OnClickPurpleHatSlot()
    {
        if (purpleHatSlotStatus == "Buy" && gemprice >= pricepurplehat)
        {
            gemprice -= pricepurplehat;
            gempricetext.text = gemprice + "";
            purpleHatSlotStatus = "equip";
            purpleHatSlot.text = "Equip";
            Hatpurplegem.enabled = false;

        }
        else if (purpleHatSlotStatus == "equip")
        {
            if (blueHatSlotStatus == "equipped")
            {
                purpleHatSlotStatus = "equipped";
                purpleHatSlot.text = "Equipped";
                blueHatSlotStatus = "equip";
                blueHatSlot.text = "Equip";
               
                blueHat.SetActive(false);
                greenHat.SetActive(false);
                purpleHat.SetActive(true);

            }
            else if (greenHatSlotStatus == "equipped")
            {
                purpleHatSlotStatus = "equipped";
                purpleHatSlot.text = "Equipped";
                greenHatSlotStatus = "equip";
                greenHatSlot.text = "Equip";
                
                blueHat.SetActive(false);
                greenHat.SetActive(false);
                purpleHat.SetActive(true);

            }
            else
            {
                purpleHatSlotStatus = "equipped";
                purpleHatSlot.text = "Equipped";
               
                blueHat.SetActive(false);
                greenHat.SetActive(false);
                purpleHat.SetActive(true);

            }
        }
        else if (purpleHatSlotStatus == "equipped")
        {
            purpleHatSlot.text = "Equip";
            purpleHatSlotStatus = "equip";
            purpleHat.SetActive(false);
        }
    }

    // FOR JACKET
    public void OnClickBluejacketSlot()
    {
        if (blueJacketSlotStatus == "Buy" && goldprice >= pricebluejacket)
        {
            goldprice -= pricebluejacket;
            goldpricetext.text = goldprice + "";
            blueJacketSlotStatus = "equip";
            blueJacketSlot.text = "Equip";
            Jacketbluegold.enabled = false;

            
        }
        else if (blueJacketSlotStatus == "equip")
        {
            if (greenJacketSlotStatus == "equipped")
            {
                blueJacketSlotStatus = "equipped";
                blueJacketSlot.text = "Equipped";
                greenJacketSlotStatus = "equip";
                greenJacketSlot.text = "Equip";
                
                
                blueJacket.SetActive(true);
                greenJacket.SetActive(false);
                purpleJacket.SetActive(false);
            }
            else if (purpleJacketSlotStatus == "equipped")
            {
                blueJacketSlotStatus = "equipped";
                blueJacketSlot.text = "Equipped";
                purpleJacketSlotStatus = "equip";
                purpleJacketSlot.text = "Equip";
               
               
                blueJacket.SetActive(true);
                greenJacket.SetActive(false);
                purpleJacket.SetActive(false);
            }

            else
            {
                blueJacketSlotStatus = "equipped";
                blueJacketSlot.text = "Equipped";
                
                
                blueJacket.SetActive(true);
                greenJacket.SetActive(false);
                purpleJacket.SetActive(false);
            }
        }
        else if (blueJacketSlotStatus == "equipped")
        {
            blueJacketSlot.text = "Equip";
            blueJacketSlotStatus = "equip";
            blueJacket.SetActive(false);
            
        }
    }

    public void OnClickGreenJacketSlot()
    {
        if (greenJacketSlotStatus == "Buy" && goldprice >= pricegreenjacket)
        {
            goldprice -= pricegreenjacket;
            goldpricetext.text = goldprice + "";
            greenJacketSlotStatus = "equip";
            greenJacketSlot.text = "Equip";
            Jacketgreengold.enabled = false;

        }
        else if (greenJacketSlotStatus == "equip")
        {
            if (blueJacketSlotStatus == "equipped")
            {
                greenJacketSlotStatus = "equipped";
                greenJacketSlot.text = "Equipped";
                blueJacketSlotStatus = "equip";
                blueJacketSlot.text = "Equip";
                
                blueJacket.SetActive(false);
                greenJacket.SetActive(true);
                purpleJacket.SetActive(false);
            }
            else if (purpleJacketSlotStatus == "equipped")
            {
                greenJacketSlotStatus = "equipped";
                greenJacketSlot.text = "Equipped";
                purpleJacketSlotStatus = "equip";
                purpleJacketSlot.text = "Equip";
                
               
                blueJacket.SetActive(false);
                greenJacket.SetActive(true);
                purpleJacket.SetActive(false);
            }
            else
            {
                greenJacketSlotStatus = "equipped";
                greenJacketSlot.text = "Equipped";
                
                blueJacket.SetActive(false);
                greenJacket.SetActive(true);
                purpleJacket.SetActive(false);
            }
        }
        else if (greenJacketSlotStatus == "equipped")
        {
            greenJacketSlot.text = "Equip";
            greenJacketSlotStatus = "equip";
            greenJacket.SetActive(false);
        }
    }

    public void OnClickPurpleJacketSlot()
    {
        if (purpleJacketSlotStatus == "Buy" && gemprice >= pricepurplejacket)
        {
            gemprice -= pricepurplejacket;
            gempricetext.text = gemprice + "";
            purpleJacketSlotStatus = "equip";
            purpleJacketSlot.text = "Equip";
            Jacketpurplegem.enabled = false;

        }
        else if (purpleJacketSlotStatus == "equip")
        {
            if (blueJacketSlotStatus == "equipped")
            {
                purpleJacketSlotStatus = "equipped";
                purpleJacketSlot.text = "Equipped";
                blueJacketSlotStatus = "equip";
                blueJacketSlot.text = "Equip";
                
                blueJacket.SetActive(false);
                greenJacket.SetActive(false);
                purpleJacket.SetActive(true);

            }
            else if (greenJacketSlotStatus == "equipped")
            {
                purpleJacketSlotStatus = "equipped";
                purpleJacketSlot.text = "Equipped";
                greenJacketSlotStatus = "equip";
                greenJacketSlot.text = "Equip";
                
                blueJacket.SetActive(false);
                greenJacket.SetActive(false);
                purpleJacket.SetActive(true);

            }
            else
            {
                purpleJacketSlotStatus = "equipped";
                purpleJacketSlot.text = "Equipped";
                
                blueJacket.SetActive(false);
                greenJacket.SetActive(false);
                purpleJacket.SetActive(true);

            }
        }
        else if (purpleJacketSlotStatus == "equipped")
        {
            purpleJacketSlot.text = "Equip";
            purpleJacketSlotStatus = "equip";
            purpleJacket.SetActive(false);
        }
    }
}



    
