using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DeadMenu : MonoBehaviour
{
    [SerializeField] private EventSystem even;
    [SerializeField] private GameObject deadScreen;
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private GameObject goals;
    [SerializeField] private GameObject playerStats;

    void Start()
    {
        deadScreen.SetActive(false);
    }

    private void Update()
    {
        if (PlayerStats.IsDead())
        {
            StartCoroutine(OpenDeadMenu());
        }
    }

    private IEnumerator OpenDeadMenu()
    {
        goals.SetActive(false);
        playerStats.SetActive(false);
        yield return new WaitForSeconds(3f);
        deadScreen.SetActive(true);
        even.SetSelectedGameObject(mainMenuButton);
    }

}
