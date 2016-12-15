using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour {
    public Transform player;
   
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name.Contains("Player"))
        {
            AudioSource playerHitAudioSource = GameObject.Find("gancho_acertou_jogador audio").GetComponent<AudioSource>();
            playerHitAudioSource.Play();

            PlayerController characterController = collision.gameObject.GetComponent<PlayerController>();
            Vector3 hookingVector = Vector3.Normalize(player.transform.position - collision.transform.position);
            characterController.Hooked(hookingVector);
            player.GetComponent<PlayerController>().DestroyHook();
        }
        else {
            AudioSource sceneryHitAudioSource = GameObject.Find("gancho_acertou_plataforma audio").GetComponent<AudioSource>();
            sceneryHitAudioSource.Play();
        }
    }
}
