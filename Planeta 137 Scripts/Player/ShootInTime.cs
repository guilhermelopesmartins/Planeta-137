using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootInTime : MonoBehaviour {

    public GameObject bullet;
    [SerializeField] private Transform hand;
    [SerializeField] private Rigidbody rbPlayer;
    [SerializeField] private Animator anim;
    [HideInInspector] public static bool aim;

    private void Shoot()
    {
        // Cria uma bala na posição do player
        Instantiate(bullet, hand.position, transform.rotation);
        PlayerSounds.instance.PlayeClipOnShoot(PlayerSounds.instance.shoot);
    }

    public void FreezePosition()
    {
        rbPlayer.constraints = RigidbodyConstraints.FreezePosition;
    }

    public void EndShoot()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Shoot", false);
        Debug.Log("aviso");
    }

    public static void Aim()
    {
        aim = true;
    }

    public void OutAim()
    {
        aim = false;
    }

    public static bool GetAim()
    {
        return aim;
    }
}
