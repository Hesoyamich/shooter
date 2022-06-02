using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int ammo = 30;
    public int magAmmo;
    public GameObject muzzleFlash;
    public int damage = 40;
    public float reloadTime = 1.5f;
    public bool infiniteAmmo;
    public string ammoType;
    Vector3 startPos;
    private Transform cam;
    public WeaponManager ammoInv;
    private bool reloading;

    public LayerMask layerMask;
    bool canShoot;

    private void Start() {
        canShoot = true;
        cam = transform.parent.transform;
        startPos = transform.localPosition;
        magAmmo = ammo;
        Debug.Log(startPos);
        ammoInv = transform.parent.gameObject.GetComponent<WeaponManager>();
    }

    void Update() {
        WeaponMoving();
        if(Input.GetMouseButton(0) && canShoot && !reloading)
        {
            if(ammo != 0) {
                canShoot = false;
                ammo -= 1;
                StartCoroutine(ShootGun());      
            } else if (ammo == 0 && (infiniteAmmo || ammoInv.ammo[ammoType]  != 0) && !reloading) 
            {
                StartCoroutine(Reload());
            }
            
        }
        if(Input.GetButtonDown("Reload") && ammo != magAmmo && (infiniteAmmo || ammoInv.ammo[ammoType]  != 0)) StartCoroutine(Reload());
    }

    IEnumerator Reload() 
    {
        reloading = true;
        startPos -= new Vector3(0, 1, 0);
        yield return new WaitForSeconds(reloadTime);
        if (infiniteAmmo)
        {
            ammo = magAmmo;
        }
        else if (ammoInv.ammo[ammoType] >= magAmmo - ammo )
        {
            int lackingAmmo = magAmmo - ammo;
            ammo = magAmmo;
            ammoInv.ammo[ammoType] -= lackingAmmo;
        } else if(ammoInv.ammo[ammoType] < magAmmo)
        {
            ammo = ammoInv.ammo[ammoType];
            ammoInv.ammo[ammoType] = 0;
        }
        startPos += new Vector3(0,1,0);
        reloading = false;
    }

    void WeaponMoving() {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * 10);
        transform.localPosition += (Vector3)mouseAxis * -5 / 1000;
    }

    void RecoilAnim()
    {
        transform.localPosition -= Vector3.forward * 0.1f;
    }

    IEnumerator ShootGun()
    {
        StartCoroutine(MuzzleFlash());
        RecoilAnim();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f, ~layerMask))
        {
            Debug.Log(hit.transform.name);

            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null) 
            {
                target.TakeDamage(damage);
            }
        }
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
        
    }

    IEnumerator MuzzleFlash() 
    {
        muzzleFlash.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
        
    }

    public bool GetShootingState()
    {
        return canShoot;
    }
    
    public bool GetReloadingState()
    {
        return reloading;
    }
}
