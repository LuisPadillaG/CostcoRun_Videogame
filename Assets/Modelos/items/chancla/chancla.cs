using UnityEngine;

public class chancla : MonoBehaviour
{
    [Header("Player")]
    GameObject Jugador;
    Jugador JugadorScript;

    [Header("Chancla")]
    GameObject canchla;
    GameObject chancla_objet;

    [Header("Vida Chancla")]
    float tiempo_efecto = 0f;
    [SerializeField] float tiempo_limite_VidaCuandoLoRecoje = 26;

    [Header("Audio")]
    AudioSource audiocanchla;

    bool recogida = false;

    void Start()
    {
        Jugador = GameObject.FindGameObjectWithTag("Player");
        JugadorScript = Jugador.GetComponent<Jugador>();

        canchla = GameObject.FindGameObjectWithTag("Chancla");
        chancla_objet = transform.Find("chancla_objet").gameObject;//<-NO CAMBIAR 
    }

    void Update()
    {
        Chancla_Girar();
        limiteDeEfecto();
    }

    // ---------------------- Funciones ---------------------- //

    void Chancla_Girar()
    {
        transform.Rotate(0f, 90f * Time.deltaTime, 0f);
    }
    void Chancla_Recoger()
    {
        recogida = true;
        JugadorScript.AumentarSalto();//lo manda a Jugador.cs

        //canchla.SetActive(false);
        chancla_objet.SetActive(false);
        //audiocanchla.Play();
    }

    void limiteDeEfecto()
    {
        if (recogida == true)
        {
            tiempo_efecto += Time.deltaTime;

            if (tiempo_efecto >= tiempo_limite_VidaCuandoLoRecoje)
            {
                JugadorScript.RecuperarSalto();//lo manda a Jugador.cs
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && recogida == false)
        {
            Chancla_Recoger();
        }
    }
}