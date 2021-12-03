using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PhotonView;
    private List<GameObject> diemBatDau = new List<GameObject>();
    private Transform target;
    void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
    }
    void Start() 
    {
        target = GameObject.FindGameObjectWithTag("DiemBatDau").GetComponent<Transform>();
        foreach (Transform trans in target)
        {
            diemBatDau.Add(trans.gameObject);
        }
        if (PhotonView.IsMine)
        {
            CreateController();
        }
    }
    void CreateController()
    { 
        Vector3 pos = diemBatDau[Random.Range(0, diemBatDau.Count)].transform.position;
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), pos, Quaternion.identity);
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);        
    }
}
