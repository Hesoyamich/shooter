using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public Dictionary<string, int> ammo = new Dictionary<string, int>();
    public bool hasPistol, hasRifle, hasShotgun, hasRocketLauncher, hasSniperRifle;
    public GameObject[] weapons = new GameObject[5];
    
    private int currentWeapon = 0;

    void Start()
    {
        weapons[currentWeapon].SetActive(true);
        ammo.Add("Rifle", 50);
        ammo.Add("Shotgun", 10);
        ammo.Add("Rocket", 100);
        ammo.Add("Sniper", 0);
    }

    void Update()
    {
        if (Input.GetButtonDown("FirstWeapon") && hasPistol) ChooseWeapon(0);
        if (Input.GetButtonDown("SecondWeapon") && hasRifle) ChooseWeapon(1);
        if (Input.GetButtonDown("ThirdWeapon") && hasShotgun) ChooseWeapon(2);
        if (Input.GetButtonDown("FourthWeapon") && hasRocketLauncher) ChooseWeapon(3);
        if (Input.GetButtonDown("FifthWeapon") && hasSniperRifle) ChooseWeapon(4);
    }

    void ChooseWeapon(int number)
    {
        if (weapons[currentWeapon].GetComponent<WeaponController>())
        {
            WeaponController curWeap = weapons[currentWeapon].GetComponent<WeaponController>();
            if (!curWeap.GetReloadingState() && curWeap.GetShootingState()) 
            {
                SwitchWeapon(number);
            }
        }else if (weapons[currentWeapon].GetComponent<RocketLauncherController>())
        {
            RocketLauncherController curWeap = weapons[currentWeapon].GetComponent<RocketLauncherController>();
            if (!curWeap.GetReloadingState() && curWeap.GetShootingState()) 
            {
                SwitchWeapon(number);
            }
        }else 
        {
            SwitchWeapon(number);
        }
       
    }

    void SwitchWeapon(int number)
    {
        weapons[currentWeapon].SetActive(false);
        weapons[number].SetActive(true);
        currentWeapon = number;
    }
}
