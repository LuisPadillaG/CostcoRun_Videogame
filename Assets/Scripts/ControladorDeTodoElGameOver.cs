
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class ControladorDeTodoElGameOver : MonoBehaviour
{
    private string baseUrl = "https://apidecosco.onrender.com";

    public int monedasFinales = 0;
    public bool estaSaltando;

    public TextMeshProUGUI textoMonedas;

    void Start()
    {
        // Recuperar monedas (si no hay, utilizar 0)
        monedasFinales = Cotador_Moneda.monedasGuardadas;

        if (monedasFinales < 0)
            monedasFinales = 0;

        textoMonedas.text = monedasFinales.ToString();

        StartCoroutine(RevisarIsJump());
    }

    IEnumerator RevisarIsJump()
    {
        while (true)
        {
            UnityWebRequest webrequest =
                UnityWebRequest.Get(baseUrl + "/is_jump");

            yield return webrequest.SendWebRequest();

            if (webrequest.result == UnityWebRequest.Result.Success)
            {
                string respuesta = webrequest.downloadHandler.text.Trim().ToLower();

                estaSaltando = (respuesta == "true");

                if (estaSaltando)
                {
                    SceneManager.LoadScene("SampleScene");
                    yield break;
                }
            }

            webrequest.Dispose();

            yield return new WaitForSeconds(0.1f);
        }
    }
}
