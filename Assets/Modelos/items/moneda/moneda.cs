using UnityEngine;

public class moneda : MonoBehaviour
{
    AudioSource audioMoneda;
    bool recogida = false;

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
    }

    void Update()
    {
        Moneda_Girar();
        Moneda_Mover();
        Moneda_Limite();
    }

    // ---------------------- Funciones ---------------------- //

    void Moneda_Girar()
    {
        transform.Rotate(0f, 0f, 0f);
    }
    void Moneda_Limite()
    {
        if (transform.position.x <= -6f)
        {
            Destroy(gameObject);
        }
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

    void Moneda_Recoger()
    {
        recogida = true;
        coin_lod.SetActive(false);
        audioMoneda.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && recogida == false)
        {
            Moneda_Recoger();
            //Contador.AcumuladorMonedas(Conteo_monedas);//Contador_Modena.cs
            Contador.AcumuladorMonedas();//Contador_Modena.cs
            Destroy(gameObject, 3f);
        }
    }
}



