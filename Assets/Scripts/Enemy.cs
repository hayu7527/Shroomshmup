using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = System.Random;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")] 
    public float speed = 5f;

    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;

    private BoundsCheck bndCheck;
    
    public float amplitude = 0.5f;
    public float frequency = 1f;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
            
        }

        set { this.transform.position = value; }
    }
    void Update()
    {
        Move();

        if (bndCheck != null && !bndCheck.isOnScreen)
        {
            if (pos.y < bndCheck.camHeight - bndCheck.radius)
            {
                Destroy(gameObject);
            }
        }

    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        tempPos.x += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        pos = tempPos;
    }

    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO);
            Destroy(gameObject);
        }
        
        if (otherGO.tag == "ProjectileHero2")
        {
            Destroy(otherGO);
            Destroy(gameObject);
        }

        else
        {
            print("Enemy hit by non-projectilehero:" +otherGO.name);
        }
    }
}
