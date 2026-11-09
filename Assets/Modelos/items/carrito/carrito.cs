using UnityEngine;
using UnityEngine.InputSystem;

public class carrito : MonoBehaviour
{

    [Header("Player")]
    CharacterController JugadorCharacterControler;
    GameObject Jugador;
    PlayerInput JugadorplayerInput;
    [SerializeField] float fuerzaSalto = 15f;
    Vector3 velocidad;

    [Header("Scena")]
    float escenarapido;


    [Header("Carrito")]
    GameObject caarrito;
    AudioSource audiocaarrito;
    bool recogida = false;


    void Start()
    {
        Funciones();
        Variables();

    }

    void Update()
    {
        Jugador = GameObject.FindGameObjectWithTag("Player");//jugador
        JugadorCharacterControler = Jugador.GetComponent<CharacterController>();//character
        JugadorplayerInput = Jugador.GetComponent<PlayerInput>();//playerinput



        //Salta_personaje();
        caarrito = GameObject.FindGameObjectWithTag("Chancla").gameObject;
    }

    void Funciones()
    {


    }
    void Variables()
    {

    }

    /* --------- FUNCIOES  ---------- */


    void Carrito_Recoger()//una sola vez
    {
        recogida = true;

        caarrito.SetActive(false);
        //audiocaarrito.Play();

        Destroy(gameObject, 8f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Carrito_Recoger();
            Correr_personaje();
            Scena_Rapido();
            //Destroy(gameObject, 9);
        }
    }


    void Correr_personaje()
    {
        JugadorCharacterControler.Move(velocidad * Time.deltaTime);
    }


    void Scena_Rapido()
    {
       //escenarapido = MovimientoContinuo.
    }

}

/*
 iR MAS RAPIDO
ESCENARIO RAPIDO
 */
