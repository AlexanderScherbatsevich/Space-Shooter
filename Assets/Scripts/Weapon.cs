using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ??? ???????????? ???? ????????? ????? ??????.
/// ????? ???????? ??? "shield", ????? ???? ??????????? ???????????????? ??????.
/// </summary>

public enum WeaponType
{
    none,
    blaster,
    spread,
    phaser,       // ?? ???????????
    missile,      // ?? ???????????
    laser,        // ?? ???????????
    shield
}

//????? WeaponDefinition ????????? ??????????? ???????? ??????????? ???? ?????? ? ??????????
//??? ????? ????? Main ????? ??????? ?????? ????????? ???? WeaponDefinition.
[System.Serializable]
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter;                       //????? ?? ?????? 
    public Color color = Color.white;           //???? ?????? ?????? ? ?????? ??????
    public GameObject projectilePrefab;
    public Color projectileColor = Color.white;
    public float damageOnHit = 0;
    public float continuousDamage = 0;
    public float delayBetweenShots = 0;
    public float velocity = 20;                 //??????? ?????? ????????
    public AudioSource shootSound;
}
public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;

    [Header("Set Dynamically")]
    [SerializeField]
    private WeaponType _type = WeaponType.none;
    public WeaponDefinition def;
    public GameObject collar;
    public float lastShotTime;
    private Renderer collarRend;
    private AudioSource _shootSound;

    private void Start()
    {
        collar = transform.Find("Collar").gameObject;
        collarRend = collar.GetComponent<Renderer>();

        //???????? ??? ?????? ?? ?????????
        SetType(_type);
        if (PROJECTILE_ANCHOR == null)
        {
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }
        //????? fireDelegate ? ???????? ???????
        GameObject rootGO = transform.root.gameObject;
        if (rootGO.GetComponent<Hero>() != null)
        {
            rootGO.GetComponent<Hero>().fireDelegate += Fire;
        }
    }

    public WeaponType type
    {
        get { return (_type); }
        set { SetType(value); }
    }

    public void SetType(WeaponType wt)
    {
        _type = wt;
        if (type == WeaponType.none)
        {
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        def = GameManager.GetWeaponDefinition(_type);
        collarRend.material.color = def.color;
        _shootSound = def.shootSound;
        lastShotTime = 0;
        //GameObject.DontDestroyOnLoad(this);      
    }

    public void Fire()
    {
        //???? this.gameObject ?? ??????? - ?????
        if (!gameObject.activeInHierarchy) return;
        //???? ????? ?????????? ?????? ???????????? ????? ??????? - ?????
        if (Time.time - lastShotTime < def.delayBetweenShots) return;

        Projectile p;
        Vector3 vel = Vector3.up * def.velocity;
        if (transform.up.y < 0)    //??? Enemy
        {
            vel.y = -vel.y;   
        }
        switch (type)
        {
            case WeaponType.blaster:
                p = MakeProjectile();
                _shootSound.Play();
                p.rigid.velocity = vel;
                break;

            case WeaponType.spread:
                p = MakeProjectile();      //??????. ??????? ?????
                _shootSound.Play();
                p.rigid.velocity = vel;
                p = MakeProjectile();      //??????. ??????? ??????
                p.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                p = MakeProjectile();      //??????. ??????? ?????
                p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                break;

            //case WeaponType.laser:
            //    p = MakeProjectile();
            //    p.transform.position = collar.transform.position;
            //    float laserLifeTime = lastShotTime + 2.5f;
            //    if (Input.GetAxis("Jump") == 0 && Time.time >= laserLifeTime)
            //    {
            //        Destroy(p);
            //    }
            //    break;
        }
    }

    public Projectile MakeProjectile()
    {
        GameObject go = Instantiate<GameObject>(def.projectilePrefab);
        if (transform.parent.gameObject.tag == "Hero")
        {
            go.tag = "ProjectileHero";
            go.layer = LayerMask.NameToLayer("ProjectileHero");
        }
        else
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }
        go.transform.position = collar.transform.position;
        go.transform.SetParent(PROJECTILE_ANCHOR, true);
        Projectile p = go.GetComponent<Projectile>();
        p.type = type;
        lastShotTime = Time.time;
        return (p);
    }
}
