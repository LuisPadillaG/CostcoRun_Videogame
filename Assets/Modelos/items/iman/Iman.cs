using UnityEngine;

public class Iman : MonoBehaviour
{
    [Header("Player")]
    Transform Jugador;

    [Header("Iman")]
    GameObject iman_objet;
    bool recogida = false;

    [Header("Configuracion del Iman")]
    [SerializeField] float radioImanAtraccion = 5f;
    [SerializeField] float velocidadIman = 12f;//rápido se acercan

    [Header("Vida del Iman")]
    float tiempo_efecto = 0f;
    [SerializeField] float tiempo_limite = 26f;

    public float velocidad_transicion = 3;
    void Start()
    {
        iman_objet = transform.Find("iman_objet").gameObject;//<-NO CAMBIAR
    }

    void Update()
    {
        limiteDeEfecto();
        Iman_Mover_x();
        Iman_Limite();
        Iman_Girar();

    }

    // ---------------------- Funciones ---------------------- //

    void Iman_Recoger()
    {
        recogida = true;

        moneda.JugadorImanRecogido = Jugador;
        moneda.radioImanAtraccion = radioImanAtraccion;
        moneda.velocidadIman = velocidadIman;
        moneda.imanActivo = true;//***/

        iman_objet.SetActive(false);
    }
    void Iman_Girar()
    {
        transform.Rotate(0f, 90f * Time.deltaTime, 0f);
    }
    void Iman_Mover_x()
    {
        transform.position = new Vector3(
            transform.position.x - velocidad_transicion * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );
    }
    void Iman_Limite()
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

            if (tiempo_efecto >= tiempo_limite)//pasaron los segundos
            {
                moneda.imanActivo = false;
                moneda.JugadorImanRecogido = null;
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && recogida == false)//ambas son sí
        {
            Jugador = other.transform;//se comvierte jugador(objeto)
            Iman_Recoger();
        }
    }
}