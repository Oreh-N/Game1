using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCloud : MonoBehaviour
{
    public Animator[] clouds;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Animator anim in clouds)
            { anim.SetBool("popUp", true); }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Animator anim in clouds)
            { anim.SetBool("popUp", false); }
        }
    }
}