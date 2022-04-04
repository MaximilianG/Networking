using UnityEngine;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class RoomItemButton : MonoBehaviour
{
    [SerializeField] TMP_Text roomName;
    public RoomInfo roomInfo;

    public void SetupRoomButton(RoomInfo _info)
    {
        roomName.text = _info.Name;
        roomInfo = _info;
    }

    public void JoinRoom()
    {
        MenuManager.Instance.openMenu("LoadingMenu");
        PhotonNetwork.JoinRoom(roomInfo.Name);
    }

}
