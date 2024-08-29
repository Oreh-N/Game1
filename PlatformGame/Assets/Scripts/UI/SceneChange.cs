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


    public override void Interact()
    {
        SceneManager.LoadSceneAsync(nxtSceneName);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadSceneAsync(nxtSceneName);
    }
}
