using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : MonoBehaviour
{

    private int countAttack;
    private float attackTime;
    private float delay;
    private bool isAttack;
    [SerializeField] private Animator anim;

    private AudioSource source;
    [SerializeField] private AudioClip airPunch;

    #region errado
    /*
    void Start()
    {
        countAttack = 0;
        delay = 2;
        isAttack = false;

    }

    void Update()
    {
        anim.SetBool("Attack " + countAttack, isAttack);
    
        if (isAttack)
            attackTime += Time.deltaTime;

        if (Input.GetButtonDown("Attack Button"))
        {
            isAttack = true;
            if (attackTime < delay)
            {
                countAttack++;
                attackTime = 0;
            }
        }
        else if (attackTime > delay)
        {
            isAttack = false;
            attackTime = 0;
            countAttack = 0;
            anim.SetTrigger("Reset");
        }
    }
    */
    #endregion

    private void Start()
    {
        countAttack = 0;
        delay = 1;


        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        #region pro via das duvidas
        /*
        switch (countAttack)
        {
            case 1:
                anim.SetBool("Attack 1", true);
                break;
            case 2:
                anim.SetBool("Attack 2", true);
                break;
            case 3:
                anim.SetBool("Attack 3", true);
                break;
            case 4:
                anim.SetBool("Attack 4", true);
                break;
            case 5:
                anim.SetBool("Attack 5", true);
                break;
            case 6:
                anim.SetBool("Attack 6", true);
                break;
            case 7:
                anim.SetBool("Attack 7", true);
                break;
            case 8:
                anim.SetBool("Attack 8", true);
                break;
            case 9:
                anim.SetBool("Attack 9", true);
                break;
            case 10:
                anim.SetBool("Attack 10", true);
                break;
            case 11:
                anim.SetBool("Attack 11", true);
                break;
            default:
                break;
        }
        */
        #endregion
        if (countAttack > 0 && countAttack < 12)
            anim.SetBool("Attack " + countAttack, true);
        //Debug.Log(anim.GetBool("Combo1"));
        if (isAttack)
            attackTime += Time.deltaTime;

        if (attackTime <= delay)
        {

            if (Input.GetButtonDown("Attack Button") && countAttack <= 11)
            {
                isAttack = true;
                countAttack++;
                attackTime = 0;

                
            }
            ///*
            else if (Input.GetButtonDown("Attack Button") && countAttack > 11)
            {
                countAttack = 1;
            }
            //*/
            /*
            else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Combo1"))
            {
                ZeroAnimations();
            }
            */
        }
        else
        {
            isAttack = false;
            anim.SetTrigger("Reset");
            attackTime = 0;
            ZeroAnimations();

        }
    }

    private void ZeroAnimations()
    {
        countAttack = 0;
        for (int i = 0; i <= 11; i++)
        {
            if (i > 0)
                anim.SetBool("Attack " + i, false);
        }
    }

}
