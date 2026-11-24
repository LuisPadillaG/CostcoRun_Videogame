
using UnityEngine;

public class Canvas_limiteDelJuego : MonoBehaviour
{
    [Header("Camaras")]
    Camera Main_Camera;
    Vector3 posicion_original;
    float size_original;

    [Header("Acercamiento")]
    Vector3 posicion_copyoriginal;
    float size_copyobjetivo;

    Vector3 posicion_inicio;
    float size_inicio;
    float tiempoAcercamiento = 0f;
    float duracionAcercamiento = 2f;

    bool acercarCamara = false;


    [Header("CanvasGameOver")]
    GameObject Canvas_GameOver;

    void Start()
    {
        Main_Camera = GameObject.Find("Main Camera").GetComponent<Camera>();

        GameObject Padre_GameOver = GameObject.Find("CanvasGameOver");
        Canvas_GameOver = Padre_GameOver.transform.Find("Image").gameObject;
        Canvas_GameOver.SetActive(false);


        // Camara posicion original
        posicion_original = Main_Camera.transform.position;
        size_original = Main_Camera.orthographicSize;

        // Camara posicion nueva
        posicion_copyoriginal = posicion_original;
        size_copyobjetivo = size_original;
    }

    void Update()
    {
        Camara_acercamineto();
    }

    /* ----- Funcion  ------- */


    void Camara_acercamineto()
    {
        if (acercarCamara)
        {
            tiempoAcercamiento += Time.deltaTime / duracionAcercamiento;

            Main_Camera.transform.position = Vector3.Lerp(
                posicion_inicio,
                posicion_copyoriginal,
                tiempoAcercamiento
            );

            Main_Camera.orthographicSize = Mathf.Lerp(
                size_inicio,
                size_copyobjetivo,
                tiempoAcercamiento
            );

            if (tiempoAcercamiento >= 1f)
            {
                Main_Camera.transform.position = posicion_copyoriginal;
                Main_Camera.orthographicSize = size_copyobjetivo;

                acercarCamara = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Objeto detectado: " + other.name);

        if (other.CompareTag("Player"))
        {
            Toque_Limite_cuadro_adentro();
            Toque_Limite_GameOver();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Toque_Limite_cuadro_afuera();
        }
    }


    void Toque_Limite_cuadro_adentro()
    {
        if (gameObject.name == "Limite_cuadro")
        {
            posicion_copyoriginal = new Vector3(3.67f, 2.98f, -12.12f);
            size_copyobjetivo = 2.31f;

            posicion_inicio = Main_Camera.transform.position;
            size_inicio = Main_Camera.orthographicSize;

            tiempoAcercamiento = 0f;
            acercarCamara = true;
        }
    }

    void Toque_Limite_cuadro_afuera()
    {
        if (gameObject.name == "Limite_cuadro")
        {
            posicion_copyoriginal = posicion_original;
            size_copyobjetivo = size_original;

            posicion_inicio = Main_Camera.transform.position;
            size_inicio = Main_Camera.orthographicSize;

            tiempoAcercamiento = 0f;
            acercarCamara = true;
        }
    }
    void Toque_Limite_GameOver()
    {
        if(gameObject.name == "Limite_GameOver")
        {
            Canvas_GameOver.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
