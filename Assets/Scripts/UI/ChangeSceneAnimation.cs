using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneAnimation : MonoBehaviour
{
    private Animator animator;
    
    private void OnEnable()
    {
        MainMenuButton.OnUIButtonClick += StartChangeSceneAnimation;
    }
    
     private void OnDisable()
    {
        MainMenuButton.OnUIButtonClick -= StartChangeSceneAnimation;
    }

    void Start()
    {
        animator = transform.gameObject.GetComponent<Animator>();
    }

    private void StartChangeSceneAnimation(string temp)
    {
        animator.SetBool("animateOut", true); 
    }
}
