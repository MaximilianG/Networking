using UnityEngine;

public class AttachMe : MonoBehaviour // Script d'Ivan pour rester accroch� � une platform.
{
    public LayerMask layerFilter;

    void OnCollisionEnter2D(Collision2D collision) // A l'entr�e parente moi !
    {
         collision.collider.transform.SetParent(transform);
    }

    void OnCollisionExit2D(Collision2D collision) // A la sortie d�parente moi !
    {
         collision.collider.transform.SetParent(null);
    }
}