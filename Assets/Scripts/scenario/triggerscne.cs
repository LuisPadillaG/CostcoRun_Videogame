using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class triggerscne : MonoBehaviour
{
    void Start(){}void Update(){}

    private void OnTriggerEnter(Collider other)
    {
        DrestruirPlayer(other);
        DesactivarScript(other);
        DesactivarPlayerInput(other);
    }

    void DrestruirPlayer(Collider other)//------ Elimina al jugador
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.childCount > 0)//busca si ahi 
            {
                Destroy(other.transform.GetChild(0).gameObject);//busca dentro gameObjet su hijo
            }
        }
    }

    void DesactivarScript(Collider other)//-------- Deselecion el Check Scripts
    {
        if (other.CompareTag("Player"))
        {
            //script: desactivar.cs
            desactivar Scripdesactivar = other.GetComponent<desactivar>();
            mandoJugador ScripmandoJugador =other.GetComponent<mandoJugador>();

            if (Scripdesactivar != null)
            {
                Scripdesactivar.enabled = false;
                ScripmandoJugador.enabled = false;
            }

            Debug.Log("Toque");
        }
    }

    void DesactivarPlayerInput(Collider other)//-------- Deselecion playerinput
    {
        if (other.CompareTag("Player"))
        {
            PlayerInput playerInput = other.GetComponent<PlayerInput>();
            if (playerInput != null) playerInput.enabled = false;
        }
    }

}
