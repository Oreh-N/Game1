using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator anim;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { Destroy(gameObject); }
    }

    public void NextLevel()
    {
        //anim.SetTrigger("Close");
        //yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //anim.SetTrigger("Open");
    }

    public void LoadLevel(string sceneName)
    {
        //anim.SetTrigger("Close");
        //yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneName);
        //anim.SetTrigger("Open");
    }
}
