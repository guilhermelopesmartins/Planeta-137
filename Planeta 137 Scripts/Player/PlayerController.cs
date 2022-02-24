using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    #region Variaveis
    private float h, v; // Horizontal and Vertical Values
    private Rigidbody rb;
    private InventoryManager _Inventory;
    public static PlayerController instance;

    [Header("Variaveis de Movimento")]
    [Tooltip("Velocidade da caminhada")] [SerializeField] private float speedWalk;
    [Tooltip("Velocidade da corrida")] [SerializeField] private float speedRun;
    [Tooltip("Velocidade atual")] [SerializeField] private float currentSpeed;
    [Tooltip("Força do pulo")] [SerializeField] private float jumpForce;
    [Tooltip("Layer do chão")] [SerializeField] private LayerMask whatIsGround;
    private Vector3 movement;
    private bool isRun;
    public bool isStop = false;
    private int x, y;

    [Header("Game Objects e Components")]
    [Tooltip("Palyer Animator")] [SerializeField] private Animator anim;
    [SerializeField] private GameObject deadBody;
    [SerializeField] private GameObject inventory;
    private GameObject[] items;
    private GameObject[] tempItems;
    private bool isSampleCollision;
    private GameObject currentSample;
    private GameObject[] enemy;
    [SerializeField] private GameObject[] runParticle;
    [SerializeField] private ParticleSystem heal;

    [Header("Variaveis de Controle")]
    [Tooltip("Quantidade de pulos em sequência")] [SerializeField] private float countJump = 2;
    [Tooltip("Se está ou não no chão")] [SerializeField] private bool jump;
    [Tooltip("Tempo para recarregar escudo")] [SerializeField] private float shiledCooldown;
    private float currentShiledCooldown;
    public float shield;
    [Tooltip("Tempo para recarregar tiro")] [SerializeField] private float shootCooldown;
    private float currentShootCooldown;
    public float shoot;
    [Tooltip("Tempo para recarregar exposão")] [SerializeField] private float explodeCooldown;
    private float currentExplodeCooldown;
    public float explode;
    [Tooltip("distnacia do taget lock para atacar corpo a corpo")] [SerializeField] private float distAttack;
    [Tooltip("distancia do target lock para atiar")] [SerializeField] private float distShoot;
    private float currentDist;
    private bool usePotion = false;
    private bool interact = false;
    private bool isGround;
    private bool shootAnim;
    private bool isExplode; // Se pode explodir
    private bool isShoot; // Se pode atirar
    private bool isShield; // Se está usando o escudo
    private float shieldTime; // Tempo de duração do escudo
    private float countDownDodge; // Tempo para equivar
    private bool countDodge;    // para começar a diminuir o tempo
    private float shieldEnergy;
    private bool shieldEmpty;
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



    void Start()
    {
        #region Variaveis de status iniciadas
        PlayerStats.SetHp(100);
        PlayerStats.SetMaxHealth(100);
        PlayerStats.SetManna(100);
        PlayerStats.SetShield(10);
        PlayerStats.SetDamage(5);
        PlayerStats.SetMagicDamage(15);
        //PlayerStats.SetSample(0);
        PlayerStats.SetPotion(0);
        countDownDodge = 1;
        countDodge = false;
        #endregion
        shieldEmpty = false;
        shieldEnergy = 100;
        shieldTime = 8;
        rb = GetComponent<Rigidbody>();
        items = GameObject.FindGameObjectsWithTag("Item");
        if (_Inventory != null)
            _Inventory = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();

        // Controla particula de correr
        runParticle = new GameObject[2];
        for (int i = 0; i < runParticle.Length; i++)
        {
            runParticle = GameObject.FindGameObjectsWithTag("runParticle");
        }
        if(heal != null)
            heal.Stop();

        #region OnEnable
        GameManager.instance.disableGate += StopMove;
        GameManager.instance.startTalk += StopMove;
        GameManager.instance.activePassage += StopMove;
        #endregion

    }

    private void OnDisable()
    {
        GameManager.instance.disableGate -= StopMove;
        GameManager.instance.startTalk -= StopMove;
        GameManager.instance.activePassage -= StopMove;
    }

    private void Update()
    {

        #region Animações
        anim.SetBool("OnGround", isGround);
        //anim.SetBool("Shoot", shootAnim);
        PlayerStats.SetGround(isGround);
        anim.SetBool("Shield", isShield);

        #endregion

        #region Ações do player

        #region Pulo
        if (Input.GetButtonDown("Jump Button") && countJump > 0)
        {
            Jump();
        }
        #endregion

        if (isGround && !Input.GetButton("Aim"))
        {
            #region Poção
            /*
            if (Input.GetAxis("Potion Button") > 0)
            {
                usePotion = true;
                Debug.Log("poção");
            }
            else
            {
                usePotion = false;
            }
            if (usePotion)
            {
                UsePotion();
            }
            */

            if (Input.GetButtonDown("Potion Button1"))
            {
                UsePotion();
            }
            #endregion

            #region  Mira
            /*
            if (Input.GetButtonDown("Magic Attack Button 1") && PlayerStats.GetManna() >= 25)
            {
                shootAnim = true;
            }

            else if (Input.GetButtonDown("Magic Attack Button 1") && PlayerStats.GetManna() < 25)
            {
                Hud.SetMessage("MANNA INSUFICIENTE");
            }
            */
            #endregion

            #region Atira
            if (Input.GetButtonDown("Magic Attack Button 1") && PlayerStats.GetManna() >= 50 && isShoot)
            {
                anim.SetBool("Shoot", true);
                currentShootCooldown = shootCooldown;
                isShoot = false;
                currentDist = distShoot;
            }
            else
            {
                currentDist = distAttack;
            }

            if (currentShootCooldown > 0)
            {
                currentShootCooldown -= Time.deltaTime;
            }
            else
            {
                isShoot = true;
            }
            #endregion

            #region Explosão
            if (Input.GetButtonDown("Magic Attack Button 2") && PlayerStats.GetManna() >= 50 && isExplode)
            {
                anim.SetTrigger("Explode");
                currentExplodeCooldown = explodeCooldown;
                isExplode = false;
            }
            /*
            else if (Input.GetButtonDown("Magic Attack Button 2") && (PlayerStats.GetManna() < 50 || !isExplode))
            {
                Hud.SetMessage("IMPOSSIVEL REALIZAR AÇÃO");
            }
            */
            if (currentExplodeCooldown > 0)
            {
                currentExplodeCooldown -= Time.deltaTime;
            }
            else
            {
                isExplode = true;
            }
            #endregion

            #region Escudo
            #region Escudo Errado
            /*
            if (Input.GetKey(KeyCode.Joystick1Button0))
            {
                if (!shieldEmpty)
                {
                    shieldEnergy -= Time.deltaTime * shieldTime;
                    //shieldTime -= Time.deltaTime;
                    currentShiledCooldown = shiledCooldown;
                    isShield = true;
                    anim.SetBool("Shield", true);
                    //shiledFill.fillAmount += shieldTime + Time.time / 100;
                    Debug.Log("Energia do escudo: " + shieldEnergy);
                    if (shieldEnergy <= 0)
                    {
                        shieldEmpty = true;
                    }
                }
                else
                {
                    isShield = false;
                    anim.SetBool("Shield", false);
                }
            }

            if (!isShield)
            {
                if (currentShiledCooldown > 0)
                {
                    shieldEnergy += Time.deltaTime / shiledCooldown;
                    currentShiledCooldown -= Time.deltaTime;
                    shiledFill.fillAmount = shieldEnergy + Time.deltaTime / 100;
                    shiledFill.fillAmount = shieldTime;
                }
            }
            */
            #endregion

            if ((Input.GetKey(KeyCode.Joystick1Button0) || Input.GetKey(KeyCode.Alpha3)) && ShieldController.ShieldEnabled)
            {
                Debug.Log("OLHA EU AQUI!!!");
                isShield = true;
            }
            else
            {
                isShield = false;
            }
            /*
            if (Input.GetKey(KeyCode.Joystick1Button0))
            {
                if (shieldEnergy <= 0)
                {
                    shieldEmpty = true;
                }
                if (!shieldEmpty)
                {
                    shieldEnergy -= Time.deltaTime / 100;
                    isShield = true;
                    anim.SetBool("Shield", true);
                }
                else
                {
                    shieldEnergy += Time.deltaTime / 100;
                }
            }
            */
            #endregion

            #region Pegar Item
            if (interact)
            {
                tempItems = GameObject.FindGameObjectsWithTag("Item");

                for (int i = 0; i < tempItems.Length; i++)
                {
                    float distance = Vector3.Distance(transform.position, tempItems[i].transform.position);
                    if (distance < 0.5f)
                    {
                        _Inventory.AddItem(tempItems[i]);
                        return;
                    }

                }
            }
            #endregion

            #region Esquiva
            if (Input.GetKeyDown(KeyCode.Joystick1Button5))
            {
                anim.SetTrigger("Dodge_Right");
            }

            if (Input.GetKeyDown(KeyCode.Joystick1Button9))
            {
                anim.SetTrigger("Dodge_Left");
            }
            #endregion
        }
        #endregion

        #region Target Lock
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemy.Length; i++)
        {
            float dist = Vector3.Distance(transform.position, enemy[i].transform.position);
            if (dist <= currentDist)
            {
                Vector3 vTarget = new Vector3(enemy[i].transform.position.x, transform.position.y, enemy[i].transform.position.z);
                transform.LookAt(vTarget);
            }
        }
        #endregion

        //Debug.Log(movement.magnitude);

        if (Input.GetAxis("Interaction Button") > 0 || Input.GetKeyDown(KeyCode.E))
        {
            interact = true;
            Debug.Log("interação");
        }
        else
        {
            interact = false;
        }

        #region Pegar Amostra
        if (interact && isSampleCollision)
        {
            SampleController.AddSample(currentSample.GetComponent<Sample>().nome);
            Debug.Log(currentSample.GetComponent<Sample>().nome);
            Destroy(currentSample);
            //currentSample.SetActive(false);
            isSampleCollision = false;
            currentSample = null;
        }
        #endregion

        if (isGround && movement.magnitude > 0)
        {
            PlayerSounds.instance.PlayClip();
        }
    }

    void FixedUpdate()
    {
        

        if (!PlayerStats.IsDead()) // Verifica se ainda está vivo
        {

            shoot = currentShootCooldown + Time.deltaTime / 100;
            explode = currentExplodeCooldown + Time.deltaTime / 100;

            // Enche abarra de manna
            if (PlayerStats.GetManna() < 100)
            {
                PlayerStats.AddManna(4 * Time.deltaTime);
            }

            // Ajusta o hp para não ficar maio que o hp máximo
            if (PlayerStats.GetHp() > PlayerStats.GetMaxHealth())
            {
                PlayerStats.SetHp(PlayerStats.GetMaxHealth());
            }
          
            

            #region countador para esquivar
            if (countDodge)
            {
                countDownDodge -= Time.deltaTime;
            }
            if (!countDodge)
            {
                countDownDodge = 1;
            }
            if (countDownDodge <= 0)
            {
                countDodge = false;
            }
            //Debug.Log(Input.GetAxis("Right Stick Y Axis"));
            //Debug.Log(countDownDodge);
            #endregion

            #region Verifica se está no chão
            Collider[] col = Physics.OverlapSphere(transform.position, 0.35f, whatIsGround);
            isGround = col.Length > 0;
            if (isGround)
                countJump = 1;
            #endregion


            #region Movimentação
            if (shootAnim || isShield || isStop)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                currentSpeed = 0;
            }
            else if (Input.GetButton("Run Button") && !shootAnim && !isShield)
            {
                currentSpeed = speedRun;
                for (int i = 0; i < runParticle.Length; i++)
                {
                    runParticle[i].SetActive(true);
                }
            }
            else if ((!shootAnim && !isShield))
            {
                currentSpeed = speedWalk;                                    // velocidade atual é a velocidade da caminhada
                for (int i = 0; i < runParticle.Length; i++)
                {
                    runParticle[i].SetActive(false);
                }
            }

            if (GameManager.instance.isJoystick)
            {
                // ***** MOVIMENTAÇÃO DE ACORDO COM A CÂMERA ***** \\
                if (Input.GetAxis("Left Stick X Axis") != 0 || Input.GetAxis("Left Stick Y Axis") != 0)
                {

                    anim.SetFloat("Velocity", currentSpeed);                        // indica a velocidade atual para o animator

                    Vector3 right = Camera.main.transform.right;
                    Vector3 forward = Vector3.Cross(right, Vector3.up);

                    movement = Vector3.zero;

                    movement += right * Input.GetAxis("Left Stick X Axis") * currentSpeed * Time.deltaTime;
                    movement += forward * Input.GetAxis("Left Stick Y Axis") * currentSpeed * Time.deltaTime;

                    movement = new Vector3(movement.x, 0.0f, movement.z);
                    rb.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.3F);
                    //rb.velocity = movement * currentSpeed;
                    rb.transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
                    countDodge = true;

                    //Debug.Log(rb.velocity);
                }
                else if (shootAnim || (shootAnim && Input.GetButton("Aim")))
                {
                    movement = Vector3.zero;
                }
                else
                {
                    anim.SetFloat("Velocity", 0);
                }
            }
            else
            {
                if (Input.GetButton("Left Stick X Axis Teclado") || Input.GetButton("Left Stick Y Axis Teclado"))
                {

                    anim.SetFloat("Velocity", currentSpeed);                        // indica a velocidade atual para o animator

                    Vector3 right = Camera.main.transform.right;
                    Vector3 forward = Vector3.Cross(right, Vector3.up);

                    movement = Vector3.zero;

                    movement += right * Input.GetAxis("Left Stick X Axis Teclado") * currentSpeed * Time.deltaTime;
                    movement += forward * Input.GetAxis("Left Stick Y Axis Teclado") * currentSpeed * Time.deltaTime;

                    movement = new Vector3(movement.x, 0.0f, movement.z);
                    rb.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.3F);
                    //rb.velocity = movement * currentSpeed;
                    rb.transform.Translate(movement * currentSpeed * Time.deltaTime, Space.World);
                    countDodge = true;

                    //Debug.Log(rb.velocity);
                }
                else if (shootAnim || (shootAnim && Input.GetButton("Aim")))
                {
                    movement = Vector3.zero;
                }
                else
                {
                    anim.SetFloat("Velocity", 0);
                }
            }
            #endregion


        }
        else
        {
            Instantiate(deadBody, transform.position, transform.rotation);
            anim.SetTrigger("Dead");
            Destroy(gameObject);
        }

    }

    #region Funções do Player
    // Para a movimentação do jogador quando ativa algum evento que troque a camera
    private void StopMove()
    {
        anim.SetFloat("Velocity", 0);
        this.GetComponent<PlayerController>().enabled = false;
    }

    private void Jump()
    {
        //rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);           // Adiciona força ao pulo                                                             
        rb.velocity = Vector3.up * jumpForce;
        countJump--;                                                            // Dimini a quantidade de pulos consecutivos
        PlayerSounds.instance.PlayeClipOnShoot(PlayerSounds.instance.jump);
    }

    private void UsePotion()
    {
        if (PlayerStats.GetPotion() >= 1 && PlayerStats.GetHp() <= (PlayerStats.GetMaxHealth() - 10))
        {
            
            PlayerStats.AddHp(10);                                              // Usa uma poção que aumenta 10 de vida
            PlayerStats.LessPotion();       

            Hud.instance.SetMessage("POTION USED");
            //anim.SetTrigger("Potion");

            PlayerSounds.instance.PlayeClipOnShoot(PlayerSounds.instance.usePotion);
            heal.Play();
        }
        else
        {
            //Hud.SetMessage("IMPOSSÍVEL USAR POÇÃO");
        }
    }

    private void Dodge()
    {

    }
    #endregion

    #region Colisões
    private void OnTriggerEnter(Collider other)
    {
        // Verifica colisão com amostras
        if (other.tag == "sample")
        {
            isSampleCollision = true;
            currentSample = other.gameObject;
        }
    }
    #endregion
}
