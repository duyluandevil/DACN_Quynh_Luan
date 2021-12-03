using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class UsernameDisplay : MonoBehaviour
{
    [SerializeField] PhotonView photonView;
    [SerializeField] TMP_Text text;

    private void Start() 
    {
        if (photonView.IsMine)
        {
            gameObject.SetActive(false);
        }
        text.text = photonView.Owner.NickName;
    }
}
