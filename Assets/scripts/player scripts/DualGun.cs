using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualGun : MonoBehaviour
{
    public Camera camera;

    private cinemachinePOVExtention cinemachinepov;
    private InputManager inputManager;

    public Transform guns;
    public GameObject leftGun;
    public GameObject rightGun;
    public int maxAmmo = 10;
    public float reloadTime = 2f;
    public AudioSource shootingSound;

    private int currentAmmo;
    private bool isRightGunActive = true;
    private bool isReloading;

    private void Start()
    {
        cinemachinepov = cinemachinePOVExtention.instance;
        inputManager = InputManager.instance;

        camera = Camera.main;
        currentAmmo = maxAmmo;
    }

    private void Update()
    {
        guns.rotation = Quaternion.Euler(-cinemachinepov.startingRotation.y, cinemachinepov.startingRotation.x, 0f);

        if (inputManager.playerShoot())
        {
            Shoot();
        }

        if (inputManager.playerReload() && currentAmmo < maxAmmo && !isReloading)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        if (currentAmmo > 0)
        {
            GameObject activeGun = (isRightGunActive) ? rightGun : leftGun;
            GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, activeGun.transform.position, transform.rotation);
            // Customize the bullet's behavior
            Vector3 shootDirection = camera.transform.forward   ;//(enemy.transform.position - activeGun.transform.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection * 100;

            bullet.SetActive(true);

            currentAmmo--;

            if (shootingSound != null)
            {
                shootingSound.Play();
            }

            isRightGunActive = !isRightGunActive;
        }
        //else Reload();
    }

    private void Reload()
    {
        isReloading = true;
        currentAmmo = maxAmmo;
        StartCoroutine(ResetReloading());
    }

    IEnumerator ResetReloading()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
}
