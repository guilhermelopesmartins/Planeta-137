using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region Variaveis
    private GameObject inventory;

    [SerializeField] private GameObject[] itemsEquiped;         // Lista dos itens equipados
    [SerializeField] private Image[] slotEquipItems;            // Lista das imagens de itens equipados

    [SerializeField] private GameObject[] items;                // Lista dos itens não equipados
    [SerializeField] private Image[] slotItems;                 // Lista das imagens de itens não equipados

    [SerializeField] private GameObject itemSelected;           // Item selecionado para equipar
    public EventSystem eventSystem;                             // Event System

    GameObject tempItem;                                        // Item temporário para equipar no inventário

    [SerializeField] private Sprite empty;

    [SerializeField] private Transform plTransform;             // Transform do player para dropar o item

    [Header("Status do Item")]
    [SerializeField] private GameObject StatsPanel;
    [SerializeField] private Text itemName;
    [SerializeField] private Text itemDamge;
    [SerializeField] private Text itemMagicDamage;
    [SerializeField] private Text itemMaxHealth;
    [SerializeField] private Text itemManna;
    [SerializeField] private Text itemShield;
    #endregion

    void Start()
    {
        items = new GameObject[8];
        itemsEquiped = new GameObject[4];
        inventory = GameObject.FindGameObjectWithTag("CanvasInventory");
        inventory.SetActive(false);
        StatsPanel.SetActive(false);
    }

    void Update()
    {
        // Abrir inventorio
        if (Input.GetKeyDown(KeyCode.Joystick1Button11))
        {
            inventory.SetActive(!inventory.activeSelf);
            
        }
        if (inventory.activeSelf == true)
        {

            if (Input.GetButtonDown("Submit"))
                useItem();

            if (Input.GetKeyDown(KeyCode.Joystick1Button1))
                dropItem();

            itemStats();
        }
        else
        {
            StatsPanel.SetActive(false);
        }
    }

    #region Funções do inventário
    // ADICIONA UM ITEM AO INVENTÁRIO NÃO EQUIPADO \\
    public void AddItem(GameObject item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                // Adiciona o item sem equipa-lo
                items[i] = item;

                // Mostra a imagem no do item no inventario não equipado
                slotItems[i].sprite = items[i].GetComponent<ItemProperties>().itemImage;

                slotItems[i].tag = "NotEquipedSelected";

                item.SetActive(false);
                Hud.instance.SetMessage("ITEM COLETADO");
                return;
            }
            else
            {
                if (i >= 7)
                {
                    Hud.instance.SetMessage("SEM ESPAÇO");
                }
            }
        }
    }

    // sELECIONA E EQUIPA OS ITENS \\
    public void useItem()
    {

        int tempName = int.Parse(eventSystem.currentSelectedGameObject.name);

        // UM ITEM TEMPORÁRIO VAI RECEBER UM ITEM SELECIONADO E NÃO EQUIPADO PARA EQUIPAR \\
        if (eventSystem.currentSelectedGameObject.tag == "NotEquipedSelected")
        {
            tempItem = items[tempName];
            items[tempName] = null;
            slotItems[tempName].sprite = empty;
        }

        // EQUIPA OS ITEMS E ATRIBUI SEUS VALORES AO PLAYER \\
        if (eventSystem.currentSelectedGameObject.tag == "Equiped" && tempItem != null)
        {
            if (itemsEquiped[tempName] == null)
            {
                // equipa o item
                itemsEquiped[tempName] = tempItem;


                // ALTERA OS ATRIBUTOS DO PLAYER DE ACORDO COM O ITEM PEGO \\
                PlayerStats.AddMaxHealth(itemsEquiped[tempName].GetComponent<ItemProperties>().itemMaxHealth);
                PlayerStats.AddDamage(itemsEquiped[tempName].GetComponent<ItemProperties>().itemDamage);
                PlayerStats.AddMagicDamage(itemsEquiped[tempName].GetComponent<ItemProperties>().itemMagicDamage);
                PlayerStats.AddShield(itemsEquiped[tempName].GetComponent<ItemProperties>().itemShield);

                slotEquipItems[tempName].sprite = itemsEquiped[tempName].GetComponent<ItemProperties>().itemImage;

                // Destroi oo item temporário impossibiltando equipa-lo de novo 
                tempItem = null;
            }
        } 
        //  DESEQUIPA O ITEM SEM LARGA-LO \\
        else if (eventSystem.currentSelectedGameObject.tag == "Equiped" && itemsEquiped[tempName] != null && tempItem == null)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    // Adiciona o item sem equipa-lo
                    items[i] = itemsEquiped[tempName];

                    // ALTERA OS ATRIBUTOS DO PLAYER DE ACORDO COM O ITEM QUE FOI DESEQUIPADO \\
                    PlayerStats.AddMaxHealth( - itemsEquiped[tempName].GetComponent<ItemProperties>().itemMaxHealth);
                    PlayerStats.AddDamage( - itemsEquiped[tempName].GetComponent<ItemProperties>().itemDamage);
                    PlayerStats.AddMagicDamage( - itemsEquiped[tempName].GetComponent<ItemProperties>().itemMagicDamage);
                    PlayerStats.AddShield( - itemsEquiped[tempName].GetComponent<ItemProperties>().itemShield);

                    // Mostra a imagem no do item no inventario não equipado
                    slotItems[i].sprite = itemsEquiped[tempName].GetComponent<ItemProperties>().itemImage;
                    slotEquipItems[tempName].sprite = empty;
                    items[i].tag = "NotEquipedSelected";
                    itemsEquiped[tempName] = null;
                    return;
                }
            }
        }
    }

    // LARGA O ITEM \\
    public void dropItem()
    {
        int tempName = int.Parse(eventSystem.currentSelectedGameObject.name);
        if (items[tempName] != null || itemsEquiped[tempName] != null)
        {
            Hud.instance.SetMessage("ITEM LARGADO");
            switch (eventSystem.currentSelectedGameObject.tag)
            {
                case "NotEquipedSelected":
                    items[tempName].SetActive(true);
                    items[tempName].transform.position = plTransform.position;
                    items[tempName] = null;
                    slotItems[tempName].sprite = empty;
                    break;

                case "Equiped":
                    itemsEquiped[tempName].SetActive(true);
                    itemsEquiped[tempName].transform.position = plTransform.position;

                    // ALTERA OS ATRIBUTOS DO PLAYER DE ACORDO COM O ITEM QUE FOI LARGADO
                    PlayerStats.AddMaxHealth(-itemsEquiped[tempName].GetComponent<ItemProperties>().itemMaxHealth);
                    PlayerStats.AddDamage(-itemsEquiped[tempName].GetComponent<ItemProperties>().itemDamage);
                    PlayerStats.AddMagicDamage(-itemsEquiped[tempName].GetComponent<ItemProperties>().itemMagicDamage);
                    PlayerStats.AddShield(-itemsEquiped[tempName].GetComponent<ItemProperties>().itemShield);

                    itemsEquiped[tempName] = null;
                    slotEquipItems[tempName].sprite = empty;
                    break;
            }
        }
        else
        {
            Hud.instance.SetMessage("IMPOSSÍVEL LARGAR ITEM");
        } 
    }

    // MOSTRA OS STATUS DO ITEM \\
    public void itemStats()
    {
        int tempName = int.Parse(eventSystem.currentSelectedGameObject.name);

        if (items[tempName] != null && eventSystem.currentSelectedGameObject.tag == "NotEquipedSelected")
        {

            StatsPanel.SetActive(true);

            itemName.text = items[tempName].GetComponent<ItemProperties>().nameItem;
            itemDamge.text = items[tempName].GetComponent<ItemProperties>().itemDamage.ToString();
            itemMagicDamage.text = items[tempName].GetComponent<ItemProperties>().itemMagicDamage.ToString();
            itemMaxHealth.text = items[tempName].GetComponent<ItemProperties>().itemMaxHealth.ToString();
            itemShield.text = items[tempName].GetComponent<ItemProperties>().itemShield.ToString();
        }
        else if (eventSystem.currentSelectedGameObject.tag == "NotEquiped")
        {
            StatsPanel.SetActive(false);
        }
        else if (itemsEquiped[tempName] != null && eventSystem.currentSelectedGameObject.tag == "Equiped")
        {

            StatsPanel.SetActive(true);

            itemName.text = itemsEquiped[tempName].GetComponent<ItemProperties>().nameItem;
            itemDamge.text = itemsEquiped[tempName].GetComponent<ItemProperties>().itemDamage.ToString();
            itemMagicDamage.text = itemsEquiped[tempName].GetComponent<ItemProperties>().itemMagicDamage.ToString();
            itemMaxHealth.text = itemsEquiped[tempName].GetComponent<ItemProperties>().itemMaxHealth.ToString();
            itemShield.text = itemsEquiped[tempName].GetComponent<ItemProperties>().itemShield.ToString();
        }

        else
        {
            StatsPanel.SetActive(false);
        }
    }
    #endregion
}
