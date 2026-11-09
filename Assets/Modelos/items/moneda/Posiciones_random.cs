using UnityEngine;

public class Posiciones_random : MonoBehaviour
{
    public GameObject Object; // prefab

    //float aleatorio;
    float aleatorio;
    public int cantidad_objeto = 5; 
    int cantidad = 0;
    public float tiempo_espera = 1f;
    float temporizadorActual;
    public float velocidad = 3;

    float [] aleatorio_posicion;
    

    void Start()
    {
        temporizadorActual = tiempo_espera;
    }

    void Update()
    {
        GenerarAleatorio();

    }

    
    void valores()
    {
        /*
        aleatorio_posicion[0] = 0;
        aleatorio_posicion[1] = 5;
        aleatorio_posicion[2] = 8;
        */
    }


    void GenerarAleatorio()
    {
       

        if (cantidad < cantidad_objeto)// límite tiempo
        {
            temporizadorActual -= Time.deltaTime;

            if (temporizadorActual <= 0)
            {
                //aleatorio
                int[] Aleatorio_Determinados = new int[] { 0, 5, 8 };
                int Aleatorio_indice = Random.Range(0, Aleatorio_Determinados.Length);
                int aleatorio = Aleatorio_Determinados[Aleatorio_indice];//aleatorio = Random.Range(-11f, 11f); // Rango aleatorio  


                //Agregar objeto
                //Instantiate(Object, new Vector3(0, aleatorio, 0), Quaternion.identity);
                GameObject objet_copia = Instantiate(Object, new Vector3(this.transform.position.x, aleatorio, 0), Quaternion.identity);

                //Se mueve
                Vector3 objet_copia_mover = objet_copia.transform.position;
                objet_copia_mover.z -= velocidad * Time.deltaTime;
                objet_copia.transform.position = objet_copia_mover;
                //objet_copia.transform.rotation = Quaternion.identity;
                

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

    /*
    void Aleatorio()
    {
        //aleatorio = Random.Range(-11f, 11f); // Rango aleatorio  
        int[] NumerosDeterminados = new int[] { 0, 5, 8 };
        int indiceAleatorio = Random.Range(0, NumerosDeterminados.Length);
        int aleatorio = NumerosDeterminados[indiceAleatorio];

    }
    */


    void PosicoiLimites()
    {

    }



}
