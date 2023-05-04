using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TeleportToStreet : MonoBehaviour
{
    public GameObject interactionMessage;
    public GameObject Player;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactionMessage.activeSelf) 
        {
            Debug.Log("telep");
            interactionMessage.SetActive(false);

            Player.transform.position = Player.GetComponent<PlayerMovement>().pos.position;
            Player.GetComponent<PlayerMovement>().gravity = 1;
            Player.GetComponent<Rigidbody2D>().gravityScale = Player.GetComponent<PlayerMovement>().gravity;
        }
    }
}
