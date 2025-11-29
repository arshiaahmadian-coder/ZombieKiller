using TMPro;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] Transform gunPos;
    [SerializeField] WeaponData gun2;
    [SerializeField] WeaponData gun3;
    [SerializeField] Shoot shoot;
    [SerializeField] TMP_Text gunInfoText;
    public WeaponData currentGun;

    private void ShowNextLevelGun()
    {
        if(currentGun.name == "Pistol (1)")
        {
            GameObject gun = Instantiate(gun2.weaponPrefab, gunPos.position, gunPos.rotation);
            Destroy(gun.GetComponent<Animator>());
            gunInfoText.text = gun2.name + " " + gun2.price + "$";
        }
        else if(currentGun.name == "Pistol (2)")
        {
            GameObject gun = Instantiate(gun3.weaponPrefab, gunPos.position, gunPos.rotation);
            Destroy(gun.GetComponent<Animator>());
            gunInfoText.text = gun3.name + " " + gun3.price + "$";
        }
    }

    public void UpgradeCurrentGun()
    {
        if(currentGun.name == "Pistol (1)")
        {
            if(!GameManager.instance.CanBuy(gun2.price)) return;
            GameManager.instance.SpendCoin(gun2.price);
            currentGun = gun2;
            ShowNextLevelGun();
        }
        else if(currentGun.name == "Pistol (2)")
        {
            if(!GameManager.instance.CanBuy(gun3.price)) return;
            GameManager.instance.SpendCoin(gun3.price);
            currentGun = gun3;
        }

        shoot.UpgradeGun();
    }

    private void Start()
    {
        ShowNextLevelGun();
    }
}
