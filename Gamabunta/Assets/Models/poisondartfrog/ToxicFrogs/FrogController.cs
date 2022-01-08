using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{

    Animator anim;

    public GameObject guts;
    GameObject gutsEx;
    bool smashed = false;
    public float pauseFor = 1f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
        // for (int i = 0; i < 100; i++) {
        //     Invoke("Tongue", i*pauseFor);
        // }
    }

    void Update()
    {
        Crawl();
    }

    private void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Player" && GetComponent<FinalBoss>().enabled) {
            coll.GetComponent<Player>().TakeDamage(10);
        }
    }

    public void Idle()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Idle");
    }

    public void Jump()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Jump");
    }

    public void Crawl()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Crawl");
    }

    public void Tongue()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Tongue");
    }

    public void Swim()
    {
        RootMotion();
        DestroyGuts();
        anim.SetTrigger("Swim");
    }

    public void Smashed()
    {
        RootMotion();
        // DestroyGuts();
        anim.SetTrigger("Smashed");
        Guts();
    }

    public void TurnLeft()
    {
        anim.applyRootMotion = true;
        DestroyGuts();
        anim.SetTrigger("TurnLeft");
    }

    public void TurnRight()
    {
        anim.applyRootMotion = true;
        DestroyGuts();
        anim.SetTrigger("TurnRight");
    }


    private bool won = false;
    public void Guts()
    {
        if (!won)
        {
            Invoke("SpreadGuts", 0.1f);
            won = true;
        }
    }

    void SpreadGuts()
    {
        smashed = false;
        if (!smashed)
        {
            Instantiate(guts, transform.position, transform.rotation);
            smashed = true;
        }
    }

    void RootMotion()
    {
        if (anim.applyRootMotion)
        {
            anim.applyRootMotion = false;
        }
    }


    void DestroyGuts()
    {
        gutsEx = GameObject.FindGameObjectWithTag("Guts");
        if (gutsEx != null)
        {
            Destroy(gutsEx);
            smashed = false;
        }
    }
}