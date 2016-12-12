using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {
    public CharacterController Player1Collider;
    public CharacterController Player2Collider;

    // Use this for initialization
    void Awake () {
        Physics.IgnoreCollision(Player1Collider, Player2Collider);
    }

}
