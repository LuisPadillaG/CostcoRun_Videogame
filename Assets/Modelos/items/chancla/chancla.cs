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
        //coin_lod = transform.Find("coin_lod").gameObject;
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

    /*
    void Chancla_Recoger()
    {
        recogida = true;//una sola vez

        coin_lod.SetActive(false);
        audioMoneda.Play();

        Destroy(gameObject, 8f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !recogida)
        {
            Moneda_Recoger();
        }
    }


    */


  



}
