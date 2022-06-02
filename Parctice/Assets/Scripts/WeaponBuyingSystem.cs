using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBuyingSystem : MonoBehaviour
{
    GameObject player;
    WeaponManager playerInventory;
    public int weaponPrice;
    public int ammoPrice;
    public int ammoPerBuy;
    public string weaponType;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerPrefab");
        playerInventory = player.transform.GetChild(1).transform.GetChild(0).GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.GetChild(0).position, transform.position) < 2f)
        {
            if (Input.GetButtonDown("Interact"))
            {
                BuyItem();
            } 
        }
    }

    void BuyItem()
    {
        if (!playerInventory.GetWeaponActive(weaponType))
        {
            if (player.transform.GetChild(0).gameObject.GetComponent<PlayerMovement>().money >= weaponPrice)
            {
                player.transform.GetChild(0).gameObject.GetComponent<PlayerMovement>().money -= weaponPrice;
                playerInventory.SetWeaponActive(weaponType);
            }
        } else 
        {
            if (player.transform.GetChild(0).gameObject.GetComponent<PlayerMovement>().money >= ammoPrice)
            {
                player.transform.GetChild(0).gameObject.GetComponent<PlayerMovement>().money -= ammoPrice;
                playerInventory.ammo[weaponType] += ammoPerBuy;
            }
        }
    }
}
