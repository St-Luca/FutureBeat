using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueMessageController : MonoBehaviour
{
    public GameObject dialogueMessageImage;


    void Start()
    {
        dialogueMessageImage.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            dialogueMessageImage.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            dialogueMessageImage.SetActive(false);
        }
    }
}
