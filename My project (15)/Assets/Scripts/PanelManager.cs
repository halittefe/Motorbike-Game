using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PanelManager : MonoBehaviour
{
    public GameObject nextPanel;
    public Button nextButton;
    public string nextSceneName;
    public GameObject ParticleEffect;
    

    GameManager GameManager;
    // Start is called before the first frame update
    
    
    
    void Start()
    {
        
        nextPanel.SetActive(false);
        ParticleEffect.SetActive(false);
        

    }

    // Update is called once per frame
   

    public void LoadNextScene()
    {   

        SceneManager.LoadScene(nextSceneName);
       
        
    }

   
    


}
