using UnityEngine;

public class moneda : MonoBehaviour
{
    AudioSource audioMoneda;
    bool recogida = false;
    ParticleSystem Particle_destello;

    GameObject coin_lod;
    public float velocidad_transicion = 3;

    
    [Header("Contador_Moneda")]
    Cotador_Moneda Contador;
    /*
    //Cotador_Moneda Contador;//Cotador_Moneda.cs
    int Conteo_monedas = 3;
    */
    [Header("Iman")]
    public static bool imanActivo = false;
    public static Transform JugadorImanRecogido;
    public static float radioImanAtraccion = 5f;
    public static float velocidadIman = 12f;//velocidad de atracción.

    void Start()
    {
        audioMoneda = GetComponent<AudioSource>();
        coin_lod = transform.Find("coin_objet").gameObject;//<-NO CAMBIAR
        Contador = GameObject.Find("Canvas_UI").GetComponent<Cotador_Moneda>();//busca el contador//<-NO CAMBIAR
        
        Particle_destello = transform.GetChild(1).GetComponent<ParticleSystem>();
        //Particle_destello = GetComponentInChildren<ParticleSystem>();

    }

    void Update()
    {
        Moneda_Girar();
        Moneda_Mover();
        Moneda_Limite();
        /*
        Monedas_Efectos();
        
        Particle_destello.gameObject.SetActive(true);
        Particle_destello.Play();
        */
    }

    // ---------------------- Funciones ---------------------- //

    void Moneda_Girar()
    {
        transform.Rotate(0f, 0f, 0f);
    }
  
    void Moneda_Mover()//--aljugador
    {
        if (recogida == false)
        {
            if (imanActivo == true && JugadorImanRecogido != null &&
                Vector3.Distance(transform.position, JugadorImanRecogido.position) <= radioImanAtraccion)//las condiciones se cumplen
            {                                           //dónde está actualmente,queremos moverla,distancia puede avanza= Moneda,muevete,poco a poco
                transform.position = Vector3.MoveTowards(transform.position,JugadorImanRecogido.position,velocidadIman * Time.deltaTime);
            }
            else//no activo
            {
                Moneda_Mover_x();
            }
        }
    }
    void Moneda_Mover_x()
    {
        transform.position = new Vector3(
            transform.position.x - velocidad_transicion * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
    }
    void Moneda_Limite()
    {
        if (transform.position.x <= -6f)
        {
            Destroy(gameObject);
        }
    }

    
    void Monedas_Efectos()//------- Particulas
    {
        if (recogida == false)
        {
            Particle_destello.Play();
            //Particle_destello.transform.position = new Vector3(2, 0, 0);
            Particle_destello.gameObject.SetActive(true);
            if (!Particle_destello)//una vez
            {
                Destroy(Particle_destello);
            }
        }
        else{
            Particle_destello.Stop();
            Particle_destello.gameObject.SetActive(false);
        }
    }


    void Moneda_Recoger()
    {
        Monedas_Efectos();
        recogida = true;
        coin_lod.SetActive(false);
        audioMoneda.Play();    
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && recogida == false)
        {
            //Particle_destello = other.transform.Find("Particle_moneda").GetComponent<ParticleSystem>();
            Moneda_Recoger();
            //Contador.AcumuladorMonedas(Conteo_monedas);//Contador_Modena.cs
            Contador.AcumuladorMonedas();//Contador_Modena.cs
            Destroy(gameObject, 3f);

        }
    }
}



