using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class Weapon : Collidable
{
    [SerializeField] private PhotonView PV;
    public float damagePoint = 10f;
    public float pushForce = 2.0f;
    //Upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;
    //Swing
    private Animator animator;
    private float cooldown = 0.5f;
    private float lastSwing;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if(PV.IsMine && Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }            
    }
    private void OnCollisionStay2D(Collision2D other) {
        //if(PV.IsMine)
        //    return;
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().UpdateHealth(-damagePoint);         
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        //if(PV.IsMine)
        //    return;
        Debug.Log(coll.name);
    }
    public void Swing()
    {
        animator.SetTrigger("Swing");
    }
}
