using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.EventSystems;
using UnityEditor;
using UnityEditor.SceneManagement;

// MainMenuBuilder (script de EDITOR)
// Crea la pantalla de inicio completa de Cosco Run con un solo clic.
// Menú de Unity:  Cosco Run > Crear pantalla de inicio
//
// Arma: cámara, fondo de video, título, botones Start Game / Options,
// panel de opciones y todo conectado. Tú solo arrastras tu video y tu
// logo después.

public class MainMenuBuilder
{
    // Colores base (estilo retro oscuro como la referencia)
    private static readonly Color MenuTextColor = Color.white;
    private static readonly Color AccentColor = new Color(0.90f, 0.20f, 0.20f); // rojo título

    [MenuItem("Cosco Run/Crear pantalla de inicio")]
    public static void BuildMainMenu()
    {
        // 1. Creamos una escena nueva y vacía para el menú.
        var scene = EditorSceneManager.NewScene(
            NewSceneSetup.EmptyScene,
            NewSceneMode.Single
        );

        // 2. Cámara principal.
        GameObject camGO = new GameObject("Main Camera");
        Camera cam = camGO.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = Color.black;
        cam.orthographic = true;
        camGO.tag = "MainCamera";
        camGO.AddComponent<AudioListener>();

        // 3. EventSystem (necesario para que los botones respondan al clic).
        GameObject esGO = new GameObject("EventSystem");
        esGO.AddComponent<EventSystem>();
        esGO.AddComponent<StandaloneInputModule>();

        // 4. Canvas principal (todo el UI vive aquí).
        GameObject canvasGO = new GameObject("MenuCanvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;
        canvasGO.AddComponent<GraphicRaycaster>();

        // 5. Fondo de VIDEO (RawImage que llena toda la pantalla).
        GameObject bgGO = new GameObject("VideoBackground");
        bgGO.transform.SetParent(canvasGO.transform, false);
        RawImage bgImage = bgGO.AddComponent<RawImage>();
        bgImage.color = Color.white;
        StretchFull(bgGO.GetComponent<RectTransform>());

        // Objeto con el VideoPlayer + nuestro script VideoBackground.
        GameObject videoGO = new GameObject("VideoPlayer");
        videoGO.transform.SetParent(canvasGO.transform, false);
        VideoPlayer vp = videoGO.AddComponent<VideoPlayer>();
        vp.playOnAwake = true;
        vp.isLooping = true;
        vp.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoGO.AddComponent<AudioSource>();
        VideoBackground vbg = videoGO.AddComponent<VideoBackground>();
        vbg.targetImage = bgImage;

        // Capa oscura encima del video para que el texto se lea mejor.
        GameObject dimGO = new GameObject("Overlay");
        dimGO.transform.SetParent(canvasGO.transform, false);
        Image dim = dimGO.AddComponent<Image>();
        dim.color = new Color(0f, 0f, 0f, 0.35f);
        StretchFull(dimGO.GetComponent<RectTransform>());

        // 6. TÍTULO (texto grande arriba; luego puedes cambiarlo por tu logo).
        GameObject titleGO = CreateText(
            "Title",
            canvasGO.transform,
            "COSCO RUN",
            90,
            FontStyle.Bold,
            AccentColor
        );
        RectTransform titleRT = titleGO.GetComponent<RectTransform>();
        titleRT.anchorMin = new Vector2(0.5f, 1f);
        titleRT.anchorMax = new Vector2(0.5f, 1f);
        titleRT.pivot = new Vector2(0.5f, 1f);
        titleRT.anchoredPosition = new Vector2(0, -120);
        titleRT.sizeDelta = new Vector2(1200, 200);

        // 7. Contenedor de los botones del menú (centro).
        GameObject menuGO = new GameObject("MenuButtons");
        menuGO.transform.SetParent(canvasGO.transform, false);
        RectTransform menuRT = menuGO.AddComponent<RectTransform>();
        menuRT.anchorMin = new Vector2(0.5f, 0.5f);
        menuRT.anchorMax = new Vector2(0.5f, 0.5f);
        menuRT.pivot = new Vector2(0.5f, 0.5f);
        menuRT.anchoredPosition = new Vector2(0, -60);
        menuRT.sizeDelta = new Vector2(400, 200);
        VerticalLayoutGroup vlg = menuGO.AddComponent<VerticalLayoutGroup>();
        vlg.spacing = 20;
        vlg.childAlignment = TextAnchor.MiddleCenter;
        vlg.childControlWidth = true;
        vlg.childControlHeight = true;
        vlg.childForceExpandWidth = true;
        vlg.childForceExpandHeight = false;

        // Botones Start Game y Options.
        GameObject startBtn = CreateMenuButton("StartButton", menuGO.transform, "Start Game");
        GameObject optionsBtn = CreateMenuButton("OptionsButton", menuGO.transform, "Options");

        // 8. PANEL DE OPCIONES (empieza oculto).
        GameObject panelGO = new GameObject("OptionsPanel");
        panelGO.transform.SetParent(canvasGO.transform, false);
        Image panelBg = panelGO.AddComponent<Image>();
        panelBg.color = new Color(0.05f, 0.05f, 0.08f, 0.95f);
        RectTransform panelRT = panelGO.GetComponent<RectTransform>();
        panelRT.anchorMin = new Vector2(0.5f, 0.5f);
        panelRT.anchorMax = new Vector2(0.5f, 0.5f);
        panelRT.pivot = new Vector2(0.5f, 0.5f);
        panelRT.anchoredPosition = Vector2.zero;
        panelRT.sizeDelta = new Vector2(700, 500);

        GameObject panelTitle = CreateText(
            "PanelTitle", panelGO.transform, "Opciones", 48, FontStyle.Bold, MenuTextColor
        );
        RectTransform ptRT = panelTitle.GetComponent<RectTransform>();
        ptRT.anchorMin = new Vector2(0.5f, 1f);
        ptRT.anchorMax = new Vector2(0.5f, 1f);
        ptRT.pivot = new Vector2(0.5f, 1f);
        ptRT.anchoredPosition = new Vector2(0, -40);
        ptRT.sizeDelta = new Vector2(600, 80);

        // Botón para cerrar el panel.
        GameObject closeBtn = CreateMenuButton("CloseButton", panelGO.transform, "Cerrar");
        RectTransform closeRT = closeBtn.GetComponent<RectTransform>();
        closeRT.anchorMin = new Vector2(0.5f, 0f);
        closeRT.anchorMax = new Vector2(0.5f, 0f);
        closeRT.pivot = new Vector2(0.5f, 0f);
        closeRT.anchoredPosition = new Vector2(0, 50);
        closeRT.sizeDelta = new Vector2(300, 70);

        panelGO.SetActive(false);

        // 9. Controlador del menú: conectamos todo.
        MainMenuController controller = canvasGO.AddComponent<MainMenuController>();
        controller.optionsPanel = panelGO;
        controller.gameSceneName = "Game";

        // Le pasamos los botones al controlador (estas referencias SÍ se guardan
        // en la escena). El controlador los conecta solo al presionar Play.
        controller.startButton = startBtn.GetComponent<Button>();
        controller.optionsButton = optionsBtn.GetComponent<Button>();
        controller.closeButton = closeBtn.GetComponent<Button>();

        // 10. Guardar la escena.
        string folder = "Assets/Scenes";
        if (!AssetDatabase.IsValidFolder(folder))
            AssetDatabase.CreateFolder("Assets", "Scenes");

        string path = folder + "/MainMenu.unity";
        EditorSceneManager.SaveScene(scene, path);

        Selection.activeGameObject = videoGO;
        EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<Object>(path));

        Debug.Log(
            "✅ Pantalla de inicio creada en " + path +
            "\nSiguiente paso: selecciona el objeto 'VideoPlayer' y arrastra tu video al campo 'Video Clip'."
        );
        EditorUtility.DisplayDialog(
            "Cosco Run",
            "¡Pantalla de inicio creada!\n\n" +
            "Siguientes pasos:\n" +
            "1. Selecciona el objeto 'VideoPlayer' en la jerarquía.\n" +
            "2. Arrastra tu video al campo 'Video Clip'.\n" +
            "3. (Opcional) Cambia el texto del título por tu logo.\n" +
            "4. En 'MenuCanvas' pon el nombre de tu escena de juego en 'Game Scene Name'.\n\n" +
            "Presiona Play para probarlo.",
            "¡Entendido!"
        );
    }

