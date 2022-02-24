using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    private static Animator animator;

    #region atributos
    private static float hp;
    private static float maxHealth;
    private static float manna;
    private static float shield;
    private static float damage;
    private static float magicDamage;
    private static float potion;
    private static bool isDead;
    private static int sample;
    private static bool isGround;
    #endregion


    #region Funções da vida
    // Soma o valor da vida
    public static void AddHp(float amount)
    {
        hp += amount;
    }

    // Modifica o valor da vida 
    public static void SetHp(float amount)
    {
        hp = amount;
    }

    // Tira vida do Player
    public static bool LessHp(float amount)
    {
        hp -= amount;
        HudPlayer.instance.DamageAnim();
        return true;
    }

    // Retorna o valor da vida
    public static float GetHp()
    {
        return hp;
    }

    // Retorna se está vivo
    public static bool IsDead()
    {
        if (hp <= 0)
        {
            isDead = true;
        }
        else
        {
            isDead = false;
        }

        return isDead;
    }

    // Soma o valor máximo da vida
    public static void AddMaxHealth(float amount)
    {
        maxHealth += amount;
    }

    // Coloca um valor na vida máxima
    public static void SetMaxHealth(float amount)
    {
        maxHealth = amount;
    }

    // Retorna o valor máximo da vida
    public static float GetMaxHealth()
    {
        return maxHealth;
    }
    #endregion

    #region Funções da Poção
    // Adiciona uma poção de vida
    public static void AddPotion()
    {
        potion++;
    }

    // Diminui uma poção de vida
    public static void LessPotion()
    {
        potion--;
    }

    // Retorna o valor da poção
    public static float GetPotion()
    {
        return potion;
    }

    // Coloca um valor na põção 
    public static void SetPotion(float amount)
    {
        potion = amount;
    }
    #endregion

    #region Funções da Mana
    // Soma o valor da mana
    public static void AddManna(float amount)
    {
        manna += amount;
    }

    // Retorna o valor da manna
    public static float GetManna()
    {
        return manna;
    }

    // Diminui a mana
    public static void LessManna(float amount)
    {
        manna -= amount;
    }

    // Coloca um valor na vida
    public static void SetManna(float amount)
    {
        manna = amount;
    }
    #endregion

    #region Funções do escudo
    // Soma os valores do escudo
    public static void AddShield(float amount)
    {
        shield += amount;
    }

    // Retorna o valor do escudo
    public static float GetShield()
    {
        return shield;
    }

    // Coloca um valor para o escudo
    public static void SetShield(float amount)
    {
        shield = amount;
    }
    #endregion

    #region Dano
    // Soma os valores do dano 
    public static void AddDamage(float amount)
    {
        damage += amount;
    }

    // Coloca um valor para o dano
    public static void SetDamage(float amount)
    {
        damage = amount;
    }

    // Retorna o valor do dano
    public static float GetDamage()
    {
        return damage;
    }

    // Soma os valores do dano mágico
    public static void AddMagicDamage(float amount)
    {
        magicDamage += amount;
    }

    // Coloca um valor para o dano mágico
    public static void SetMagicDamage(float amount)
    {
        magicDamage = amount;
    }

    // Retorna os valores do dano mágico
    public static float GetMagicDamage()
    {
        return magicDamage;
    }
    /*
    internal static void SetDamage(object itemDamage)
    {
        throw new NotImplementedException();
    }
    */
    #endregion

    #region Amostra
    /*
// Coloca um valor para a amostra
public static void SetSample(int amount)
{
    sample = amount;
}

// Adiciona um a amostra

public static void AddSample()
{
    sample++;
}

// Retorna o valor da amostra
public static int GetSample()
{
    return sample;
}
*/
    #endregion

    #region Ground
    public static bool GetGround()
    {
        return isGround;
    }

    public static void SetGround(bool amount)
    {
        isGround = amount;
    }
    #endregion
}
