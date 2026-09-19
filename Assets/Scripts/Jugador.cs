using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    CharacterController characterController;
    PlayerInput playerInput;
    //Inspector
    [Header("Configuracion del personaje")]
    [Range(1f, 50f)]
    public float corrida;
    [SerializeField] Vector3 velocida;
    float tiempo;
    float movNormal;
    void Start()
    {
        characterController = this.GetComponent<CharacterController>();
        playerInput = this.GetComponent<PlayerInput>();
        velocida = Vector3.zero;
        tiempo = Time.deltaTime * corrida;
        corrida = 1f;
        movNormal = tiempo * corrida;
    }

    void Update()
    {
        movNormal = corrida * tiempo; //lo pongo así para que cada frame recuerde su valor original. Si existe un boton de correr, lo multiplico X2

        //--move
        Vector2 inputMovimiento = playerInput.actions["move"].ReadValue<Vector2>();

        velocida.x = inputMovimiento.x;
        velocida.z = inputMovimiento.y;

        //Debug.Log(playerInput.actions["move"].ReadValue<Vector3>());
        characterController.Move(velocida * movNormal);


    }
}