    // ---------- Funciones de ayuda ----------

    // Estira un RectTransform para que llene toda la pantalla.
    private static void StretchFull(RectTransform rt)
    {
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    // Crea un texto con la fuente integrada de Unity (sin configurar nada).
    private static GameObject CreateText(
        string name, Transform parent, string content,
        int fontSize, FontStyle style, Color color)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        Text txt = go.AddComponent<Text>();
        txt.text = content;
        txt.font = (Font)Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf");
        txt.fontSize = fontSize;
        txt.fontStyle = style;
        txt.color = color;
        txt.alignment = TextAnchor.MiddleCenter;
        txt.horizontalOverflow = HorizontalWrapMode.Overflow;
        txt.verticalOverflow = VerticalWrapMode.Overflow;
        return go;
    }

    // Crea un botón de menú con su texto.
    private static GameObject CreateMenuButton(string name, Transform parent, string label)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);

        Image img = go.AddComponent<Image>();
        img.color = new Color(0f, 0f, 0f, 0.45f);

        Button btn = go.AddComponent<Button>();

        // Efecto visual al pasar el mouse / presionar.
        ColorBlock cb = btn.colors;
        cb.normalColor = new Color(0f, 0f, 0f, 0.45f);
        cb.highlightedColor = AccentColor;
        cb.pressedColor = new Color(0.7f, 0.15f, 0.15f, 1f);
        cb.fadeDuration = 0.1f;
        btn.colors = cb;

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(360, 64);

        LayoutElement le = go.AddComponent<LayoutElement>();
        le.preferredHeight = 64;
        le.preferredWidth = 360;

        // Texto dentro del botón.
        GameObject txtGO = CreateText(name + "_Label", go.transform, label, 32, FontStyle.Bold, MenuTextColor);
        StretchFull(txtGO.GetComponent<RectTransform>());

        return go;
    }
}
