using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    private PhotonView PV;
    private GameObject Controller;
    private int Coin = 0;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PV.IsMine)
            createController();
    }

    public PhotonView getPV()
    {
        return PV;
    }

    public void gotNewCoin()
    {
        Coin++; // CHEH MOHAMED
        Debug.LogError(Coin);
    }

    public int getCoin()
    {
        return Coin;
    }

    public void createController()
    {
        // Ajout de la gestion du spawn lors de l'instantiate du playercontroller
        Transform mySpawn = FindObjectOfType<SpawnManager>().getSpawn(PhotonNetwork.LocalPlayer.ActorNumber);
        // on met la position de l'instantiate à myspawn.position                               vvvvvvv
        Controller = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "PlayerController"), mySpawn.position, Quaternion.identity);
    }

    public void Die()
    {
        return;
    }
}
