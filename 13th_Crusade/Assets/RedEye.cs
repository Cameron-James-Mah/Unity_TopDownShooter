using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEye : MonoBehaviour
{
    private bool spawned = false;
    public GameObject player;
    private Animator animator;
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 movement;
    public int health;
    private bool hit = false;
    private GameObject text;
    public float overShoot;
    
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        text = GameObject.FindGameObjectWithTag("Text");
        health = 200;
        //gameObject.SetActive(false);
        


    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad >= 114.0f & spawned == false)
        {
            spawned = true;
            //gameObject.SetActive(true);
            transform.position = new Vector3(-12, 0, 0);
            animator.SetTrigger("isSpawning");
            //animator.ResetTrigger("isSpawning");
            
            //Move to position and do spawn animation
            StartCoroutine(active());
        }
    }

    

    
    
    IEnumerator active()
    {
        while (gameObject != null && player != null)
        {
            hit = false;
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            movement = direction;
            float distance = Vector2.Distance(player.transform.position, transform.position);
            Vector3 tempPlayerPos = player.transform.position;
            Vector3 overShootPos = tempPlayerPos + (tempPlayerPos - transform.position) * overShoot;
            yield return new WaitForSeconds(0.2f);

            animator.SetTrigger("isLunging");
            //Debug.Log("Start Lunge");
            while (Vector2.Distance(overShootPos, transform.position) >= 0.1f)
            {
                //Debug.Log("Lunging");
                transform.position = Vector3.MoveTowards(transform.position, overShootPos, moveSpeed * Time.deltaTime);
                yield return null;

                float distance2 = Vector2.Distance(player.transform.position, transform.position);
                if (distance2 <= 3.0f & hit == false)
                {
                    hit = true;
                    text.GetComponent<textScript>().playerHp -= 20;
                    player.GetComponent<ShadeEnemy>().health -= 20;
                }
            }
            animator.ResetTrigger("isLunging");
            animator.SetTrigger("isResetting");
            yield return new WaitForSeconds(0.8f);//Match animation time to delay time



        }
        //animator.ResetTrigger("isLunging");
        //animator.SetTrigger("isResetting");
        
        /*
        while(gameObject != null && player != null)
        {
            Vector3 direction = player.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            movement = direction;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            float distance = Vector2.Distance(player.transform.position, transform.position);
            //Change this
            if (distance <= 3.0f & hit == false)
            {              
                text.GetComponent<textScript>().playerHp -= 20;
                player.GetComponent<ShadeEnemy>().health -= 20;
                if (health <= 0)
                {
                    health = 0;
                    Destroy(gameObject);

                }
                else
                {
                    hit = true;
                    animator.SetBool("isAttacking", true);
                    yield return new WaitForSeconds(0.5f);
                    if (gameObject != null)
                    {
                        animator.SetBool("isAttacking", false);
                    }
                    hit = false;
                }
            }
            yield return null;
        }*/


    }

    void OnTriggerEnter2D(Collider2D other)
    {
        health -= 10;
        Debug.Log(health);

        if(health <= 0)
        {
            Destroy(gameObject);
            StateNameController.result = "Victory";
            StateNameController.time = Time.timeSinceLevelLoad;
        }


    }
}
