using UnityEngine;

public class AttachMe : MonoBehaviour // Script d'Ivan pour rester accroché à une platform.
{
    public LayerMask layerFilter;

    void OnCollisionEnter2D(Collision2D collision) // A l'entrée parente moi !
    {
        if (collision.gameObject.layer == layerFilter.value)
        {
            collision.collider.transform.SetParent(transform, false);
        }
    }

    void OnCollisionExit2D(Collision2D collision) // A la sortie déparente moi !
    {
        if (collision.gameObject.layer == layerFilter.value)
        {
            collision.collider.transform.SetParent(null);
        }
    }
}