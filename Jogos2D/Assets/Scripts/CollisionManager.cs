using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {
    public Collider2D Player1Collider;
    public Collider2D Player2Collider;

    // Use this for initialization
    void Awake () {
        Physics2D.IgnoreCollision(Player1Collider, Player2Collider);
	}
	
}
