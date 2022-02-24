using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudPlayer : MonoBehaviour
{
    [SerializeField] private Image hpFill, manaFill, explodeFill, shootFill, shiledFill;
    [SerializeField] private Animator anim;

    public static HudPlayer instance;

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
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shootFill.fillAmount = PlayerController.instance.shoot + Time.deltaTime / 100;
        explodeFill.fillAmount = PlayerController.instance.explode + Time.deltaTime / 100;
        shiledFill.fillAmount = ShieldController.instance.currentShield;

        hpFill.fillAmount = PlayerStats.GetHp() / 100; //ENCHER BARRINHA DE HP
        manaFill.fillAmount = PlayerStats.GetManna() / 100; //ENCHER BARRINHA DE MANA 
    }

    public void DamageAnim()
    {
        anim.SetTrigger("Damage");
    }
}
