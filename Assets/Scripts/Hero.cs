using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    static public Hero S;

    [Header("Set in Inspector")] 
    public float speed = 30;

    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;

    public GameObject projectilePrefab;
    public float projectileSpeed = 10;
    
    [Header("Set Dynamically")] [SerializeField]
    private float _shieldLevel = 3;
    
    public KeyCode moveUpKey, moveDownKey, moveRightKey, moveLeftKey;
    private int scorer1 = 0;
    private GameObject lastTriggerGo = null;
    AudioSource source;
    void Awake()
    {
        Invoke("Wait", 2);
        source = GetComponent<AudioSource>();
        if (S == null)
        {
            S = this;
        }

        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }
    
    void Update()
    {
        if (Input.GetKey(moveUpKey))
        {
            //move paddle up
            Vector3 currPos = transform.position;
            Vector3 newPos = new Vector3(currPos.x, currPos.y + speed * Time.deltaTime, currPos.z);
            transform.position = newPos;
        }

             
        if (Input.GetKey(moveDownKey))
        {
            //move paddle down
            Vector3 currPos = transform.position;
            Vector3 newPos = new Vector3(currPos.x, currPos.y - speed * Time.deltaTime, currPos.z);
            transform.position = newPos;
        }
        
        if (Input.GetKey(moveRightKey))
        {
            //move paddle down
            Vector3 currPos = transform.position;
            Vector3 newPos = new Vector3(currPos.x + speed * Time.deltaTime, currPos.y, currPos.z);
            transform.position = newPos;
        }
        
        if (Input.GetKey(moveLeftKey))
        {
            //move paddle down
            Vector3 currPos = transform.position;
            Vector3 newPos = new Vector3(currPos.x - speed * Time.deltaTime, currPos.y, currPos.z);
            transform.position = newPos;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        rigidB.velocity = Vector3.right * projectileSpeed;
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
        if (go.tag == "Enemy")
        {
            shieldLevel++;
            
            Destroy(go);
        }
        
        if (go.tag == "Hero2")
        {
            shieldLevel--;
            source.Play();


        }
        
        if (go.tag == "ProjectileHero2")
        {
            shieldLevel--;
            source.Play();
            Destroy(go);
        }
        
        if (go.tag == "ProjectileChild")
        {
            shieldLevel--; 
            source.Play();
            Destroy(go);
        }

        else
        {
            print("Triggered by non-Enemy"+go.name);
        }
        
    }

    public float shieldLevel
    {
        get { return (_shieldLevel); }
        set
        {
            _shieldLevel = Mathf.Min(value, 4);
            if (value < 0)
            {
                this.gameObject.SetActive(false);
                scorer1++;
                scorescript.S.UpdateScore2(scorer1);
                Awake();
            }
        }
    }
    
    void Wait()
    {
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(-50, 0, 0);
        shieldLevel = 2;
    }


}
