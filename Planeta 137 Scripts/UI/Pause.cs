using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject Menu;
    //[SerializeField] private GameObject btn,btn2;
    private bool isMenu;
    [SerializeField] private GameObject botao1, botao2, botao3, botao4;
    [SerializeField] private GameObject controles;
    [SerializeField] private GameObject goals;
    [SerializeField] private GameObject playerStats;
    [SerializeField] private GameObject bossHud;

    [SerializeField] private EventSystem evSystem;

    private void Start()
    {
        PauseOff();
        isMenu = false;
    }
    public void Update()
    {
        if (!PlayerStats.IsDead())
        {
            if (Input.GetButtonDown("Pause") && isMenu == false)
            {
                MenuPause();
            }

            else if (Input.GetButtonDown("Pause") && isMenu == true)
            {
                PauseOff();
            }
        }

    }


    public void MenuPause()
    {
        Debug.Log("pause");
        isMenu = true;
        Time.timeScale = 0;
        Menu.SetActive(true);
        botao1.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(botao1, null);
        goals.SetActive(false);
        playerStats.SetActive(false);
        if(bossHud != null)
            bossHud.SetActive(false);
        Cursor.visible = true;
        controles.SetActive(false);
        botao2.SetActive(true);
        botao3.SetActive(true);
        botao4.SetActive(false);
        
    }

    public void PauseOff()
    {
        isMenu = false;
        Menu.SetActive(false);
        goals.SetActive(true);
        playerStats.SetActive(true);
        bossHud.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(null);
        Time.timeScale = 1;
        Cursor.visible = false;
    }

    public void PauseControles()
    {
        botao4.SetActive(true);  
        botao1.SetActive(false);
        botao2.SetActive(false);
        botao3.SetActive(false);
        controles.SetActive(true);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(botao4, null);
    }
    public void Retornar()
    {
        
        botao1.SetActive(true);
        botao2.SetActive(true);
        botao3.SetActive(true);
        botao4.SetActive(false);
        controles.SetActive(false);
        GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(botao1.gameObject, null);
    }

    public void BackMenu()
    {
        PlayerStats.SetHp(0);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }
}
