using System.Collections.Generic;
using UnityEngine;

public class CreadorDeModulo : MonoBehaviour
{
    //plan: inserta los otros modulos (distintos a este no manches 
    [Header("Modulos")] 
    public List<GameObject> modulosDisponibles = new List<GameObject>();
    Transform posicionFinalDelModulo;
    int eleccionProximoModulo; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicionFinalDelModulo = this.transform.GetChild(0).GetComponent<Transform>();
        eleccionProximoModulo = Random.Range(0, modulosDisponibles.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Deberiamos de crear un nuevo modulo papu");
            Instantiate(modulosDisponibles[eleccionProximoModulo], posicionFinalDelModulo.position, Quaternion.identity);
        }
    }
}
