using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    [SerializeField] [Tooltip("Time to bullet Self Destroy")] private float time;
    [SerializeField] [Tooltip("Bullet Speed")] private float speed;

    private void Start()
    {
        Destroy(gameObject, time);
        PlayerStats.LessManna(25);
    }

    void FixedUpdate ()
    {
        transform.Translate(0, 0, speed);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(gameObject);
            //EnemyMovement.LessHp(PlayerStats.GetMagicDamage());
        }
    }
}
