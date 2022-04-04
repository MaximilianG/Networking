using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Death : MonoBehaviour // systeme de respawn, script à mettre sur les gameobject qui tue (ennemy, spikes, lave, vide, etc)
{ // Ce script je l'ai fais moi même les autre et le profs ne l'ont pas encore
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController controller = collision.gameObject.GetComponent<PlayerController>(); // on recupere le PlayerController

            controller.SetHealthPoints(controller.GetHealthPoints() - 1); // pas besoin pour toi j'ai juste rajouté des PV

            if (PhotonNetwork.LocalPlayer.ActorNumber == 1 ) // On récupere l'id du joueur pour les spawns
                collision.gameObject.transform.position = GameObject.FindGameObjectWithTag("Respawn").transform.position;
            else
                collision.gameObject.transform.position = GameObject.FindGameObjectWithTag("Respawn2").transform.position;

        }
    }
}
