using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class comida : MonoBehaviour
{

     GameObject Comida;
     GameObject miPrefab;
     ParticleSystem Comida_Particle;

    Vector3 posicion;

    void Start()
    {
        //Comida = transform.Find("carrito_objet").gameObject;
        Comida = GameObject.FindGameObjectWithTag("comida");
        miPrefab = Resources.Load<GameObject>("Enemigos/prefabs/particulas/Particle_moneda");

        //Comida_Particle = ParticleSystem.FindAnyObjectByType<Particle_comida>();

        posicion = this.transform.position;
        

    }

    // Update is called once per frame
    void Update()
    {
        posicion.x -= Time.deltaTime * 4;
        transform.position = posicion;
    }

    void Particulas()
    {
       
            Comida_Particle.gameObject.SetActive(true);
        
        /*
        else
        {
            Comida_Particle.gameObject.SetActive(false);
        }*/
        

    }


    //private void OnCollisionEnter(Collision collision)
 

    private void OnCollisionEnter(Collision collision)
    {
        
        if (CompareTag("Player"))
        {
            //Particulas();
            Comida_Particle.gameObject.SetActive(true);
            Comida_Particle.transform.position = posicion;
            //transform.position = new Vector3(0,0,0);
        }

        
    }



}

