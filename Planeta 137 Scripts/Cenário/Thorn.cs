using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    private Animator animHealth;
    [SerializeField] private float impulseBack;
    [SerializeField] private float impulseUp;
    [SerializeField] private float damage; 

    private void Start()
    {
        animHealth = GameObject.FindGameObjectWithTag("PlayerStats").GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerStats.LessHp(damage);
            other.attachedRigidbody.AddForce(-transform.forward * impulseBack, ForceMode.Impulse);
            other.attachedRigidbody.AddForce(transform.up * impulseUp, ForceMode.Impulse);
            animHealth.SetTrigger("Damage");
        }
    }
}
