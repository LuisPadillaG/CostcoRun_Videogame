using UnityEngine;
using UnityEngine.UI;

public class x2 : MonoBehaviour
{
    /*
    [Header("Player")]
    Jugador JugadorScript;
    */
    [Header("x2")]
    AudioSource audiox2;
    GameObject x2_lod;
    bool recogida = false;
    public float velocidad_transicion = 3;

    [Header("Vida x2")]
    float tiempo_efecto = 0f;
    [SerializeField] float tiempo_limite_VidaCuandoLoRecoje = 30;


    [Header("Canvas")]
    //GameObject Canvas;
    GameObject x2_Canvas;

    [Header("contador_por_2")]
    public static bool x2Activo = false;
    static x2 X2Activo;//solo un itmes

    void Start()
    {
        audiox2 = GetComponent<AudioSource>();
        x2_lod = transform.Find("x2_objet").gameObject;//<-NO CAMBIAR
        //GameObject Canvas = GameObject.Find("Canvas");
        x2_Canvas = GameObject.Find("Canvas").transform.Find("x2").gameObject;
        /*
        if (Canvas != null)
        {
            x2_Canvas = Canvas.transform.Find("x2").gameObject;
        }
        else
        {
            Debug.Log("NO SE ENCONTRO EL CANVAS");
        }
        */
    }

    void Update()
    {
            if (X2Activo == this)//ona vez
            {
                LimiteDeEfecto();
            }

            if (recogida == false)
            {
                Carrito_Girar();
                x2_Mover_x();
            }
        
    }

    // ---------------------- Funciones ---------------------- //

    void Carrito_Girar()
    {
        transform.Rotate(0f, 90f * Time.deltaTime, 0f);
    }

    void X2_Recoger()
    {
        recogida = true;
        x2_lod.SetActive(false);

        if (X2Activo == null)
        {
            X2Activo = this;

            x2Activo = true;
            x2_Canvas.SetActive(true);//canvas x2

            audiox2.Play();
        }
        else
        {
            X2Activo.tiempo_limite_VidaCuandoLoRecoje += tiempo_limite_VidaCuandoLoRecoje;

            Destroy(gameObject);
        }
    }

    void x2_Mover_x()
    {
        transform.position = new Vector3(
            transform.position.x - velocidad_transicion * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
    }

    void LimiteDeEfecto()
    {
        if (recogida == true)
        {
            tiempo_efecto += Time.deltaTime;

            if (tiempo_efecto >= tiempo_limite_VidaCuandoLoRecoje)
            {
                x2Activo = false;
                x2_Canvas.SetActive(false);//canva
                X2Activo = null; //1 itmes
                Destroy(gameObject);
            }
        }
    }


    public static int MultiplicarMonedas(int cantidad)//Contador_Moneda.cs = (10)
    {
        if (x2Activo == true)
        {cantidad = cantidad * 2; }
        return cantidad;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && recogida == false)//if (other.CompareTag("Player") && recogida == false && x2Activo == false)//solo una vez
        {
            X2_Recoger();
            //Destroy(gameObject, 10f);
        }
    }
}