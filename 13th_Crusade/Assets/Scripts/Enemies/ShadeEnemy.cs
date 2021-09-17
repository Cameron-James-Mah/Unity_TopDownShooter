using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShadeEnemy : MonoBehaviour
{
    public GameObject Shade;
    //public Rigidbody2D rb;
    private Vector2 movement;
    public float moveSpeed;
    
    public GameObject Enemy;

    
    public int health;
    public bool hit;

    private Animator ShadeAnimator;

    private AudioSource audioSrc;

    public AudioClip playerHit;

    public AudioClip playerDeath;

    private GameObject text;

    private GameObject mode;

    
    

    




    void Awake()
    {
        
    }
    void Start()
    {
        mode = GameObject.FindGameObjectWithTag("SceneHandle");
        text = GameObject.FindGameObjectWithTag("Text");
        //playerHp = text.GetComponent<textScript>().playerHp;
        audioSrc = GetComponent<AudioSource>();
        //rb = Shade.GetComponent<Rigidbody2D>();
        GameObject Enemy = Shade;
       //ShadeAnimator = Enemy.GetComponent<Animator>();
        //ShadeAnimator.enabled = true;
        StartCoroutine(TempSpawn(Enemy));
        health = 100;
        hit = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        
        /*
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(TempSpawn());
            
            //Debug.Log("Spawned enemy");
            int spawnArea;
            spawnArea = Random.Range(1, 5);
            Debug.Log(spawnArea);
            GameObject Enemy = Shade;
            if(spawnArea == 1)//Left spawn
            {
                Enemy = Instantiate(Shade, new Vector3(Random.Range(-30.0f, -25.0f), Random.Range(-20.0f, 20.0f), 0), Quaternion.identity);
            }
            else if (spawnArea == 2)//Top spawn
            {
                Enemy = Instantiate(Shade, new Vector3(Random.Range(30.0f, -30.0f), Random.Range(25.0f, 20.0f), 0), Quaternion.identity);
            }
            else if (spawnArea == 3)//Right spawn
            {
                Enemy = Instantiate(Shade, new Vector3(Random.Range(30.0f, 25.0f), Random.Range(-20.0f, 20.0f), 0), Quaternion.identity);
            }
            else if (spawnArea == 4)//Bottom spawn
            {
                Enemy = Instantiate(Shade, new Vector3(Random.Range(30.0f, -30.0f), Random.Range(-25.0f, -20.0f), 0), Quaternion.identity);
            }
            
            Rigidbody2D rb = Enemy.GetComponent<Rigidbody2D>();
            
            Vector3 direction = transform.position - Enemy.transform.position;
            //Debug.Log(direction);
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //rb.rotation = angle;
            //Enemy.direction.Normalize();
            //movement = direction;
            StartCoroutine(Move(Enemy, rb));
        }*/
    }


    IEnumerator TempSpawn(GameObject Enemy)
    {
        while (gameObject != null)
        {
            //Debug.Log(StateNameController.difficulty);
            //Debug.Log(Time.time);
            float spawnTime = 1.0f;
            if (StateNameController.difficulty == "Easy")
            {
                spawnTime = 1.0f;
            }
            else if (StateNameController.difficulty == "Normal")
            {
                spawnTime = 0.7f;
            }
            if (StateNameController.difficulty == "Hard")
            {
                spawnTime = 0.5f;
            }

            /*
            else if(Time.timeSinceLevelLoad >= 114.0f)
            {
                spawnTime = 0.2f;
            }*/

            int spawnArea;
            spawnArea = Random.Range(1, 5);
            //Debug.Log(spawnArea);
            
            if (spawnArea == 1)//Left spawn
            {
                Enemy = Instantiate(Shade, new Vector3(Random.Range(-30.0f, -25.0f), Random.Range(-20.0f, 20.0f), 0), Quaternion.identity);
            }
            else if (spawnArea == 2)//Top spawn
            {
                Enemy = Instantiate(Shade, new Vector3(Random.Range(30.0f, -30.0f), Random.Range(25.0f, 20.0f), 0), Quaternion.identity);
            }
            else if (spawnArea == 3)//Right spawn
            {
                Enemy = Instantiate(Shade, new Vector3(Random.Range(30.0f, 25.0f), Random.Range(-20.0f, 20.0f), 0), Quaternion.identity);
            }
            else if (spawnArea == 4)//Bottom spawn
            {
                Enemy = Instantiate(Shade, new Vector3(Random.Range(30.0f, -30.0f), Random.Range(-25.0f, -20.0f), 0), Quaternion.identity);
            }
            Animator ShadeAnimator = Enemy.GetComponent<Animator>();

            Rigidbody2D rb = Enemy.GetComponent<Rigidbody2D>();

            Vector3 direction = transform.position - Enemy.transform.position;

            StartCoroutine(Move(Enemy, rb, ShadeAnimator));

            yield return new WaitForSeconds(spawnTime);
        }
    }

    IEnumerator Move(GameObject Enemy, Rigidbody2D rb, Animator ShadeAnimator)
    {
        while (Enemy != null && gameObject != null)
        {
            
            Vector3 direction = transform.position - Enemy.transform.position;
            //Debug.Log(direction);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            //Enemy.direction.Normalize();
            movement = direction;
            Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, transform.position, moveSpeed * Time.deltaTime);
            //Debug.Log(direction);
            float distance = Vector2.Distance(transform.position, Enemy.transform.position);
            //Debug.Log(distance);
            if (distance <= 3.0f & hit == false)
            {
                health -= 10;
                text.GetComponent<textScript>().playerHp -= 10;
                //Debug.Log(health);
                if (health <= 0)
                {
                    text.GetComponent<textScript>().playerHp = 0;
                    //AudioSource.PlayClipAtPoint(playerDeath, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0));
                    Destroy(gameObject);
                    StateNameController.result = "Defeat";
                    StateNameController.time = Time.timeSinceLevelLoad;

                }
                else
                {
                    hit = true;
                    ShadeAnimator.SetBool("isAttacking", true);
                    audioSrc.PlayOneShot(playerHit);
                    yield return new WaitForSeconds(0.5f);
                    if (Enemy != null)
                    {
                        ShadeAnimator.SetBool("isAttacking", false);
                    }
                    hit = false;
                }
            }
            
            if (health <= 0)
            {
                text.GetComponent<textScript>().playerHp = 0;
                //AudioSource.PlayClipAtPoint(playerDeath, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0));
                Destroy(gameObject);
                StateNameController.result = "Defeat";
                StateNameController.time = Time.timeSinceLevelLoad;
                
            }
            yield return null;
        }
    }

    

    
}
