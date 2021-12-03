using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter2D;
    private BoxCollider2D boxCollider2D;
    private Collider2D[] hits = new Collider2D[10];
    protected virtual void Start() 
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    protected virtual void Update() 
    {
        boxCollider2D.OverlapCollider(filter2D, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i] == null)
                continue;
            
            hits[i] = null;
        }
    }
    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log(coll.name);
    }
}
