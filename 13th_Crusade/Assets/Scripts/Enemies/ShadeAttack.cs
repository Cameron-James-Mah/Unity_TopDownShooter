using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeAttack : MonoBehaviour
{
    public GameObject playerPrefab;
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        //GameObject myPlayer = playerPrefab;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*float distance = Vector2.Distance(transform.position, myPlayer.transform.position);
        Debug.Log(distance);
        if(distance <= 2.0f)
        {
            animator.SetBool("isAttacking", true);
        }
        if(distance >= 2.0f)
        {
            animator.SetBool("isAttacking", false);
        }*/
    }
}
