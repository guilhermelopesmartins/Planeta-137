using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private Camera mainCam;
    [Header("Position of cameras")]
    [SerializeField] private Transform[] camPositions_Dialogue;
    [SerializeField] private Transform[] camPositions_Passage;
    [SerializeField] private Transform[] gateCam;
    private int countGate = 0;
    private int countPassage = 0;

    public static CameraController instance;

    private string currentCam;

    [Header("Hud to enable and disable")]
    [SerializeField] private GameObject huds;

    private void Awake()
    {
        if (instance == null && instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private void Start()
    {
        GameManager.instance.startTalk += TalkCam;
        GameManager.instance.endTalk += MoveCam;
        GameManager.instance.activePassage += ShowPassage;
        GameManager.instance.disableGate += ShowGate;
    }

    private void OnDisable()
    {
        GameManager.instance.startTalk -= TalkCam;
        GameManager.instance.endTalk -= MoveCam;
        GameManager.instance.activePassage -= ShowPassage;
        GameManager.instance.disableGate -= ShowGate;
    }

    private void ShowGate()
    {
        StartCoroutine(IShowGate());
    }

    private IEnumerator IShowGate()
    {
        active(false);

        Debug.Log("Show Gate");

        GameManager.instance.cam.enabled = false;

        camPositions_Dialogue[1] = mainCam.transform;
        mainCam.transform.position = gateCam[countGate].transform.position;
        mainCam.transform.rotation = gateCam[countGate].transform.rotation;
        yield return new WaitForSeconds(3);
        mainCam.transform.position = camPositions_Dialogue[1].transform.position;
        mainCam.transform.rotation = camPositions_Dialogue[1].transform.rotation;
        if (countGate == 0)
            countGate++;

        GameManager.instance.cam.enabled = true;
        GameManager.instance.player.enabled = true;

        active(true);
    }

    private void ShowPassage()
    {
        if (countPassage == 0)
            StartCoroutine(IShowPassage(0));
        else if (countPassage < 3 && countPassage > 0)
            StartCoroutine(IShowPassage(1));
        else
            StartCoroutine(IShowPassage(2));
    }

    private IEnumerator IShowPassage(int i)
    {
        active(false);

        GameManager.instance.cam.enabled = false;

        camPositions_Dialogue[1] = mainCam.transform;
        mainCam.transform.position = camPositions_Passage[i].transform.position;
        mainCam.transform.rotation = camPositions_Passage[i].transform.rotation;
        yield return new WaitForSeconds(3);
        mainCam.transform.position = camPositions_Dialogue[1].transform.position;
        mainCam.transform.rotation = camPositions_Dialogue[1].transform.rotation;
        countPassage++;

        GameManager.instance.cam.enabled = true;
        GameManager.instance.player.enabled = true;

        active(true);
    }


    public void TalkCam()
    {
        active(false);

        camPositions_Dialogue[1] = mainCam.transform;
        GameManager.instance.cam.enabled = false;
        GameManager.instance.player.enabled = false;

        mainCam.transform.position = camPositions_Dialogue[0].position;
        mainCam.transform.rotation = camPositions_Dialogue[0].rotation;
        //mainCam.transform.LookAt(GameManager.instance.player.transform.position);
    }

    public void MoveCam()
    {
        active(true);

        mainCam.transform.position = camPositions_Dialogue[1].position;
        mainCam.transform.rotation = camPositions_Dialogue[1].rotation;

        GameManager.instance.cam.enabled = true;
        GameManager.instance.player.enabled = true;
    }

    private void active(bool value)
    {
        if (huds != null)
            huds.SetActive(value);

    }

}
