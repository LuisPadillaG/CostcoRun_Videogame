using UnityEngine;
using UnityEngine.InputSystem;

public class chancla : MonoBehaviour
{

    [Header ("Player")]
    GameObject Jugador;
    PlayerInput playerInput;
    [SerializeField] float fuerzaSalto = 15f;
    Vector3 velocidad;


    void Start()
    {
        Funciones();
        Variables();

    }

    void Update()
    {
        Jugador = GameObject.FindGameObjectWithTag("Player");
        Salta_personaje();
    }

    void Funciones()
    {

    }
    void Variables()
    {

    }




    void Salta_personaje()
    {
        
        if (playerInput.actions["Jump"].WasPressedThisFrame()) 
        {
            velocidad.y = fuerzaSalto;
        }
        //characterController.Move(velocidad * Time.deltaTime);
    }




}
