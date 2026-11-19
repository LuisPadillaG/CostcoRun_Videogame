using UnityEngine;

public class x2 : MonoBehaviour
{

    bool recogida = false;
    AudioSource audiox2;
    GameObject x2_lod;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Carrito_Girar();
        audiox2 = GetComponent<AudioSource>();
        x2_lod = transform.Find("x2_objet").gameObject;//<-NO CAMBIAR


    }

    void Carrito_Girar()
    {
        transform.Rotate(0f, 90f * Time.deltaTime, 0f);
    }

    void X2_Recoger()
    {
        Carrito_Girar(); 
        recogida = true;
        //x2_lod.SetActive(false);
        //audiox2.Play();
    }




    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && recogida == false)
        {
            //Particle_destello = other.transform.Find("Particle_moneda").GetComponent<ParticleSystem>();
            X2_Recoger();
            //Contador.AcumuladorMonedas(Conteo_monedas);//Contador_Modena.cs
            //Contador.AcumuladorMonedas();//Contador_Modena.cs
            Destroy(gameObject, 3f);

        }
    }


}
