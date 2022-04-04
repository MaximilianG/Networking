using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OpenDoor : MonoBehaviour // script Qui permet d'envoyer l'information aux autres joueurs qu'on ouvre la porte (disable l'objet)
{
    [SerializeField] PhotonView DoorPV; // PV de la porte
    private PhotonView PV; // PV du levier (nécessaire à l'envoit du PV.RPC)

    private void Awake()
    {
        PV = GetComponent<PhotonView>(); // on récupere le PV du bouton
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PV.RPC("openDoor", RpcTarget.All); // envoit du message de l'appel de la fonction à tous le monde
        }
    }

    [PunRPC] // IMPORTANT
    public void openDoor()
    {
        DoorPV.gameObject.SetActive(false); // désactive le gameobject
    }

    private void OnTriggerExit2D(Collider2D collision) // pareil pour fermer
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PV.RPC("closeDoor", RpcTarget.All);
        }
    }

    [PunRPC]
    public void closeDoor()
    {
        DoorPV.gameObject.SetActive(true);
    }
}
