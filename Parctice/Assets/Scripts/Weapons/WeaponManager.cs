using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{

    public Dictionary<string, int> ammo = new Dictionary<string, int>();
    public bool hasPistol, hasRifle, hasShotgun, hasRocketLauncher, hasSniperRifle;
    public GameObject[] weapons = new GameObject[5];

    //
    public GameObject magzAmmo, maxAmmo, invAmmo;
    //
    
    private int currentWeapon = 0;

    void Start()
    {
        weapons[currentWeapon].SetActive(true);
        ammo.Add("Rifle", 50);
        ammo.Add("Shotgun", 10);
        ammo.Add("Rocket", 100);
        ammo.Add("Sniper", 10);
    }

    void Update()
    {
        if (Input.GetButtonDown("FirstWeapon") && hasPistol) ChooseWeapon(0);
        if (Input.GetButtonDown("SecondWeapon") && hasRifle) ChooseWeapon(1);
        if (Input.GetButtonDown("ThirdWeapon") && hasShotgun) ChooseWeapon(2);
        if (Input.GetButtonDown("FourthWeapon") && hasRocketLauncher) ChooseWeapon(3);
        if (Input.GetButtonDown("FifthWeapon") && hasSniperRifle) ChooseWeapon(4);
        //
        AmmoValue(weapons[currentWeapon]);
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
        }
        else if (weapons[currentWeapon].GetComponent<RocketLauncherController>())
        {
            RocketLauncherController curWeap = weapons[currentWeapon].GetComponent<RocketLauncherController>();
            if (!curWeap.GetReloadingState() && curWeap.GetShootingState()) 
            {
                SwitchWeapon(number);
            }
        }
        else 
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

    public bool GetWeaponActive(string weapon)
    {
        switch (weapon)
        {
            case "Rifle":
                return hasRifle;
            case "Shotgun":
                return hasShotgun;
            case "Rocket":
                return hasRocketLauncher;
            case "Sniper":
                return hasSniperRifle;
            default: return false;
        }
    }

    //
    void AmmoValue(GameObject weapon)
    {
        if (currentWeapon != 3)
        {
            WeaponController curWeap = weapon.GetComponent<WeaponController>();
            magzAmmo.GetComponent<TextMeshProUGUI>().text = "" + curWeap.ammo;
            maxAmmo.GetComponent<TextMeshProUGUI>().text = "" + curWeap.magAmmo;
            if (!curWeap.infiniteAmmo)
            {
                invAmmo.GetComponent<TextMeshProUGUI>().text = "" + curWeap.ammoType + " " + curWeap.ammoInv.ammo[curWeap.ammoType];
            }
            else
            {
                invAmmo.GetComponent<TextMeshProUGUI>().text = "inf";
            }
        }
        else if (currentWeapon == 3)
        {
            RocketLauncherController curWeap = weapon.GetComponent<RocketLauncherController>();
            magzAmmo.GetComponent<TextMeshProUGUI>().text = "";
            maxAmmo.GetComponent<TextMeshProUGUI>().text = "";
            invAmmo.GetComponent<TextMeshProUGUI>().text = "" + ammo["Rocket"];
        }

    }
    //

    public void SetWeaponActive(string weapon)
    {
        switch (weapon)
        {
            case "Rifle":
                hasRifle = true;
                break;
            case "Shotgun":
                hasShotgun = true;
                break;
            case "Rocket":
                hasRocketLauncher = true;
                break;
            case "Sniper":
                hasSniperRifle = true;
                break;
        }
    }
}
