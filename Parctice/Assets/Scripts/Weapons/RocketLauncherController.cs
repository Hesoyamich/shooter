using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncherController : MonoBehaviour
{
    public GameObject rocket;
    public GameObject projectile;
    public GameObject shootPos;

    bool canShoot;
    bool reloading;
    Vector3 startPos;
    public WeaponManager ammoInv;


    void Start()
    {
        canShoot = true;
        startPos = transform.localPosition;
        ammoInv = transform.parent.gameObject.GetComponent<WeaponManager>();
    }

    void Update()
    {
        WeaponMoving();
        Shoot();
        ammoInv = transform.parent.gameObject.GetComponent<WeaponManager>();
    }

    void Shoot()
    {
        if (ammoInv.ammo["Rocket"] != 0 && Input.GetMouseButton(0) && canShoot)
        {
            rocket.SetActive(false);
            ammoInv.ammo["Rocket"] -= 1;
            Instantiate(projectile, shootPos.transform.position, shootPos.transform.rotation);
            canShoot = false;
            if (ammoInv.ammo["Rocket"] != 0)
            {
                StartCoroutine(Reload());
            }

        }
    }

    void WeaponMoving() {
        Vector2 mouseAxis = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * 10);
        transform.localPosition += (Vector3)mouseAxis * -5 / 1000;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(0.2f);
        reloading = true;
        startPos += new Vector3(0,-1,0);
        yield return new WaitForSeconds(1f);
        startPos += new Vector3(0,1,0);
        rocket.SetActive(true);
        canShoot = true;
        reloading = false;
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
