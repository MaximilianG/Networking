using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] spawns; // on drag & drop un gameobject contenant plusieurs gameobject de spawn en enfant

    public Transform getSpawn(int id) // on recupere l'id du joueur (entre 1 et 2 pour 2 joueurs)
    {
        return spawns[id-1]; // et on lui assigne le spawn en indice 0 ou 1 du tableau
    }
}
