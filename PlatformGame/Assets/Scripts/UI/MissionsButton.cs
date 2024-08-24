using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsButton : MonoBehaviour
{
    public GameObject missions;
    private bool missionsOpen;

    private void Start()
    { missionsOpen = true; }

    public void OnClick()
    {
        if (missionsOpen)
        {
            missionsOpen = false;
            missions.SetActive(false);
        }
        else
        { 
            missionsOpen = true;
            missions.SetActive(true);
        }
    }
}
