using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noslot : MonoBehaviour , IWeapon
{
    [SerializeField] private WeaponInfo weaponInfo;

    public void Attack(){
        
    }

        public WeaponInfo GetWeaponInfo(){
        return weaponInfo;
    }
}
