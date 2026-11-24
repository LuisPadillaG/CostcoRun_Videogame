using UnityEngine;

public class DestructorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        DestruirObjetivo(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DestruirObjetivo(collision.collider);
    }

    private void DestruirObjetivo(Collider col)
    {
        // Destruye el objeto raíz si tiene Rigidbody, o el objeto directo si no lo tiene
        if (col.attachedRigidbody != null)
        {
            Destroy(col.attachedRigidbody.gameObject);
        }
        else
        {
            Destroy(col.gameObject);
        }
    }
}