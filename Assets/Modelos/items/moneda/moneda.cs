
using UnityEngine;

public class moneda : MonoBehaviour
{
    float velocidad;

    AudioSource audioMoneda;
    bool recogida = false;

    GameObject coin_lod;//moneda

    void Start()
    {
        Componentes();
        Valores();
    }

    void Update()
    {
       Moneda_Girar();
    }

    void Componentes()
    {
        audioMoneda = GetComponent<AudioSource>();
        coin_lod = transform.Find("coin_lod").gameObject;
    }

    void Valores()
    {
        //velocidad = 20f;
    }

    // ---------------------- Funciones ---------------------- //

    void Moneda_Girar()
    {
        transform.Rotate(0f, 0 * Time.deltaTime, 0f);
    }

    void Moneda_Recoger()
    {
        recogida = true;//una sola vez

        coin_lod.SetActive(false);
        audioMoneda.Play();

        Destroy(gameObject, 8f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !recogida)
        {
            Moneda_Recoger();
        }
    }

}
