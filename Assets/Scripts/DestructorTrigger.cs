using UnityEngine;

public class DestructorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Destruye el objeto completo (si el collider está en un hijo, destruye al padre raíz)
        if (other.attachedRigidbody != null)
        {
            Destroy(other.attachedRigidbody.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}