using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using System.Linq;

public class CoinCountDisplay : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text coinText;

    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        PV.RPC("addCoin", RpcTarget.AllBuffered);
    }

    [PunRPC]

    public void addCoin()
    {
        coinText.text = FindObjectsOfType<PlayerManager>().Where(x => x.getPV().IsMine).ToArray()[0].getCoin().ToString();
    }
}
