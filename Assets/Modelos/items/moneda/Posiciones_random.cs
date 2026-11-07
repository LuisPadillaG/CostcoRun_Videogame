using UnityEngine;

public class Posiciones_random : MonoBehaviour
{
    public GameObject Object; // prefab

    float aleatorio;
    public int cantidad_objeto = 5; 
    int cantidad = 0;
    public float tiempo_espera = 1f;
    float temporizadorActual;
    public float velocidad = 3;

    void Start()
    {
        temporizadorActual = tiempo_espera;
    }

    void Update()
    {
        GenerarAleatorio();
    }

    void GenerarAleatorio()
    {
       

        if (cantidad < cantidad_objeto)// límite tiempo
        {
            temporizadorActual -= Time.deltaTime;

            if (temporizadorActual <= 0)
            {
                aleatorio = Random.Range(-11f, 11f); // Rango aleatorio  
                //Instantiate(Object, new Vector3(0, aleatorio, 0), Quaternion.identity);
                GameObject objet_copia = Instantiate(Object, new Vector3(this.transform.position.x, aleatorio, 0), Quaternion.identity);

                //se mueve
                Vector3 nuevaPosicion = objet_copia.transform.position;
                nuevaPosicion.z -= velocidad * Time.deltaTime;
                objet_copia.transform.position = nuevaPosicion; 


                /*
                if(objet_copia.transform.position.x  >= -6)
                {
                    Destroy(objet_copia);
                }
                */
                cantidad++;
                temporizadorActual = tiempo_espera; // Reiniciamos temporizador
            }
        }
    }


    void PosicoiLimites()
    {

    }



}
