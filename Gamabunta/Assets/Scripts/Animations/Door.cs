using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Player") {
            animator.SetBool("isOpen", true);
        }
    }
}
