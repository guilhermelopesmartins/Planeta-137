using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shiled : MonoBehaviour
{
    [SerializeField] private GameObject shieldParticle;
    private GameObject tempShield;
    [SerializeField] private Rigidbody rb;
    public void InstShield(float isShield)
    {
        if (isShield > 0)
        {
            tempShield = Instantiate(shieldParticle, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
            PlayerSounds.instance.PlayeClipOnShoot(PlayerSounds.instance.shield);
        }
        else
        {
            Destroy(tempShield);
        }
    }

    private void Freeze()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }
}
