using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild2 : MonoBehaviour
{
    [Header("Set in Inspector")] 
    public float rotationsPerSecond = 0.1f;
    [Header("Set Dynamically")] 
    public int levelShown = 0;
    private Material mat2;
    void Start()
    {
        mat2 = GetComponent<Renderer>().material;
    }

    void Update()
    {
        int currLevel = Mathf.FloorToInt(hero2.A.shieldLevel);

        if (levelShown != currLevel)
        {
            levelShown = currLevel;
            mat2.mainTextureOffset = new
                Vector2(0.2f * levelShown, 0);
        }

        float rZ = -(rotationsPerSecond * Time.deltaTime * 360) % 360f;
        transform.rotation = Quaternion.Euler(0,0,rZ);
    }

}
