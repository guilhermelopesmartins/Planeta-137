using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAim : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 offset2;
    [SerializeField] private GameObject invetory;
    [SerializeField] private GameObject aimImage;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float Y_ANGLE_MIN = -20;
    private float Y_ANGLE_MAX = 20;

    [SerializeField] private Transform aimPosition; // Posição da camera ao mirar
    [SerializeField] private Transform tempTransf; // posição de onde a mira olha

    //[SerializeField] private Transform spine;

    private Transform tempCam;
    private float time;
    private bool isSmooth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
        offset2 = aimPosition.position - player.position;

    }

    private void FixedUpdate()
    {
        if (invetory.active == false)
        {
            currentX += Input.GetAxis("Right Stick X Axis");
            currentY += Input.GetAxis("Right Stick Y Axis");
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        }
        aimImage.SetActive(false);

        if (!PlayerStats.IsDead())
        {

            Quaternion rot;
            if (Input.GetButton("Aim") && PlayerStats.GetGround())
            {
                //player.eulerAngles = new Vector3(0, transform.rotation.y, 0);

                aimImage.SetActive(true);
                Y_ANGLE_MIN = -10;//-2.5f
                Y_ANGLE_MAX = 10;//2.5f

                //spine.eulerAngles += new Vector3(spine.rotation.x + currentY, 0, 0) * 1.5f;
                player.eulerAngles = new Vector3(0, -currentX, 0) * 2f;
                rot = player.rotation;

                //transform.position = Vector3.Lerp(transform.position, player.position + rot * offset2, 10 * Time.deltaTime);
                transform.position = player.position + rot * offset2;
                transform.LookAt(tempTransf);
            }
        }
    }

}
