using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;
    
    private bool isOpen;
    

    //private bool isTarget = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        //playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("isOpen", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("isOpen", false);
        }
    }


}
