using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private bool shieldEnabled;
    [SerializeField] private float shieldCooldown = 6;
    [SerializeField] private float shieldDuration = 3;
    [SerializeField] private Image shieldFill;

    private float shieldCount;
    private bool shieldEmpty = false;

    public Text shieldText;

    public static bool ShieldEnabled;

    public float currentShield;

    public static ShieldController instance;

    public ShieldController()
    {

    }

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

    private void Start()
    {
        shieldEnabled = true;
        shieldCount = shieldDuration;
        //shieldFill.fillAmount = 0;
    }

    void RecarregarEscudo()
    {
        //Recarregar
        if (shieldCount >= 0)
        {
            shieldCount -= Time.deltaTime / 2;
        }

        if (shieldCount >= shieldDuration)
        {
            shieldCount = shieldDuration;

            shieldEmpty = false;
        }
    }

    private void Update()
    {

        ShieldEnabled = shieldEnabled;
        currentShield = shieldCount / shieldDuration;

        //Verficar se está vazio
        if (shieldEmpty)
        {
            RecarregarEscudo();

            shieldEnabled = (shieldCount <= shieldDuration);           
        }

        if (Input.GetKey(KeyCode.Joystick1Button0))
        {           
            if (shieldEnabled)
            {
                //Usar habilidade
                Debug.Log("Usando habilidade.");

                shieldCount += Time.deltaTime;

                if (shieldCount >= 3)
                {
                    Debug.Log("Escudo vazio");

                    shieldCount = 3;

                    shieldEmpty = true;                    
                }
            }            
        }
        else
        {
            RecarregarEscudo();
        }
    }
}
