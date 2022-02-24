using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchController : MonoBehaviour {

    [SerializeField] protected SphereCollider handR;
    [SerializeField] protected SphereCollider handL;
    [SerializeField] protected SphereCollider footR;
    [SerializeField] protected SphereCollider footL;

    // Use this for initialization
    void Start ()
    {
        handR.gameObject.SetActive(false);
        handL.gameObject.SetActive(false);
        if (footR != null)
        {
            footR.gameObject.SetActive(false);
        }
        if (footL != null)
        {
            footL.gameObject.SetActive(false);

        }
	}

    void StartPunchL()
    {
        handL.gameObject.SetActive(true);
        PlayerSounds.instance.PlayeClipOnShoot(PlayerSounds.instance.punchAndkick);
    }

    void EndPunchL()
    {
        handL.gameObject.SetActive(false);
    }

    void StartPunchR()
    {
        handR.gameObject.SetActive(true);
        PlayerSounds.instance.PlayeClipOnShoot(PlayerSounds.instance.punchAndkick);
    }

    void EndPunchR()
    {
        handR.gameObject.SetActive(false);
    }

    void StartKickR()
    {
        if (footR != null)
        {
            footR.gameObject.SetActive(true);
            PlayerSounds.instance.PlayeClipOnShoot(PlayerSounds.instance.punchAndkick);
        }
        
    }

    void EndKickR()
    {
        if (footR != null)
        {
            footR.gameObject.SetActive(false);
        }
    }

    void StartKickL()
    {
        if (footL != null)
        {
            footL.gameObject.SetActive(true);
            PlayerSounds.instance.PlayeClipOnShoot(PlayerSounds.instance.punchAndkick);
        }

    }

    void EndKickL()
    {
        if (footL != null)
        {
            footL.gameObject.SetActive(false);
        }
    }
}
