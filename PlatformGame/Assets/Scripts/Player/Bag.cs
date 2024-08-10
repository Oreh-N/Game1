using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bag : MonoBehaviour
{
    GameMenu bag = new GameMenu();


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (bag.pauseGame)
            { bag.Resume(); }
            else { bag.Pause(); }
        }
    }
}
