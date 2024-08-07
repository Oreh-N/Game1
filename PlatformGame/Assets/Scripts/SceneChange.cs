using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : Interactable
{
    public string nxtSceneName;
    private Animator anim;

    private bool canBeOpen;


    //private void Start()
    //{ anim.GetComponent<Animator>(); }

    public override void Interact()
    {
        SceneManager.LoadSceneAsync(nxtSceneName);
        //Debug.Log("WE ARE TRYING TO INTERACT");
        //anim.SetTrigger("Close");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadSceneAsync(nxtSceneName);
    }
}
