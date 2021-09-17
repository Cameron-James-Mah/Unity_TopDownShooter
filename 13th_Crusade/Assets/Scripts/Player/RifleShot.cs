using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleShot : MonoBehaviour
{

    public Transform firePoint;
    public Transform firePoint2;
    public Transform firePoint3;
    public GameObject bulletPrefab;

    public float bulletForce = 140f; 

    public LayerMask solidObjectsLayer;

    public AudioSource audioSrc;

    public AudioClip rifleShot;
    public AudioClip shotgunShot;
    public AudioClip handgunShot;
    public AudioClip rifleReload;
    public AudioClip shotgunReload;
    public AudioClip handgunReload;

    private bool shooting = false, reloading = false;

    private int currWeapon;

    private Animator animator;

    private int rifleReserves = 100;
    private int rifleMag = 30, rifleFullMag = 30;

    private int shotgunReserves = 50;
    private int shotgunMag = 15, shotgunFullMag = 15;

    private int handgunMag = 10, handgunFullMag = 10;

    private GameObject text;








    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
        text = GameObject.FindGameObjectWithTag("gunText");
        currWeapon = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.ResetTrigger("handgunSwitch");
            animator.ResetTrigger("shotgunSwitch");
            animator.SetTrigger("rifleSwitch");
            text.GetComponent<gunText>().currWeapon = 1;
            text.GetComponent<gunText>().currMag = rifleMag;
            text.GetComponent<gunText>().currReserves = rifleReserves;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.ResetTrigger("rifleSwitch");
            animator.ResetTrigger("handgunSwitch");
            animator.SetTrigger("shotgunSwitch");
            text.GetComponent<gunText>().currWeapon = 2;
            text.GetComponent<gunText>().currMag = shotgunMag;
            text.GetComponent<gunText>().currReserves = shotgunReserves;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            animator.ResetTrigger("rifleSwitch");
            animator.ResetTrigger("handgunSwitch");
            animator.SetTrigger("handgunSwitch");
            text.GetComponent<gunText>().currWeapon = 3;
            text.GetComponent<gunText>().currMag = handgunMag;
            text.GetComponent<gunText>().currReserves = 999;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currWeapon = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currWeapon = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currWeapon = 3;
        }

        if (Input.GetKeyDown(KeyCode.R) && reloading == false)
        {
            if (currWeapon == 1 && rifleMag < rifleFullMag && rifleReserves > 0)
            {
                if(rifleReserves <= 0 && rifleMag != rifleFullMag)
                {
                    Debug.Log("Out of rifle reserves");
                }
                else
                {
                    StartCoroutine(Reloading());
                }
                
            }
            else if (currWeapon == 2 && shotgunMag < shotgunFullMag && shotgunReserves > 0)
            {
                if (shotgunReserves <= 0 && shotgunMag != shotgunFullMag)
                {
                    Debug.Log("Out of shotgun reserves");
                }
                else
                {
                    StartCoroutine(Reloading());
                }
            }
            else if (currWeapon == 3)
            {
                if(handgunMag == handgunFullMag)
                {
                    //mag is full
                }
                else
                {
                    StartCoroutine(Reloading());
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && shooting == false && reloading == false)
        {
            if (currWeapon == 1)
            {
                StartCoroutine(RifleShooting());
            }
            else if(currWeapon == 2)
            {
                StartCoroutine(ShotgunShooting());
            }
            else if(currWeapon == 3)
            {
                StartCoroutine(HandgunShooting());
            }
        }
        
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
    
   //Make sure mag isnt full before calling this and there are reserves for equipped weapon
    IEnumerator Reloading()
    {
        if(currWeapon == 1)
        {
            audioSrc.PlayOneShot(rifleReload);
            reloading = true;
            animator.SetBool("isReloadingRifle", true);
            yield return new WaitForSeconds(1.0f);
            if(rifleReserves >= rifleFullMag-rifleMag)
            {
                rifleReserves -= rifleFullMag-rifleMag;
                rifleMag = rifleFullMag;
                text.GetComponent<gunText>().currMag = rifleMag;
                text.GetComponent<gunText>().currReserves = rifleReserves;
            }
            else
            {
                rifleMag += rifleReserves;
                rifleReserves = 0;
                text.GetComponent<gunText>().currMag = rifleMag;
                text.GetComponent<gunText>().currReserves = rifleReserves;
            }
            animator.SetBool("isReloadingRifle", false);
            reloading = false;
            //Update ammo text
        }
        else if (currWeapon == 2)
        {
            audioSrc.PlayOneShot(shotgunReload, 4.0f);
            reloading = true;
            animator.SetBool("isReloadingShotgun", true);
            yield return new WaitForSeconds(1.0f);
            if (shotgunReserves >= shotgunFullMag - shotgunMag)
            {
                shotgunReserves -= shotgunFullMag-shotgunMag;
                shotgunMag = shotgunFullMag;
                text.GetComponent<gunText>().currMag = shotgunMag;
                text.GetComponent<gunText>().currReserves = shotgunReserves;
            }
            else
            {
                shotgunMag += shotgunReserves;
                shotgunReserves = 0;
                text.GetComponent<gunText>().currMag = shotgunMag;
                text.GetComponent<gunText>().currReserves = shotgunReserves;
            }
            animator.SetBool("isReloadingShotgun", false);
            reloading = false;
            //Update ammo text
        }
        else if (currWeapon == 3)
        {
            audioSrc.PlayOneShot(handgunReload, 6.0f);
            reloading = true;
            animator.SetBool("isReloadingHandgun", true);
            yield return new WaitForSeconds(1.0f);
            handgunMag = handgunFullMag;
            animator.SetBool("isReloadingHandgun", false);
            text.GetComponent<gunText>().currMag = handgunMag;
            reloading = false;
        }

    }

    IEnumerator RifleShooting()
    {
        while (Input.GetMouseButton(0) && currWeapon == 1 && rifleMag > 0)
        {
            animator.SetBool("isShootingRifle", true);
            shooting = true;
            audioSrc.PlayOneShot(rifleShot);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            rifleMag -= 1;
            text.GetComponent<gunText>().currMag = rifleMag;
            //Update ammo text
            yield return new WaitForSeconds(0.1f);

        }
        animator.SetBool("isShootingRifle", false);
        shooting = false;
    }

    IEnumerator HandgunShooting()
    {
            if (Input.GetMouseButton(0) && handgunMag > 0)
            {
                animator.SetBool("isShootingHandgun", true);
                shooting = true;
                audioSrc.PlayOneShot(handgunShot, 2.0f);
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
                handgunMag -= 1;
                text.GetComponent<gunText>().currMag = handgunMag;
                yield return new WaitForSeconds(0.2f);
            }
        animator.SetBool("isShootingHandgun", false);
        shooting = false;
    }

    IEnumerator ShotgunShooting()
    {
        if (Input.GetMouseButton(0) && shotgunMag > 0)
        {
            animator.SetBool("isShootingShotgun", true);
            shooting = true;
            audioSrc.PlayOneShot(shotgunShot, 2.0f);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            GameObject bullet2 = Instantiate(bulletPrefab, firePoint2.position, firePoint2.rotation);
            GameObject bullet3 = Instantiate(bulletPrefab, firePoint3.position, firePoint3.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
            Rigidbody2D rb3 = bullet3.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
            rb2.AddForce(firePoint2.up * bulletForce, ForceMode2D.Impulse);
            rb3.AddForce(firePoint3.up * bulletForce, ForceMode2D.Impulse);
            shotgunMag -= 1;
            text.GetComponent<gunText>().currMag = shotgunMag;
            yield return new WaitForSeconds(0.2f);
        }
        animator.SetBool("isShootingShotgun", false);
        shooting = false;
    }

   



}
