using UnityEngine;
using TMPro;

public class Cotador_Moneda : MonoBehaviour
{
    public TextMeshProUGUI[] Textos;//public TextMeshProUGUI Puntuaje;
    public int cantidadMonedas = 0;

    void Start()
    {
    }

    void Update()
    {
        //Puntuaje.text = cantidadMonedas.ToString();
        for (int i = 0; i < Textos.Length; i++)//todo textos
        {
            Textos[i].text = cantidadMonedas.ToString();
        }
    }

    // ---------------------- Funciones ---------------------- //
    public void AcumuladorMonedas()//-----x2.cs 
    {
        cantidadMonedas += x2.MultiplicarMonedas(10); //cantidadMonedas += 10;
        //Puntuaje.text = cantidadMonedas.ToString();
    }

   


}