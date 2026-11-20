using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class UICanvas_Botones : MonoBehaviour
{

    private PlayerInput playerInput;
    private bool juegoPausado;
    bool interfaz_activa;
    [Header("CanvasPausa")]
    GameObject Canvas_Pausa;


    void Start()
    {
        ObtenerComponentes();
        PrepararValoresIniciales();
        Canvas_Pausa = GameObject.Find("Image");

    }

    void Update()
    {
       /*--- boton "p"---*/
       PreMenu();

       one_interfaz_canvas();

    }

    void ObtenerComponentes() // -- COMPONENTES
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void PrepararValoresIniciales() // -- VALORES INICIALES
    {
        juegoPausado = false;
        Time.timeScale = 1f;
    }


    /* -----------  -FUNCIONES-  ----------- */

    public void PreMenu()//-- restaura el menu
    {
        if (Keyboard.current != null && Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (juegoPausado)
            {
                juegoPausado = false;
                Time.timeScale = 1f; // Restaura el tiempo
            }
            else
            {
                menu();// pausa el tiempo
            }
        }
    }

    void one_interfaz_canvas()
    {
        if (juegoPausado == false)
        {
            PreMenu();
        }
    }



    /* -----------  -ONCLICK-  ----------- */
    public void menu()//-- cambia al menu (detiene el juego)
    {
        juegoPausado = true;
        Time.timeScale = 0f;
    }

    public void Continuar()//-- Continua el juego
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Jugar()//-- Comienza el juego de inico
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GAME_1");
    }

    public void Reintentar()//-- Devuelve el juego
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Resumuir()
    {
        juegoPausado = false;
        Canvas_Pausa.SetActive(false);//quita - Canvas_Pausa
        Time.timeScale = 1f;
    }

    public void Salir_Inicio()//-- Sale del juego al inicio
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Inicio");
    }

    public void Salir_Juego()//-- Sale del juego, juego
    {
        //Debug.Log("Saliendo");
        Application.Quit();
    }

    public void equisd()
    {

    }
}
