
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Prioridad del Parallax")]
    [Range(1, 3)]
    public int prioridad = 1;

    private Material materialParallax;
    private Vector2 offsetActual;

    // Progresión global de velocidad
    private float tiempoTranscurrido = 0f;

    // Velocidad inicial según prioridad
    private float ObtenerVelocidad()
    {
        switch (prioridad)
        {
            case 1:
                return 0.4f;  // Más rápido
            case 2:
                return 0.3f;  // Intermedio
            case 3:
                return 0.2f;  // Más lento
            default:
                return 0.2f;
        }
    }

    void Start()
    {
        Renderer renderizador = GetComponent<Renderer>();

        if (renderizador == null)
        {
            Debug.LogError("No se encontró un Renderer en " + gameObject.name);
            enabled = false;
            return;
        }

        materialParallax = renderizador.material;
        offsetActual = materialParallax.mainTextureOffset;
    }

    void Update()
    {
        // El tiempo avanza durante la partida
        tiempoTranscurrido += Time.deltaTime;

        // Aumento muy lento: 10 % adicional por minuto
        float progresionVelocidad = 1f + (tiempoTranscurrido / 60f) * 0.1f;

        // Velocidad de esta capa con la progresión aplicada
        float velocidad = ObtenerVelocidad() * progresionVelocidad;

        // Desplazar la textura mediante el offset
        offsetActual.x += velocidad * Time.deltaTime;

        materialParallax.mainTextureOffset = offsetActual;
    }

    void OnDestroy()
    {
        if (materialParallax != null)
        {
            Destroy(materialParallax);
        }
    }
}