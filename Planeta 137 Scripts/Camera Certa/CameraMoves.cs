using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoves : MonoBehaviour
{
    private Transform player;
    [SerializeField] private Vector3 offset;
    public float currentX = 0.0f;
    public float currentY = 0.0f;
    private float Y_ANGLE_MIN = -20;
    private float Y_ANGLE_MAX = 20;


    private Transform tempCam;
    private float time;
    private bool isSmooth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - player.position;

        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        if (GameManager.instance.isJoystick)
        {
            currentX += Input.GetAxis("Right Stick X Axis");
            currentY += Input.GetAxis("Right Stick Y Axis");
        }
        else
        {
            currentX += Input.GetAxis("Right Stick X Axis Mouse");
            currentY += Input.GetAxis("Right Stick Y Axis Mouse");
        }
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        if (!PlayerStats.IsDead())
        {
            Quaternion rot;

            Y_ANGLE_MIN = -20;
            Y_ANGLE_MAX = 20;
            Quaternion rotation = Quaternion.Euler(currentY * 1.5f, -currentX * 1.5f, 0);
            rot = rotation;
            transform.position = player.position + rot * offset;
            transform.LookAt(new Vector3(player.position.x, player.position.y + 1, player.position.z));

        }
    }
}
