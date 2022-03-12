using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    //[Header("Set in Inspector")]
    //public float rotationsPerSecond = 0.1f;

    //[Header("Set Dynamically")]
    //public int levelShown = 0;

    Material mat;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        ////прочитать текущую мощность защитного поля из объекта-одиночки Hero
        //int currLevel = Mathf.FloorToInt(Hero.S.shieldLevel);
        //if (levelShown != currLevel)
        //{
        //    levelShown = currLevel;
        //    //скорректировать смещение в текстуре, для отображения поля другой мощности
        //    mat.mainTextureOffset = new Vector2(0.2f * levelShown, 0);
        //}
        //float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        //transform.rotation = Quaternion.Euler(0, 0, rZ);
        int currLevel = Mathf.FloorToInt(Hero.S.shieldLevel);       
        switch (currLevel)
        {
            case 0:
                this.gameObject.SetActive(false);
                //mat.color = Color.clear;
                break;
            case 1:
                mat.color = Color.red;
                break;
            case 2:
                mat.color = Color.yellow;
                break;
            case 3:
                mat.color = Color.green;
                break;
            case 4:
                mat.color = Color.blue;
                break;
            default:
                break;
        }
    }
}
