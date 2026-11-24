using UnityEngine;

public class MovimientoContinuo : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] float velocidadInicial = 5f;
    [SerializeField] float incrementoPorBloque = 0.15f; // Cuánto sube cada vez que sale un mapa nuevo
    [SerializeField] float velocidadMaxima = 15f;       // Tope para que sea jugable

    /*--- ITEMS ---*/
    [Header("Carrito")]
    static float multiplicadorVelocidad = 1f;
    static float velocidadBase = 0f;

    public static float VelocidadActual => velocidadBase * multiplicadorVelocidad;

    void Awake()
    {
        // Si es el primer bloque de la partida, arrancamos con la velocidad inicial
        if (velocidadBase <= 0f)
        {
            velocidadBase = velocidadInicial;
        }
        else
        {
            // Cada nuevo bloque instanciado sube un poco la velocidad global, sin pasar el máximo
            velocidadBase = Mathf.Min(velocidadBase + incrementoPorBloque, velocidadMaxima);
        }
    }

    void Update()
    {
        Vector3 posicion = transform.position;
        posicion.x -= VelocidadActual * Time.deltaTime;
        transform.position = posicion;
    }

    // IMPORTANTE: Llama a este método cuando el jugador pierda o reinicies la escena
    public static void ReiniciarVelocidad()
    {
        velocidadBase = 0f;
        multiplicadorVelocidad = 1f;
    }

    /*--- ITEMS ---*/
    //--- CARRITO (De = carrito.cs)
    public static void AumentarVelocidad()
    {
        multiplicadorVelocidad = 1.5f;
    }

    public static void RecuperarVelocidad()
    {
        multiplicadorVelocidad = 1f;
    }
}