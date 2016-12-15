using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CollisionManager : MonoBehaviour {
    public CharacterController Player1Collider;
    public CharacterController Player2Collider;
    public Text WinnerText;

    // Use this for initialization
    void Awake () {
        Physics.IgnoreCollision(Player1Collider, Player2Collider);
    }

    public void EndGame(string playerName) {
        Player1Collider.enabled = false;
        Player2Collider.enabled = false;

        WinnerText.text = playerName + " venceu!";
        WinnerText.gameObject.SetActive(true);

        GameObject HUDPanel = GameObject.Find("HUD Panel");
        HUDPanel.SetActive(false);
    }
}
