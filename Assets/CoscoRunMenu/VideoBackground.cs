using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// VideoBackground
// Reproduce un video como fondo de la pantalla de inicio.
// Crea automáticamente la "textura" donde se dibuja el video,
// así no tienes que configurar un Render Texture a mano.

[RequireComponent(typeof(VideoPlayer))]
public class VideoBackground : MonoBehaviour
{
    [Tooltip("El RawImage que llena la pantalla y muestra el video.")]
    public RawImage targetImage;

    [Tooltip("Tu archivo de video (arrástralo aquí: .mp4, .mov, .webm).")]
    public VideoClip videoClip;

    private VideoPlayer videoPlayer;
    private RenderTexture renderTexture;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        // Creamos una textura del tamaño de la pantalla para proyectar el video.
        renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
        renderTexture.Create();

        // Conectamos el video a esa textura, y la textura al RawImage.
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.targetTexture = renderTexture;

        if (targetImage != null)
            targetImage.texture = renderTexture;

        // Configuración del reproductor: en bucle, con audio opcional.
        videoPlayer.isLooping = true;
        videoPlayer.playOnAwake = true;
        videoPlayer.waitForFirstFrame = true;

        // Si asignaste un clip por código, lo usamos.
        if (videoClip != null)
        {
            videoPlayer.source = VideoSource.VideoClip;
            videoPlayer.clip = videoClip;
        }

        videoPlayer.Play();
    }

    private void OnDestroy()
    {
        // Liberamos la textura al cerrar para no dejar memoria ocupada.
        if (renderTexture != null)
        {
            renderTexture.Release();
            Destroy(renderTexture);
        }
    }
}
