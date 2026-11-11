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

    void Start()
    {
        iman_objet = transform.Find("iman_objet").gameObject;//<-NO CAMBIAR
    }

    void Update()
    {
        limiteDeEfecto();
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