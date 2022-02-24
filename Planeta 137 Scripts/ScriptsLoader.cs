using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptsLoader : MonoBehaviour
{

    private GameObject[] enemys;
    private GameObject player;
    [SerializeField] private float minDist;

    void Start()
    {
        enemys = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            if (enemys[i] != null && player != null)
            {
                float dist = Vector3.Distance(player.transform.position, enemys[i].transform.position);
                if (dist > minDist)
                {
                    enemys[i].SetActive(false);
                }
                else
                {
                    enemys[i].SetActive(true);
                }
            }
        }
    }
}
