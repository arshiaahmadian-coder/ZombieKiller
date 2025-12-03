using TMPro;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private WeaponData weaponData;
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject hitVfx;
    [SerializeField] GameObject bloodImpactVfx;
    [SerializeField] GameObject bloodHeadShotVfx;
    [SerializeField] GameObject bloodFallVfx;
    // [SerializeField] ParticleSystem shootVfx;
    [SerializeField] TMP_Text magInfoText;

    private float timer;
    private bool canShoot;
    private bool isReloading;
    private Animator animator;
    private GameObject prevGunObject;
    private int magCapacity;
    private int currentMagCapacity;

    private void Start()
    {
        UpgradeGun();
        timer = weaponData.shootInterval;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            canShoot = true;
            timer = weaponData.shootInterval;
        }

        // check can shoot
        if(Input.GetMouseButton(0) && canShoot && !isReloading) ShootBulet();
        // reset timer if released auto shoot
    }

    private void Reload()
    {
        isReloading = false;
        currentMagCapacity = magCapacity;
        magInfoText.text = currentMagCapacity.ToString();
    }

    private void ShootBulet()
    {
        currentMagCapacity--;
        magInfoText.text = currentMagCapacity.ToString();
        if(currentMagCapacity <= 0)
        {
            // reload
            isReloading = true;
            magInfoText.text = "reloading...";
            // shootVfx.Play();
            Invoke(nameof(Reload), weaponData.reloadTime);
        }

        canShoot = false;
        animator.SetTrigger("Shoot");
        timer = weaponData.shootInterval;

        Vector3 rayOrigin = playerCam.transform.position;
        Vector3 rayDirection = playerCam.transform.forward;
        float rayRange = weaponData.range;
        // shoot raycast
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, rayRange))
        {
            ZombieHealth zombieHealth = hit.transform.GetComponent<ZombieHealth>();

            if(zombieHealth != null)
            {
                GameObject effect = Instantiate(bloodImpactVfx, hit.point, 
                    Quaternion.LookRotation(hit.normal));
                Destroy(effect, 4f);

                GameObject effectFall = Instantiate(bloodFallVfx, hit.point, 
                    Quaternion.LookRotation(hit.normal));
                effectFall.transform.SetParent(zombieHealth.transform);

                zombieHealth.AddObjectToList(effectFall);

                zombieHealth.TakeDamage(weaponData.damage, hit.point, 
                (hit.point - (playerCam ? playerCam.transform.position : transform.position)).normalized);
            } else
            {
                ZombieHeadshot headshot = hit.transform.GetComponent<ZombieHeadshot>();
                if(headshot != null)
                {
                    headshot.TakeHeadshotDamage(weaponData.damage, hit.point, 
                (hit.point - (playerCam ? playerCam.transform.position : transform.position)).normalized);

                    GameObject effect = Instantiate(bloodHeadShotVfx, hit.point, 
                        Quaternion.LookRotation(hit.normal));
                    Destroy(effect, 4f);
                } else
                {
                    GameObject effect = Instantiate(hitVfx, hit.point, 
                        Quaternion.LookRotation(hit.normal));
                    Destroy(effect, 2f);
                }
            }

            UpgradeSystem upgrade = hit.transform.GetComponent<UpgradeSystem>();
            if(upgrade != null)
            {
                upgrade.UpgradeCurrentGun();
            }
        }
    }

    private void InstantiateWeapon()
    {
        if(prevGunObject)
        {
            Destroy(prevGunObject);
        }
        GameObject weaponObj = Instantiate(weaponData.weaponPrefab);
        weaponObj.transform.SetParent(playerCam.transform);
        animator = weaponObj.GetComponent<Animator>();
        prevGunObject = weaponObj;
    }

    public void UpgradeGun()
    {
        weaponData = FindFirstObjectByType<UpgradeSystem>().currentGun;
        magCapacity = weaponData.magCapacity;
        currentMagCapacity = magCapacity;
        magInfoText.text = currentMagCapacity.ToString();
        InstantiateWeapon();
    }
}
