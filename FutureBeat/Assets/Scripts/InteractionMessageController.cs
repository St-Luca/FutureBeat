using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionMessageController : MonoBehaviour
{
    public GameObject interactionMessageImage;

    void Start()
    {
        interactionMessageImage.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactionMessageImage.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            interactionMessageImage.SetActive(false);
        }
    }
}
