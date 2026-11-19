using UnityEngine;

public class MovimientoContinuo : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] float velocidadMov = 5f;

    /*--- ITEMS ---*/
    [Header("Carrito")]
    static float multiplicadorVelocidad = 1f;
    static float velocidadBase = 5f;

    // Propiedad pública para que otros scripts (como moneda) lean la velocidad exacta
    public static float VelocidadActual => velocidadBase * multiplicadorVelocidad;

    void Awake()
    {
        velocidadBase = velocidadMov;
    }

    void Update()
    {
        // Mueve el objeto hacia la izquierda de forma fluida y sincronizada
        Vector3 posicion = transform.position;
        posicion.x -= VelocidadActual * Time.deltaTime;
        transform.position = posicion;
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