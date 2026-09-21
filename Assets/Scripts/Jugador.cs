using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    CharacterController characterController;
    PlayerInput playerInput;
    Animator animator;

    [Header("Configuración de Salto y Gravedad")]
    [Range(10f, 50f)]
    [SerializeField] float fuerzaSalto = 15f;
    [Range(50f, 200f)]
    [SerializeField] float gravedad = 50f;
    float posicionX_inicial;
    Vector3 velocidad;
    bool is_grounded;
    float cooldownRecuperacion; //tiempo que tardas en volver a tu posicion normal

    float tiempoAgachado;
    [SerializeField] float alturaNormal = 2f;
    [SerializeField] float alturaAgachado = 1f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        posicionX_inicial = this.transform.position.x;
        cooldownRecuperacion = 0;
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        tiempoAgachado = 0f;
        if (alturaNormal == 2f) alturaNormal = characterController.height;
    }

    void Update()
    {
        //normalizar variables 
        animator.SetInteger("Estado", 0);
        cooldownRecuperacion -= Time.deltaTime;
        is_grounded = characterController.isGrounded; 

        if (is_grounded && velocidad.y < 0) // ------ SUELO
        {
            velocidad.y = -2f;
        }
        else
        {
            
            //animator.SetInteger("Estado", 3); preparar cayendo
        }
        if (tiempoAgachado > 0) // ------ AGACHARSE
        {
            tiempoAgachado -= Time.deltaTime;
            animator.SetInteger("Estado", 2);
            if (tiempoAgachado <= 0)
            {
                characterController.height = alturaNormal;
                characterController.center = new Vector3(0, alturaNormal / 2f, 0);
            }
        }
        else if (playerInput.actions["Crouch"].WasPressedThisFrame() && is_grounded)
        {
            tiempoAgachado = 0.2f;
            characterController.height = alturaAgachado;
            characterController.center = new Vector3(0, alturaAgachado / 2f, 0);

            animator.SetInteger("Estado", 3);
        }
        if (playerInput.actions["Jump"].WasPressedThisFrame() && is_grounded) // ------ SALTO
        {
            velocidad.y = fuerzaSalto;
            animator.SetInteger("Estado", 1);
        } 
        if(posicionX_inicial > this.transform.position.x && cooldownRecuperacion <= 0)
        {
            velocidad.x += Time.deltaTime * 0.5f;
        }
        else
        {
            velocidad.x = 0;
        }
        if (Physics.Raycast(this.transform.position + Vector3.up * 0.5f, Vector3.right, 0.35f)) // ------ DETECTAR OBSTÁCULO
        {
            cooldownRecuperacion = 0.3F;
        }
        velocidad.y -= gravedad * Time.deltaTime; //------ GRAVEDAD
        characterController.Move(velocidad * Time.deltaTime);
    }
}