using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    [SerializeField] bool goNextLevel;
    [SerializeField] string levelName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (goNextLevel)
            { SceneController.instance.NextLevel(); }
            else
            { SceneController.instance.LoadLevel(levelName); }
        }
    }
}
