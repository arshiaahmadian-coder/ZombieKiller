using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] WeaponData weaponData;
    [SerializeField] GameObject playerCam;

    private float timer;
    private bool canShoot;
    private Animator animator;

    private void Start()
    {
        InstantiateWeapon();
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
        if(Input.GetMouseButton(0) && canShoot) ShootBulet();
        // reset timer if released auto shoot
    }

    private void ShootBulet()
    {
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
                zombieHealth.TakeDamage(weaponData.damage);
            } else
            {
                ZombieHeadshot headshot = hit.transform.GetComponent<ZombieHeadshot>();
                if(headshot != null)
                {
                    headshot.TakeHeadshotDamage(weaponData.damage);
                }
            }
        }
    }

    private void InstantiateWeapon()
    {
        GameObject weaponObj = Instantiate(weaponData.weaponPrefab);
        weaponObj.transform.SetParent(playerCam.transform);
        animator = weaponObj.GetComponent<Animator>();
    }
}
