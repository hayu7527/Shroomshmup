using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotcount : MonoBehaviour
{
    static public shotcount S;
    private GameObject lastTriggerGo = null;
    
    [Header("Set Dynamically")] [SerializeField]
    private float count1 = 0;
    [Header("Set Dynamically")] [SerializeField]
    private float count2 = 0;

    public GameObject projectilePrefab;
    public float projectileSpeed = 10;
    public float speed = 1f;
    private float timer = 1;
    
    public float amplitude = 0.5f;
    public float frequency = 1f;
    void Awake()
    {
        if (S == null)
        {
            S = this;
        }

        else
        {
            Debug.LogError("shotcount.Awake() - Attempted to assign second shotcount.S!");

        }
        

            SpriteRenderer renderer = GetComponent<SpriteRenderer>();
            renderer.color = new Color(1f, 1f, 1f); 
            
    }
    private void Update()
    {
        Move();
        timer++;
        if (count1 > count2)
        {
            if (timer % 50 == 0)
            {
                TempFire2();
                timer = 1;
            }
        }

        if (count2 > count1)
        {
            if (timer % 50 == 0)
            {
                TempFire();
                timer = 1;
            }
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
            {
                Transform rootT = other.gameObject.transform.root;
                GameObject go = rootT.gameObject;

                //print("Triggered:" +go.name);

                if (go == lastTriggerGo)
                {
                    return;
                }

                lastTriggerGo = go;

                if (go.tag == "ProjectileHero2")
                {
                    count2++;
                    print(count2);
                    Destroy(go);
                }

                if (go.tag == "ProjectileHero")
                {
                    count1++;
                    print(count1);
                    Destroy(go);
                }
                
                if (count1 > count2)
                {
                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = new Color(1f, 0, 0f); 
                }

                if (count1 < count2)
                {

                    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
                    renderer.color = new Color(0f, 01, 0f); 
                }
                

                else
                {
                    print("Triggered by non-Enemy" + go.name);
                }
            }
    
    void TempFire()
    {
        
            GameObject projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = transform.position;
            Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
            rigidB.velocity = Vector3.left * projectileSpeed;
           
            return;
        
    }
    
    void TempFire2()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = transform.position;
            Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
            rigidB.velocity = Vector3.right * projectileSpeed;
            return;
        
    }
    
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
            
        }

        set { this.transform.position = value; }
    }
    
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y += Mathf.Sin (Time.fixedTime * Mathf.PI * frequency) * amplitude;
        pos = tempPos;
    }
    
}