using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Launcher Instance;

    [SerializeField] Transform RoomImage;
    [SerializeField] GameObject ButtonRoomPrefab;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform PseudoImage;
    [SerializeField] GameObject PseudoPrefab;
    [SerializeField] Button StartGameButton;

    [Header("InputCheck")]
    [SerializeField] Button CreateButtonMainMenu;
    [SerializeField] Button JoinButtonMainMenu;
    [SerializeField] TMP_InputField InputFieldMainMenu;
    [SerializeField] Button CreateButtonCreateRoomMenu;


    [Header("ErrorLabels")]
    [SerializeField] TMP_Text ErrorPseudoText, ErrorRoomText;

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartGameButton.interactable = true;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (PhotonNetwork.IsConnected == false )
            PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true; // si on change de scène, ça le fait chez tous les users de la room
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.openMenu("MainMenu");
        Debug.Log("Joined Lobby :)"); 
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform transform in RoomImage)
        {
            Destroy(transform.gameObject);
        }

        for (int i = 0; i < roomList.Count; i++)
            if (!roomList[i].RemovedFromList)
                Instantiate(ButtonRoomPrefab, RoomImage).GetComponent<RoomItemButton>().SetupRoomButton(roomList[i]);
    }

    public void createRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }

        MenuManager.Instance.openMenu("LoadingMenu");
        PhotonNetwork.CreateRoom(roomNameInputField.text);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) // Si un player entre dans notre room on reçoit un pseudo
    {
        Instantiate(PseudoPrefab, PseudoImage).GetComponent<RoomPseudoItem>().Setup(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) // meme chose mais s'il part
    {
        Player[] players = PhotonNetwork.PlayerList; // Récupère tous les players de la room

        foreach (Transform transform in PseudoImage) // Détruit tout les prefabs pseudos actuellement dans l'image
        {
            Destroy(transform.gameObject);
        }

        foreach (Player p in players) // Boucle sur tout les players de notre tableau récupérés précédemment
            Instantiate(PseudoPrefab, PseudoImage).GetComponent<RoomPseudoItem>().Setup(p); // Instantie le player de [i] du tablea dans l'image
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
            StartGameButton.interactable = true;

        // Changer le nom de la room dans l'ui
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
        // Instancier tout les mecs dans la room

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform transform in PseudoImage)
        {
            Destroy(transform.gameObject);
        }

        foreach (Player p in players)
            Instantiate(PseudoPrefab, PseudoImage).GetComponent<RoomPseudoItem>().Setup(p);


        MenuManager.Instance.openMenu("RoomMenu");
    }

    public void setUserName(string actualUsername)
    {
        if (InputFieldMainMenu.text == null || InputFieldMainMenu.text == "")
        {
            CreateButtonMainMenu.interactable = false;
            JoinButtonMainMenu.interactable = false;
            return;
        }
        else
        {
            CreateButtonMainMenu.interactable = true;
            JoinButtonMainMenu.interactable = true;
        }
        
        PhotonNetwork.NickName = actualUsername;

    }

    public void setRoomName()
    {
        if (roomNameInputField.text == null || roomNameInputField.text == "")
        {
            CreateButtonCreateRoomMenu.interactable = false;
            return;
        }
        else
        {
            CreateButtonCreateRoomMenu.interactable = true;
        }
    }

    public void QuitRoom()
    {
        PhotonNetwork.LeaveRoom();
        StartGameButton.interactable = false;
        MenuManager.Instance.openMenu("JoinRoomMenu");
        
    }

    public void LaunchGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

}
