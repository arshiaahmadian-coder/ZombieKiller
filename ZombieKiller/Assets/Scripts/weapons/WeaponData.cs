using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float shootInterval;
    public float reloadTime;
    public int magCapacity;
    public float damage;
    public GameObject weaponPrefab;
    public int range;

    public int price;
}
