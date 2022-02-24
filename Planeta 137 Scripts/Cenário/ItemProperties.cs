using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperties : MonoBehaviour {

    // Raridade do item
    public enum itemRarity
    {
        Common,
        Rare,
        Legendary,
        Epic
    }

    public string nameItem; // nome do item
    public Sprite itemImage; // imagem do item

    #region Valor de aprimoramento do item
    public float itemDamage;
    public float itemMagicDamage;
    public float itemMaxHealth;
    public float itemShield;
    #endregion
}
