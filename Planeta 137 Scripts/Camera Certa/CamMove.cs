using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour {

    private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Vector3 offset2;
    [SerializeField] private GameObject invetory;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    private float Y_ANGLE_MIN = -20;
    private float Y_ANGLE_MAX = 20;

    [SerializeField] private Transform aimPosition; // Posição da camera ao mirar
    [SerializeField] private Transform tempTransf; // posição de onde a mira olha

    [SerializeField] private Transform spine;

    private Transform tempCam;
    private float time;
    private bool isSmooth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;
        //offset2 = aimPosition.position - player.position;

    }

    private void FixedUpdate()
    {
        //if (invetory.active == false)
        //{
            currentX += Input.GetAxis("Right Stick X Axis");
            currentY += Input.GetAxis("Right Stick Y Axis");
            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        //}
        //aimImage.SetActive(false);

        if (!PlayerStats.IsDead())
        {

            Quaternion rot;
            //Quaternion rotX;

            if (Input.GetButtonDown("Aim"))
            {
                tempCam = transform;
                player.rotation = aimPosition.rotation;
            }
            if (Input.GetButton("Aim") && PlayerStats.GetManna() >= 25 && PlayerStats.GetGround())
            {
                Vector3 right = Camera.main.transform.right;
                Vector3 forward = Vector3.Cross(right, Vector3.up);
                //player.eulerAngles = new Vector3(0, transform.rotation.y, 0);
                ShootInTime.Aim();

                Y_ANGLE_MIN = -10;//-2.5f
                Y_ANGLE_MAX = 10;//2.5f

                spine.eulerAngles += new Vector3(spine.rotation.x + currentY, 0, 0) * 1.5f;
                player.eulerAngles = new Vector3(0, -currentX, 0) * 2f;
                //aimPosition.rotation = player.rotation;
                rot = player.rotation;

                transform.position = Vector3.Lerp(transform.position, player.position + rot * offset2, 10 * Time.deltaTime);
                //transform.position = player.position + rot * offset2;
                transform.LookAt(tempTransf);
            }
            else if (Input.GetButtonUp("Aim"))
            {
                isSmooth = true;
            }
            else
            {

                Y_ANGLE_MIN = -20;
                Y_ANGLE_MAX = 20;
                Quaternion rotation = Quaternion.Euler(currentY * 1.5f, -currentX * 1.5f, 0);
                rot = rotation;
                if (time <= 0.5 && isSmooth)
                {
                    Debug.Log(time);
                    transform.position = Vector3.Lerp(tempCam.position, player.position + rot * offset, 8 * Time.deltaTime);
                    time += Time.deltaTime;
                }
                else
                {
                    isSmooth = false;
                    time = 0;
                    transform.position = player.position + rot * offset;
                }
                transform.LookAt(new Vector3(player.position.x, player.position.y + 1, player.position.z));
            }
        }
    }
}
