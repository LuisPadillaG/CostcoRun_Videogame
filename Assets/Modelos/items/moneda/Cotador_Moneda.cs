using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Cotador_Moneda : MonoBehaviour
{
    public TextMeshProUGUI Puntuaje;
    public int cantidadMonedas = 0;
    private string baseUrl = "https://apidecosco.onrender.com";
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
        EnviarMonedas();
    }
    public void EnviarMonedas()
    {
        StartCoroutine(PostMonedas());
    }
    IEnumerator PostMonedas()
    {
        string url = baseUrl + "/subir_monedas";

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm(url, ""))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("POST exitoso. Respuesta: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error en el POST: " + request.error);
            }
        }
    }
    //IEnumerator PostMonedas()
    //{
    //    string url = baseUrl + "/subir_monedas";
    //    string json = "{\"monedas\":" + cantidadMonedas + "}";
    //    byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

    //    using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
    //    {
    //        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //        request.downloadHandler = new DownloadHandlerBuffer();
    //        request.SetRequestHeader("Content-Type", "application/json");

    //        yield return request.SendWebRequest();

    //        if (request.result == UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log("POST exitoso. Respuesta: " + request.downloadHandler.text);
    //        }
    //        else
    //        {
    //            Debug.LogError("Error en el POST: " + request.error);
    //        }
    //    }
    //}
}