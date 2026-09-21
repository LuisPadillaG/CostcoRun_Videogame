using UnityEngine;
using UnityEngine.InputSystem;

public class mandoJugador : MonoBehaviour
{

    //--- COMPONENTES
    CharacterController characterController;
    PlayerInput playerInput;

    //--- MOVIMIENTO Y GRAVEDAD
    [Header("Movimiento")]

    float velocidadMovimiento;
    float fuerzaSalto;
    float gravedad;
    Vector3 posicionInicial;

    Vector3 velocidad;
    bool is_grounded;

    void Start()
    {
        ObtenerComponentes();
        PrepararVariables();
    }

    void Update()
    {
        ComprobarSuelo();
        ControlarMovimiento();
        ControlarSalto();
        AplicarGravedad();
        MoverJugador();
    }

    void ObtenerComponentes()//-------------------- COMPONENTES
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        posicionInicial = transform.position;

    }
    void PrepararVariables()//--------------------------- PREPARAR VARIABLES
    {
        velocidadMovimiento = 5f;
        fuerzaSalto = 15f;
        gravedad = 50f;

    }


    void ComprobarSuelo()//-------------------- SUELO
    {
        is_grounded = characterController.isGrounded;

        if (is_grounded && velocidad.y < 0)
        {
            velocidad.y = -2f;
        }
    }

    void ControlarMovimiento()//-------------------- IZQUIERDA / DERECHA
    {
        float movimiento = playerInput.actions["Move"].ReadValue<Vector2>().x;
        velocidad.x = movimiento * velocidadMovimiento;
    }
    void ControlarSalto()//-------------------- SALTO
    {
        if (playerInput.actions["Jump"].WasPressedThisFrame() && is_grounded)
        {
            velocidad.y = fuerzaSalto;
        }
    }

    void AplicarGravedad()//-------------------- GRAVEDAD
    {
        velocidad.y -= gravedad * Time.deltaTime;
    }
    void MoverJugador()//-------------------- MOVIMIENTO (CHARACTER CONTROLLER)
    {
        characterController.Move(velocidad * Time.deltaTime);
    }
}
