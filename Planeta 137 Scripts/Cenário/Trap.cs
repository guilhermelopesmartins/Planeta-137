using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    private Animator animHealth;
    
    private void Start()
    {
        animHealth = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats.AddHp(-1/PlayerStats.GetShield());
            animHealth.SetTrigger("Damage");
        }
    }
    

    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animHealth.SetBool("Damage", false);
        }
    }
   
}
