using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;
using System.Collections;
using System.Globalization;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public GameObject explosionprefab;
    float contadorcooldownDeath;
    bool activarContadorDeath = false;
    CharacterController characterController;
    PlayerInput playerInput;
    Animator animator;
    private string baseUrl = "https://apidecosco.onrender.com";
    float contador;

    [Header("Configuración de Salto y Gravedad")]
    [Range(10f, 50f)]
    [SerializeField] float fuerzaSalto = 15f;
    [Range(50f, 200f)]
    [SerializeField] float gravedad = 50f;
    float posicionX_inicial;
    Vector3 velocidad;
    bool is_grounded;
    float cooldownRecuperacion; //tiempo que tardas en volver a tu posicion normal

    float tiempoAgachado;
    [SerializeField] float alturaNormal = 2f;
    [SerializeField] float alturaAgachado = 1f;

    [Header("Configuración de Jetpack")]
    public GameObject modeloNormal;          // Modelo que corre y salta
    public GameObject modeloJetpack;         // Modelo con animación de vuelo
    [SerializeField] float alturaMinJetpack = 1f;    // Límite inferior al volar (valor -1)
    [SerializeField] float alturaMaxJetpack = 8f;    // Límite superior / techo (valor 1)
    [SerializeField] float suavizadoVuelo = 8f;      // Qué tan suave sube/baja al valor objetivo
    [SerializeField] float pausaConsultaJetpack = 0.05f; // Pausa mínima entre ciclos al servidor

    bool jetpackActivo = false;
    float valorJetpack = 0f;

    /*--------------ITEMS--------------------*/
    int recogida_solo_una_chancla = 0;
    int recogida_solo_una_carrito = 0;
    [Header("Chancla")]
    float fuerzaSalto_original;
    [Header("Carrito")]
    float velocidadAnimacion_original;

    void Start()
    {
        contadorcooldownDeath = 0.5f;
        contador = 0;
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        posicionX_inicial = this.transform.position.x;
        cooldownRecuperacion = 0;
        animator = this.transform.GetChild(0).GetComponent<Animator>();
        tiempoAgachado = 0f;
        if (alturaNormal == 2f) alturaNormal = characterController.height;

        // Estado inicial de modelos
        if (modeloNormal != null) modeloNormal.SetActive(true);
        if (modeloJetpack != null) modeloJetpack.SetActive(false);

        ITEMS();

        // Inicia el ciclo constante de baja latencia para el jetpack
        StartCoroutine(LoopServidorJetpack());
    }

    void Update()
    {
        if (activarContadorDeath)
        {
            contadorcooldownDeath -= Time.deltaTime;
            if (contadorcooldownDeath < 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        cooldownRecuperacion -= Time.deltaTime;

        // Recuperación en X (aplica tanto corriendo como volando)
        if (posicionX_inicial > this.transform.position.x && cooldownRecuperacion <= 0)
        {
            velocidad.x += Time.deltaTime * 0.5f;
        }
        else
        {
            velocidad.x = 0;
        }

        if (Physics.Raycast(this.transform.position + Vector3.up * 0.5f, Vector3.right, 0.35f)) // ------ DETECTAR OBSTÁCULO
        {
            cooldownRecuperacion = 0.3F;
        }

        // Si el jetpack está activo, usa el movimiento de vuelo y omite salto/gravedad normal
        if (jetpackActivo)
        {
            MoverConJetpack();
            return;
        }

        contador += Time.deltaTime;
        if (contador > 0.1f)
        {
            contador = 0;
            StartCoroutine(RevisarIsJump());
        }

        //normalizar variables 
        animator.SetInteger("Estado", 0);
        is_grounded = characterController.isGrounded;

        if (is_grounded && velocidad.y < 0) // ------ SUELO
        {
            velocidad.y = -2f;
        }

        if (tiempoAgachado > 0) // ------ AGACHARSE
        {
            tiempoAgachado -= Time.deltaTime;
            animator.SetInteger("Estado", 2);
            if (tiempoAgachado <= 0)
            {
                characterController.height = alturaNormal;
                characterController.center = new Vector3(0, alturaNormal / 2f, 0);
            }
        }
        else if (playerInput.actions["Crouch"].WasPressedThisFrame() && is_grounded)
        {
            tiempoAgachado = 0.2f;
            characterController.height = alturaAgachado;
            characterController.center = new Vector3(0, alturaAgachado / 2f, 0);

            animator.SetInteger("Estado", 3);
        }

        if (playerInput.actions["Jump"].WasPressedThisFrame() && is_grounded) // ------ SALTO
        {
            velocidad.y = fuerzaSalto;
            animator.SetInteger("Estado", 1);
        }

        velocidad.y -= gravedad * Time.deltaTime; //------ GRAVEDAD
        characterController.Move(velocidad * Time.deltaTime);
    }

    /*--------------JETPACK--------------------*/

    void CambiarEstadoJetpack(bool activar)
    {
        if (jetpackActivo == activar) return;

        jetpackActivo = activar;

        if (modeloNormal != null) modeloNormal.SetActive(!activar);
        if (modeloJetpack != null) modeloJetpack.SetActive(activar);

        // Reinicia la velocidad vertical al entrar o salir del vuelo
        velocidad.y = 0f;

        // Si estaba agachado al activar el jetpack, restaura la altura del collider
        if (activar && tiempoAgachado > 0)
        {
            tiempoAgachado = 0f;
            characterController.height = alturaNormal;
            characterController.center = new Vector3(0, alturaNormal / 2f, 0);
        }
    }

    void MoverConJetpack()
    {
        // Convierte el rango [-1, 1] del celular a [alturaMinJetpack, alturaMaxJetpack]
        float t = (valorJetpack + 1f) * 0.5f;
        float alturaObjetivo = Mathf.Lerp(alturaMinJetpack, alturaMaxJetpack, t);

        float nuevaY = Mathf.Lerp(transform.position.y, alturaObjetivo, suavizadoVuelo * Time.deltaTime);
        float desplazamientoY = nuevaY - transform.position.y;

        characterController.Move(new Vector3(velocidad.x * Time.deltaTime, desplazamientoY, 0f));
    }

    IEnumerator LoopServidorJetpack()
    {
        while (true)
        {
            // 1. Revisa si el jetpack está encendido
            using (UnityWebRequest reqEstado = UnityWebRequest.Get(baseUrl + "/is_jetpack"))
            {
                yield return reqEstado.SendWebRequest();

                if (reqEstado.result == UnityWebRequest.Result.Success)
                {
                    bool activoEnServidor = reqEstado.downloadHandler.text.Trim().ToLower() == "true";
                    CambiarEstadoJetpack(activoEnServidor);
                }
            }

            // 2. Solo pide el valor de vuelo si el jetpack está activo (ahorra peticiones)
            if (jetpackActivo)
            {
                using (UnityWebRequest reqValor = UnityWebRequest.Get(baseUrl + "/recibir_valor_jetpack"))
                {
                    yield return reqValor.SendWebRequest();

                    if (reqValor.result == UnityWebRequest.Result.Success)
                    {
                        string texto = reqValor.downloadHandler.text.Trim();
                        if (float.TryParse(texto, NumberStyles.Float, CultureInfo.InvariantCulture, out float valor))
                        {
                            valorJetpack = Mathf.Clamp(valor, -1f, 1f);
                        }
                    }
                }
            }

            yield return new WaitForSeconds(pausaConsultaJetpack);
        }
    }

    /*--------------ITEMS--------------------*/
    void ITEMS()
    {
        fuerzaSalto_original = fuerzaSalto;//v "Chancla" v
        velocidadAnimacion_original = animator.speed;//v "Carrito" v
    }

    void Recogida_Solo_Una()
    {
        if (recogida_solo_una_chancla >= 1)
        {
            AumentarSalto();
            RecuperarSalto();
        }

        if (recogida_solo_una_carrito >= 1)
        {
            AumentarVelocidad();
            RecuperarVelocidad();
        }
    }

    //------- CHANCLA (De = chancla.cs)
    public void AumentarSalto()
    {
        fuerzaSalto += 10f;
    }

    public void RecuperarSalto()
    {
        fuerzaSalto = fuerzaSalto_original;
    }

    //------- CARRITO (De = carrito.cs)
    public void AumentarVelocidad()
    {
        animator.speed = 1.5f;
    }

    public void RecuperarVelocidad()
    {
        animator.speed = velocidadAnimacion_original;
    }

    IEnumerator RevisarIsJump()
    {
        using (UnityWebRequest webrequest = UnityWebRequest.Get(baseUrl + "/is_jump"))
        {
            yield return webrequest.SendWebRequest();

            if (webrequest.result == UnityWebRequest.Result.Success)
            {
                string respuesta = webrequest.downloadHandler.text.Trim().ToLower();
                bool salto = (respuesta == "true");

                if (salto && is_grounded && !jetpackActivo) // ------ SALTO
                {
                    velocidad.y = fuerzaSalto;
                    animator.SetInteger("Estado", 1);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "GameOver")
        {
            Instantiate(explosionprefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.6f);
            activarContadorDeath = true;
        }
    }
}