using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class CambioEscenaAlTerminarVideo : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private string nombreSiguienteEscena = "Juego";
    private VideoPlayer videoPlayer;
    private void Awake()
    {
        if (menuCanvas != null)
        {
            // Busca VideoPlayer en MenuCanvas o en sus hijos
            videoPlayer = menuCanvas.GetComponentInChildren<VideoPlayer>();
        }
    }
    private void OnEnable()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += AlTerminarVideo;
        }
    }
    private void OnDisable()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= AlTerminarVideo;
        }
    }
    private void AlTerminarVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene(nombreSiguienteEscena);
    }
}
