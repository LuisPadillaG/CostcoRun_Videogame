using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class UICanvas_Botones : MonoBehaviour
{

    private PlayerInput playerInput;
    private bool juegoPausado;

    void Start()
    {
        ObtenerComponentes();
        PrepararValoresIniciales();
    }

    void Update()
    {
       /*--- boton "p"---*/
            PreMenu();
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
