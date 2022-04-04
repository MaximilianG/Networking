using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomPseudoItem : MonoBehaviourPunCallbacks
{
    private Player player;
    [SerializeField] TMP_Text playerName;

    public void Setup(Player _player)
    {
        player = _player;
        playerName.text = _player.NickName;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (player == newPlayer)
            Destroy(gameObject);
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject);
    }
}
