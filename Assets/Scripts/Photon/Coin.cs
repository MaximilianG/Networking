using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Coin : MonoBehaviour // On appel la fonction OnCoin du player controller (l� o� on va g�rer l'envoit du msg � tous le monde)
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerController>().OnCoin(gameObject.GetComponent<PhotonView>());
        }
    }

}
