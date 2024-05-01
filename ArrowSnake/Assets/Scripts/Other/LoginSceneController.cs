using System;
using UnityEngine;

public class LoginSceneController : MonoBehaviour
{
    Animator animator;
    int que = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        string name = this.gameObject.name;
        que = Int16.Parse(name);
    }


    void Update()
    {
        
        InvokeRepeating(nameof(Anim), que/10 , .5f);
    }

    void Anim()
    {
        animator.SetTrigger("shakebody");
    }
}
