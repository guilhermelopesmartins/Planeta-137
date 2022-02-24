using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Hud : MonoBehaviour {

    #region Variaveis
    public static Hud instance;

    public Animator anim;
    private bool interact;

    [Header("Mensagem na tela")]
    [SerializeField] private Text message;
    [SerializeField] private Font font;
    private static float countdown = 0.5f;
    private static string ms;
    private static bool count = false;
    [SerializeField] private Text potions;

    [Header("Objetivos")]
    [SerializeField] private Text mainGoal;
    [SerializeField] private GameObject secondGoal;
    #endregion

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
    }
    /*
    private void Start()
    {
        GameManager.instance.takeSample += UpdateGoals;
    }

    private void OnDisable()
    {
        GameManager.instance.takeSample -= UpdateGoals;
    }

    private void UpdateGoals ()
    {
        if (mainGoal != null)
        {

            if (GameManager.instance.goalsCompleted)
            {
                string samples = " Find and Kill the Swamp Monster!";
                mainGoal.fontSize = 30;
                mainGoal.text = samples;
                secondGoal.SetActive(false);
            }
            else
            {
                Debug.Log("Atualiza");

                string samples = "    *Gather 3 samples of Misconha (" + SampleController.GetSample("Misconha").ToString() + " / 3)\n"
                        + "\n" +
                      "    *Gather 5 samples of de Xerebebé (" + SampleController.GetSample("Xerebebe").ToString() + " / 5)\n"
                        + "\n" +
                      "    *Gather 2 samples of Goto (" + SampleController.GetSample("Goto").ToString() + " / 2)\n";
                mainGoal.text = samples;
            }
        }
    }
    */

    void FixedUpdate () {

        if (mainGoal != null)
        {

            if (GameManager.instance.goalsCompleted)
            {
                string samples = " Find and Kill the Swamp Monster!";
                mainGoal.fontSize = 30;
                mainGoal.text = samples;
                secondGoal.SetActive(false);
            }
            else
            {
                Debug.Log("Atualiza");

                string samples = "    *Gather 3 samples of Misconha (" + SampleController.GetSample("Misconha").ToString() + " / 3)\n"
                        + "\n" +
                      "    *Gather 5 samples of de Xerebebé (" + SampleController.GetSample("Xerebebe").ToString() + " / 5)\n"
                        + "\n" +
                      "    *Gather 2 samples of Goto (" + SampleController.GetSample("Goto").ToString() + " / 2)\n";
                mainGoal.text = samples;
            }
        }

        #region Mostrar Objetivo
        anim.SetBool("Show", interact);
        if (Input.GetAxis("Goals Button") > 0 || Input.GetKey(KeyCode.R))
        {
            interact = true;
        }
        else
        {
            interact = false;
        }
        #endregion

        #region Status do Player na HUD
        message.font = font;
        message.text = ms;

        potions.text = "x" + PlayerStats.GetPotion();
        #endregion

        #region Tempo da Menssagem
        if (countdown <= 0)
        {
            count = false;

        }

        if (count)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            ms = "";
            countdown = 2f;
        }
        #endregion
    }

    // MOSTRA UMA MENSSAGEM NA TELA \\
    public void SetMessage(string a)
    {
        if (countdown > 0)
        {
            ms = a;
            count = true;
        }
    }


}
