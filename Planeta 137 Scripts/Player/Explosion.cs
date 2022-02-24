using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float power;
    [SerializeField] private float radius;
    [SerializeField] private float upForce;
    [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private GameObject explosionPraticle;
    public void Explode()
    {
        //Instantiate(explosionPraticle, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.identity);
        var particle = Instantiate(explosionPraticle, player.transform.position, Camera.main.transform.rotation);
        particle.transform.parent = player.transform;
        //StartCoroutine(ExploseParticle());
        Debug.Log(rbPlayer.constraints);
        rbPlayer.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

        PlayerStats.LessManna(25);

        Vector3 explosionPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);

        PlayerSounds.instance.PlayeClipOnShoot(PlayerSounds.instance.explosion);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upForce, ForceMode.Impulse);
                if (hit.tag == "Enemy")
                {
                    EnemyMovement tempEnemy = hit.gameObject.GetComponent<EnemyMovement>();
                    tempEnemy.LessHp(10);
                } 
            }
        }
        
    }
    public void Constrains()
    {
        rbPlayer.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;
    }

}



