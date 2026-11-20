using UnityEngine;

public class chancla : MonoBehaviour
{
    //bool OneMoreTIme = false;
    //float TimepoOneMoreTIme = 0;
    [Header("Player")]
    GameObject Jugador;
    Jugador JugadorScript;

    [Header("Chancla")]
    GameObject canchla;
    GameObject chancla_objet;

    [Header("Vida Chancla")]
    float tiempo_efecto = 0f;
    [SerializeField] float tiempo_limite_VidaCuandoLoRecoje = 26;

    [Header("Canvas")]
    GameObject Chancla_Canvas;

    [Header("Audio")]
    AudioSource audiocanchla;


    bool recogida = false;
    //static bool chanclaActiva = false;//verifica que solo ahiga una
    static chancla ChanclaActiva;
    public float velocidad_transicion = 3;

    void Start()
    {
        Jugador = GameObject.FindGameObjectWithTag("Player");
        JugadorScript = Jugador.GetComponent<Jugador>();

        canchla = GameObject.FindGameObjectWithTag("Chancla");
        chancla_objet = transform.Find("chancla_objet").gameObject;//<-NO CAMBIAR 
        Chancla_Canvas = GameObject.Find("Canvas").transform.Find("Chancla").gameObject;//canvas
    }

    void Update()
    {
        if (ChanclaActiva == this)//aumenta vida items
        {
            limiteDeEfecto();
        }

        if (recogida == false)
        {
            Chancla_Girar();
            Chancla_Mover_x();
            Chancla_Limite();
        }
    }

    // ---------------------- Funciones ---------------------- //

    void Chancla_Girar()
    {
        transform.Rotate(0f, 90f * Time.deltaTime, 0f);
    }
    void Chancla_Recoger()
    {
        recogida = true;
        chancla_objet.SetActive(false);

        if (ChanclaActiva == null)
        {
            ChanclaActiva = this;
            Chancla_Canvas.SetActive(true);//canvas

            JugadorScript.AumentarSalto();//lo manda a Jugador.cs
        }
        else
        {
            ChanclaActiva.tiempo_limite_VidaCuandoLoRecoje += tiempo_limite_VidaCuandoLoRecoje;//aumenta vida item
            Destroy(gameObject);
        }
        /*
        chanclaActiva = true;
        JugadorScript.AumentarSalto();//lo manda a Jugador.cs
        */

        //canchla.SetActive(false);
        //chancla_objet.SetActive(false);
        //audiocanchla.Play();
    }

    void Chancla_Mover_x()
    {
        transform.position = new Vector3(
            transform.position.x - velocidad_transicion * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
    }
    void Chancla_Limite()
    {
        if (transform.position.x <= -6f)
        {
            Destroy(gameObject);
        }
    }

    void limiteDeEfecto()
    {
        if (recogida == true)
        {
            tiempo_efecto += Time.deltaTime;

            if (tiempo_efecto >= tiempo_limite_VidaCuandoLoRecoje)
            {
                JugadorScript.RecuperarSalto();//lo manda a Jugador.cs
                ChanclaActiva = null;//null static
                Chancla_Canvas.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && recogida == false)//if (other.CompareTag("Player") && recogida == false && chanclaActiva == false)
        {
            Chancla_Recoger();
        }
    }
}
/*
 
 */