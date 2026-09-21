using UnityEngine;
using UnityEngine.InputSystem;

public class Player_2 : MonoBehaviour
{
    //--- COMPONENTES
    CharacterController characterController;
    PlayerInput playerInput;
    Animator animator;

    //-- MOVIMIENTO Y GRAVEDAD
    [Header("Movimiento y Gravedad")]

    [Range(10f, 50f)]
    [SerializeField] float fuerzaSalto = 15f;

    [Range(50f, 200f)]
    [SerializeField] float gravedad = 50f;

    Vector3 velocidad;
    bool is_grounded;

    //--- RECUPERACIÓN
    float posicionX_inicial;
    float cooldownRecuperacion;

    //--- AGACHARSE
    [Header("Agacharse")]

    [SerializeField] float alturaNormal = 2f;
    [SerializeField] float alturaAgachado = 1f;

    float tiempoAgachado;

    void Start()//---------------------------INICIALIZACIÓN
    {
        ObtenerComponentes();
        PrepararValoresIniciales();
    }

    void Update()//--------------------------- ACTUALIZACIÓN PRINCIPAL
    {
        PrepararVariables();

        ControlarSuelo();
        ControlarAgacharse();
        ControlarSalto();

        ControlarRecuperacion();
        DetectarObstaculos();

        AplicarGravedad();
        MoverJugador();
    }

    void ObtenerComponentes()//--------------------------- COMPONENTES
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    void PrepararValoresIniciales()//--------------------------- VALORES INICIALES
    {
        posicionX_inicial = transform.position.x;

        cooldownRecuperacion = 0;
        tiempoAgachado = 0f;

        if (alturaNormal == 2f) {alturaNormal = characterController.height;}
    }

    void PrepararVariables()//--------------------------- PREPARAR VARIABLES
    {
        animator.SetInteger("Estado", 0);

        cooldownRecuperacion -= Time.deltaTime;

        is_grounded = characterController.isGrounded;
    }


    void ControlarSuelo()//--------------------------- SUELO
    {
        if (is_grounded && velocidad.y < 0)
        {
            velocidad.y = -2f;
        }
    }

    void ControlarAgacharse()//--------------------------- AGACHARSE
    {
        if (tiempoAgachado > 0)
        {
            tiempoAgachado -= Time.deltaTime;

            animator.SetInteger("Estado", 2);

            if (tiempoAgachado <= 0)
            {
                characterController.height = alturaNormal;

                characterController.center =
                    new Vector3(0, alturaNormal / 2f, 0);
            }
        }
        else if (playerInput.actions["Crouch"].WasPressedThisFrame() && is_grounded)
        {
            tiempoAgachado = 0.2f;

            characterController.height = alturaAgachado;

            characterController.center =
                new Vector3(0, alturaAgachado / 2f, 0);

            animator.SetInteger("Estado", 3);
        }
    }

    void ControlarSalto()//--------------------------- SALTO
    {
        if (playerInput.actions["Jump"].WasPressedThisFrame() && is_grounded)
        {
            velocidad.y = fuerzaSalto;

            animator.SetInteger("Estado", 1);
        }
    }

    void ControlarRecuperacion()//--------------------------- RECUPERACIÓN HORIZONTAL
    {
        if (posicionX_inicial > transform.position.x &&
            cooldownRecuperacion <= 0)
        {
            velocidad.x += Time.deltaTime * 0.5f;
        }
        else
        {
            velocidad.x = 0;
        }
    }

    void DetectarObstaculos()//--------------------------- OBSTÁCULOS
    {
        if (Physics.Raycast(
            transform.position + Vector3.up * 0.5f,
            Vector3.right,
            0.35f))
        {
            cooldownRecuperacion = 0.3f;
        }
    }

    void AplicarGravedad()//--------------------------- GRAVEDAD
    {
        velocidad.y -= gravedad * Time.deltaTime;
    }


    void MoverJugador()//---------------------------  MOVIMIENTO
    {
        characterController.Move(velocidad * Time.deltaTime);
    }
}
