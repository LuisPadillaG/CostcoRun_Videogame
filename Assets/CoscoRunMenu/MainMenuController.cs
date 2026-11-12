using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// MainMenuController
// Controla la pantalla de inicio de Cosco Run.
// Se encarga de los botones del menú: Start Game y Options.
// Este script va pegado a un objeto del menú (el builder lo hace solo).

public class MainMenuController : MonoBehaviour
{
    [Header("Escena del juego")]
    [Tooltip("Nombre EXACTO de la escena de tu juego, tal como aparece en File > Build Settings.")]
    public string gameSceneName = "Game";

    [Header("Paneles")]
    [Tooltip("El panel de opciones que se muestra/oculta al presionar Options.")]
    public GameObject optionsPanel;

    [Header("Botones (se conectan solos)")]
    public Button startButton;
    public Button optionsButton;
    public Button closeButton;

    // Conectamos cada botón con su acción al arrancar el juego.
    // Hacerlo aquí por código garantiza que el clic SIEMPRE funcione.
    private void Awake()
    {
        if (startButton != null)
            startButton.onClick.AddListener(OnStartGame);

        if (optionsButton != null)
            optionsButton.onClick.AddListener(OnOpenOptions);

        if (closeButton != null)
            closeButton.onClick.AddListener(OnCloseOptions);
    }

    // Al arrancar, nos aseguramos de que el panel de opciones empiece oculto.
    private void Start()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }

    // --- Se conecta al botón "Start Game" ---
    public void OnStartGame()
    {
        // Si pusiste bien el nombre de la escena, la carga.
        if (!string.IsNullOrEmpty(gameSceneName))
        {
            Debug.Log("Cargando escena: " + gameSceneName);
            SceneManager.LoadScene(gameSceneName);
        }
        else
        {
            Debug.LogWarning("No hay nombre de escena asignado en gameSceneName.");
        }
    }

    // --- Se conecta al botón "Options" ---
    public void OnOpenOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(true);
    }

    // --- Se conecta al botón "Cerrar" dentro del panel de opciones ---
    public void OnCloseOptions()
    {
        if (optionsPanel != null)
            optionsPanel.SetActive(false);
    }

    // --- Opcional: botón de salir del juego ---
    public void OnQuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

#if UNITY_EDITOR
        // En el editor, Application.Quit no hace nada, así que paramos el Play.
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
