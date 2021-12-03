using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class KhoiChay : MonoBehaviourPunCallbacks
{
    public static KhoiChay Instance;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform listRoom;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] Transform listPlayer;
    [SerializeField] GameObject playerListItemPrefab;
    [SerializeField] GameObject startGameButton;


    void Awake() {
        Instance = this;
    }

    void Start()
    {
        Debug.Log("Connect to Master");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("menu");
        Debug.Log("Joined Lobby");
    }

    public void TaoPhong()
    {
        if(string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenu("loading");
    }  

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Player[] players = PhotonNetwork.PlayerList;

        foreach (Transform trans in listPlayer)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            Instantiate(playerListItemPrefab, listPlayer).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public void StartGame(){
        PhotonNetwork.LoadLevel(1);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuManager.Instance.OpenMenu("error");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("loading");
    }

    public void VaoPhong(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("menu");
        Debug.Log("LeftRoomCountOfRooms " + PhotonNetwork.CountOfRooms.ToString());
    }

    /*private List<RoomListItem> _listroom = new List<RoomListItem>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {  
        foreach(RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _listroom.FindIndex(x => x.info.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_listroom[index].gameObject);
                    _listroom.RemoveAt(index);
                }
            }
            else
            {
                if(Instantiate(roomListItemPrefab, listRoom).GetComponent<RoomListItem>() != null){
                    Instantiate(roomListItemPrefab, listRoom).GetComponent<RoomListItem>().SetUp(info);
                    _listroom.Add(Instantiate(roomListItemPrefab, listRoom).GetComponent<RoomListItem>());
                }               
            }
        }
        Debug.Log("CountOfRooms " + PhotonNetwork.CountOfRooms.ToString());
    }*/

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform trans in listRoom)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            if(roomList[i].RemovedFromList)
                continue;
            Instantiate(roomListItemPrefab, listRoom).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerListItemPrefab, listPlayer).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
