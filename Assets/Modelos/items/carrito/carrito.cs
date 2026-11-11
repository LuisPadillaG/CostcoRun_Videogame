using UnityEngine;

public class carrito : MonoBehaviour
{
    [Header("Player")]
    Jugador JugadorScript;

    [Header("Carrito")]
    GameObject caarrito;
    ParticleSystem Particle_corriendo;
    bool recogida = false;


    [Header("Vida Carrito")]
    float tiempo_efecto = 0f;
    [SerializeField] float tiempo_limite_VidaCuandoLoRecoje = 26;

    public float velocidad_transicion = 3;

    void Start()
    {
        caarrito = transform.Find("carrito_objet").gameObject;//<-NO CAMBIAR 
        //MovimientoContinuoScript = GetComponent<MovimientoContinuo>();



        //Particle_corriendo = transform.Find("Particle_corriendo").GetComponent<ParticleSystem>();
        //Particle_corriendo.SetActive(false);

    }

    void Update()
    {
        Carrito_Girar();
        limiteDeEfecto();
        Carrito_Mover_x();
        Carrito_Limite();

    }
    
    // ---------------------- Funciones ---------------------- //
    void Carrito_Girar()
    {
        transform.Rotate(0f, 90f * Time.deltaTime, 0f);
    }

    void limiteDeEfecto()
    {
        if (recogida == true)
        {
            tiempo_efecto += Time.deltaTime;

            if (tiempo_efecto >= tiempo_limite_VidaCuandoLoRecoje)
            {
                JugadorScript.RecuperarVelocidad();//lo manda a Jugador.cs
                MovimientoContinuo.RecuperarVelocidad();//lo manda a MovimientoContinuo.cs(static)
                Particle_corriendo.Stop();
                Particle_corriendo.gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    void Carrito_Mover_x()
    {
        transform.position = new Vector3(
            transform.position.x - velocidad_transicion * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
    }
    void Carrito_Limite()
    {
        if (transform.position.x <= -6f)
        {
            Destroy(gameObject);
        }
    }

    void Carrito_Recoger()
    {
        recogida = true;

        JugadorScript.AumentarVelocidad();//lo manda a Jugador.cs
        MovimientoContinuo.AumentarVelocidad();//lo manda a MovimientoContinuo.cs(static)

        caarrito.SetActive(false);

        Particle_corriendo.gameObject.SetActive(true);
        Particle_corriendo.Play();//particulas
        

    }
    /**/
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && recogida == false)
        {
            JugadorScript = other.GetComponent<Jugador>();
            Particle_corriendo = other.transform.Find("Particle_corriendo").GetComponent<ParticleSystem>();

            if (JugadorScript != null)
            {
                Carrito_Recoger();
            }
        }
    }
}