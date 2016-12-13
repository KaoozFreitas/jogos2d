using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour {
    public Transform player;
   
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name.Contains("Player")) {
            PlayerController characterController = collision.gameObject.GetComponent<PlayerController>();
            Vector3 hookingVector = Vector3.Normalize(player.transform.position - collision.transform.position);
            characterController.Hooked(hookingVector);
            player.GetComponent<PlayerController>().DestroyHook();
        }
    }
}
