using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll) {
        if (coll.tag == "Player") {
            MySoundManager.PlaySound("key");
            Destroy(gameObject);
            Player.keys++;
        }
    }
}
