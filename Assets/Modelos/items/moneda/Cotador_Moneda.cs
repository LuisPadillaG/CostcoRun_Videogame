using UnityEngine;
using TMPro;

public class Cotador_Moneda : MonoBehaviour
{
    public TextMeshProUGUI Puntuaje;
    public int cantidadMonedas = 0;

    void Start()
    {
    }

    void Update()
    {
        Puntuaje.text = cantidadMonedas.ToString();
    }

    // ---------------------- Funciones ---------------------- //
    public void AcumuladorMonedas()
    {
        cantidadMonedas += 10;
        Puntuaje.text = cantidadMonedas.ToString();
    }
}