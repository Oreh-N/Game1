using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : Interactable
{
    public string sceneName;
    private Animator anim;

    private bool canBeOpen;


    private void Start()
    {
        anim.GetComponent<Animator>();
    }

    public override void Interact()
    {
        anim.SetTrigger("Close");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadSceneAsync(sceneName);
        anim.SetTrigger("Open");
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Player"))
    //    {
    //        if (goNextLevel)
    //        { SceneController.instance.NextLevel(); }
    //        else
    //        { SceneController.instance.LoadLevel(levelName); }
    //    }
    //}
}
