using UnityEngine;
using UnityEngine.InputSystem;

public class chancla : MonoBehaviour
{

    [Header("Player")]
    CharacterController JugadorCharacterControler;
    GameObject Jugador;
    PlayerInput JugadorplayerInput;
    [SerializeField] float fuerzaSalto = 15f;
    Vector3 velocidad;

    GameObject canchla;
  

    AudioSource audiocanchla;
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
        canchla = GameObject.FindGameObjectWithTag("Chancla").gameObject;
    }

    void Funciones()
    {
        
        
    }
    void Variables()
    {

    }
       
    void Chancla_Recoger()//una sola vez
    {
        recogida = true;

        canchla.SetActive(false);
        //audiocanchla.Play();

        Destroy(gameObject, 8f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Chancla_Recoger();
            Salta_personaje();
            //Destroy(gameObject, 9);
        }
    }
    void Salta_personaje()
    {
        if (JugadorplayerInput.actions["Jump"].WasPressedThisFrame())
        {
            velocidad.y = fuerzaSalto;
        }
    }
}
/*
 Saltar rapido
 */