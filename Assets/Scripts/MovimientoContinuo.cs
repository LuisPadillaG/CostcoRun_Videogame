using UnityEngine;

public class MovimientoContinuo : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] float velocidadMov = 5f;

    /*--- ITEMS ---*/
    [Header("Carrito")]
    static float multiplicadorVelocidad = 1f;


    void Update()
    {
        // Mueve el objeto hacia la izquierda de forma fluida y sincronizada
        Vector3 posicion = transform.position;
        /*posicion.x -= velocidadMov * Time.deltaTime;*/posicion.x -= velocidadMov * multiplicadorVelocidad * Time.deltaTime;
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