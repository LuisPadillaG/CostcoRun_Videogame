using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CanvasBotones : MonoBehaviour
{
    [Header("Player")]
    private PlayerInput playerInput;

    [Header("CanvasPausa")]
    GameObject Canvas_Pausa;

    [Header("CanvasGameOver")]
    GameObject Canvas_GameOver;

    bool juegoPausado;

    void Start()
    {
        ObtenerComponentes();
        PrepararValoresIniciales();

        //pausa
        GameObject Padre_pausa = GameObject.Find("CanvasPausa");
        Canvas_Pausa = Padre_pausa.transform.Find("Image").gameObject;

        //GameOver
        /*
        GameObject Padre_GameOver = GameObject.Find("CanvasGameOver");
        Canvas_GameOver = Padre_GameOver.transform.Find("Image").gameObject;
        */

        Canvas_Pausa.SetActive(false);
    }

    void Update()
    {
        PreMenu();
    }

    void ObtenerComponentes()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void PrepararValoresIniciales()
    {
        juegoPausado = false;
        Time.timeScale = 1f;
    }

    // ----------- PAUSA Y REANUDAR -----------

    public void PreMenu()
    {
        if (Keyboard.current != null &&
            (Keyboard.current.pKey.wasPressedThisFrame ||
             Keyboard.current.escapeKey.wasPressedThisFrame))
        {
            if (juegoPausado == false)
            {
                menu();
            }
            else
            {
                Resumuir();
            }
        }
    }

    public void menu()
    {
        juegoPausado = true;
        Canvas_Pausa.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resumuir()
    {
        juegoPausado = false;
        Canvas_Pausa.SetActive(false);
        Time.timeScale = 1f;
    }

    // ----------- BOTONES(ONCLICK) -----------

    public void Continuar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Jugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GAME_1");
    }

    public void Reintentar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir_Inicio()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Start");
    }

    public void Salir_Juego()
    {
        Application.Quit();
    }
}
