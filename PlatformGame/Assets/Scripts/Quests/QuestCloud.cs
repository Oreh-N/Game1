using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCloud : MonoBehaviour
{
    public Animator[] clouds;
    bool isOpen = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Animator anim in clouds)
            {
                if (anim.gameObject.activeSelf && !isOpen)
                { 
                    anim.SetTrigger("popUp");
                    isOpen = true;
                }
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Animator anim in clouds)
            { 
                if (anim.gameObject.activeSelf && isOpen)
                {
                    anim.SetTrigger("popUp");
                    isOpen = false;
                }
                    
            }
        }
    }
}