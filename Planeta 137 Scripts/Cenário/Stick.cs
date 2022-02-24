using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    public Animator passage;
    public Animator bossPassage;

    private int countStick = 0;

    private bool interact;
    public Animator[] stick;

    
    private void Update()
    {

        if (Input.GetAxis("Interaction Button") > 0 || Input.GetKeyDown(KeyCode.E))
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
            for (int i = 0; i < stick.Length; i++)
            {
                if (stick[i] != null)
                {
                    float distance = Vector3.Distance(PlayerController.instance.transform.position, stick[i].transform.position);
                    if (distance < 2f)
                    {
                        stick[i].SetTrigger("Active");
                        PushStick();
                        stick[i] = null;
                        GameManager.instance.ActivePassage();
                    }
                }

            }
        }
    }
    
    public void PushStick()
    {
        countStick++;
        switch (countStick)
        {
            case 1:
                passage.SetTrigger("Active1");
                break;
            case 2:
                passage.SetTrigger("Active2");
                break;
            case 3:
                bossPassage.SetTrigger("Active");
                break;
            default:
                break;
        }
    } 
}
