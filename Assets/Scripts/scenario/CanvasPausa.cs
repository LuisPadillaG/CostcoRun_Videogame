
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CanvasPausa : MonoBehaviour
{
    [Header("Canvas")]
    GameObject Canvas_Pausa;
    //Canvas Canvas_Pausa;
    bool pausado = false;

    void Start()
    {
        //Canvas_Pausa = GameObject.Find("CanvasPausa");
        //Canvas_Pausa = GameObject.Find("CanvasPausa").GetComponent<Canvas>();
        //Canvas[] todosLosCanvas = FindObjectsByType<Canvas>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        //Canvas_Pausa = transform.Find("CanvasPausa").gameObject;
        GameObject Padre = GameObject.Find("CanvasPausa");
        Canvas_Pausa = Padre.transform.Find("Image").gameObject;
        Canvas_Pausa.SetActive(false);//Canvas_Pausa.enabled = false;
        Time.timeScale = 1f;
    }

    void Update()
    {
        controlPausa_reanudar();
    }

    void controlPausa_reanudar()
    {
        //if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (pausado == false)
            {
                pausa();
            }
            else
            {
                reanudar();
            }
        }
    }

    void pausa()
    {
        pausado = true;
        Canvas_Pausa.SetActive(true);//Canvas_Pausa.enabled = true;
        Time.timeScale = 0f;
    }

    void reanudar()
    {
        pausado = false;
        Canvas_Pausa.SetActive(false);//Canvas_Pausa.enabled = false;
        Time.timeScale = 1f;
    }
}
