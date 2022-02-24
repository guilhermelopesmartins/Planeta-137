using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSuplies : MonoBehaviour
{
    public Animator[] suplies;

    private bool interact;

    private void Start()
    {
        suplies = new Animator[transform.childCount];

        for (int i = 0; i < suplies.Length; i++)
        {
            suplies[i] = transform.GetChild(i).GetComponentInChildren<Animator>();
        }
    }

    private void Update()
    {
        if (Input.GetAxis("Interaction Button") > 0  || Input.GetKeyDown(KeyCode.E))
        {
            interact = true;
            Debug.Log("interação");
        }
        else
        {
            interact = false;
        }

        if (interact)
        {
            for (int i = 0; i < suplies.Length; i++)
            {
                if (suplies[i] != null)
                {
                    float distance = Vector3.Distance(PlayerController.instance.transform.position, suplies[i].transform.position);
                    if (distance < 2f)
                    {
                        suplies[i].SetTrigger("Active");
                        suplies[i] = null;
                        return;
                    }
                }

            }
        }
    }

}
