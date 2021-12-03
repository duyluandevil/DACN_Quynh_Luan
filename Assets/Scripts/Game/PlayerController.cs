using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

public class PlayerController : MonoBehaviourPunCallbacks
{
    private Rigidbody2D rb;
    private Transform username, healthy;
    private Animator animator;
    //private SpriteRenderer spriteRenderer;
    private PhotonView PV;
    private float moveH, moveV;
    private Vector3 moveDelta;
    [SerializeField] private float moveSpeed = 5.0f;
    private float health = 0f;
    [SerializeField] TMP_Text text;
    [SerializeField] private float maxHealth = 100f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        username = GameObject.FindGameObjectWithTag("Username").GetComponent<Transform>();
        healthy = GameObject.FindGameObjectWithTag("Health").GetComponent<Transform>();

        //spriteRenderer = GetComponent<SpriteRenderer>();
        PV = GetComponent<PhotonView>();
        health = maxHealth;
        if (PV.IsMine)
        {
            healthy.gameObject.SetActive(false);
        }
    }
    public void UpdateHealth(float mod)
    {
        health += mod;
        if(health > maxHealth)
        {
            health = maxHealth;
        }else if (health <= 0f)
        {
            health = 0f;
            Debug.Log("Die");
        }
        text.text = health.ToString();
    }
    private void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        moveH = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveV = Input.GetAxisRaw("Vertical") * moveSpeed;

        moveDelta = new Vector3(moveH, moveV, 0);
        if (moveDelta.x < 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(moveH, moveV);
        if (PV.IsMine)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("changeScale", transform.localScale);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hashtable);
        }     
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!PV.IsMine && targetPlayer == PV.Owner)
        {
            transform.localScale = username.localScale = healthy.localScale = (Vector3) changedProps["changeScale"];
        }
    }
}
