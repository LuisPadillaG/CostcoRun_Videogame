using UnityEngine;

public class loop_scene : MonoBehaviour
{
    //--- VARIABLES
    Vector3 posicion;
    float repeticion;
    public float velocidad;

    void Start()
    {
        ObtenerComponentes();
        PrepararValoresIniciales();
    }

    void Update()
    {
        MoverEscenario();
        ComprobarRepeticion();
    }


    void ObtenerComponentes()//--------------------------- COMPONENTES
    {
        posicion = transform.position;
        //repeticion = GetComponent<BoxCollider>().size.x / 2;
    }

    void PrepararValoresIniciales()//--------------------------- VALORES INICIALES
    {
        repeticion = 140.2f;
        velocidad = 20f;
    }

    void MoverEscenario()//--------------------------- MOVIMIENTO
    {
        transform.position += Vector3.left * velocidad * Time.deltaTime;
    }

    void ComprobarRepeticion()//--------------------------- REPETICIÓN
    {
        if (transform.position.x < posicion.x - repeticion)
        {
            transform.position = posicion;
        }
    }


}
