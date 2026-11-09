using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cotador_Moneda : MonoBehaviour
{
    public TextMeshProUGUI Puntuaje;
    private GameObject Monedas;


    public int cantidadMonedas = 0;

    void Start()
    {
        Monedas = GameObject.FindGameObjectWithTag("Moneda");
    }

    void Update()
    {
        AcumuladorMonedas();
    }

    void AcumuladorMonedas()
    {
        if (Monedas != null)
        {
          
            Puntuaje.text = cantidadMonedas.ToString();
            Puntuaje.color = Color.red;
            cantidadMonedas ++;
        }
    }
}