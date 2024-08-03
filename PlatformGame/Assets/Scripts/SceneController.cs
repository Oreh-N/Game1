using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnim;


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
        //transitionAnim.SetTrigger("Close");
        //yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        //transitionAnim.SetTrigger("Open");
    }

    public void LoadLevel(string sceneName)
    {
        //transitionAnim.SetTrigger("Close");
        //yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(sceneName);
        //transitionAnim.SetTrigger("Open");
    }
}