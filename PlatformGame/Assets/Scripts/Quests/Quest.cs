using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int questNum;
    public int[] items;
    public GameObject[] clouds;
    public GameObject barrier;
    public GameObject reward;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag != "Player" && coll.tag != "Enemy" && coll.gameObject.GetComponent<PickUp>().id == items[questNum])
        {
            questNum++;
            Destroy(coll.gameObject);
            CheckQuest();
        }
    }

    public void CheckQuest()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            if (i == questNum)
            {
                clouds[i].SetActive(true);
                clouds[i].GetComponent<Animator>().SetTrigger("popUp");
                break;
            }
            else
            {
                clouds[i].SetActive(false);
            }
        }

        if (questNum == 1)
        { barrier.SetActive(false); }

        //if (questNum == 1)
        //{ reward.SetActive(true); }
    }
}
