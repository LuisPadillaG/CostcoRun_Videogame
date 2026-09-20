using UnityEngine;

public class MovimientoContinuo : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] float velocidadMov = 5f; 

    void Update()
    {
        // Mueve el objeto hacia la izquierda de forma fluida y sincronizada
        Vector3 posicion = transform.position;
        posicion.x -= velocidadMov * Time.deltaTime;
        transform.position = posicion;
    }
}