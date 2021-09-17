using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;


    private Vector3 mouse_pos;
    public Transform target;
    private Vector3 object_pos;
    private float angle;

    private Animator animator;

    public LayerMask solidObjectsLayer;

    private int currWeapon;
    

    

    



   
    

    private void Start()
    {
        currWeapon = 1;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {


        mouse_pos = Input.mousePosition;
        mouse_pos.z = -20;
        object_pos = Camera.main.WorldToScreenPoint(target.position);
        mouse_pos.x = mouse_pos.x - object_pos.x;
        mouse_pos.y = mouse_pos.y - object_pos.y;
        angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);



        /*
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Pressed primary button.");
            animator.SetBool("isShootingRifle", true);
            animator.SetBool("isShootingHandgun", true);
            animator.SetBool("isShootingShotgun", true);

        }
        if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("isShootingRifle", false);
            animator.SetBool("isShootingHandgun", false);
            animator.SetBool("isShootingShotgun", false);
        }*/

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




        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Pressed secondary button.");
        }
            
        if (!isMoving) //I think because it may cause issue with IEnumerator 
        {
                
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input != Vector2.zero)
            {
                //Debug.Log("Moving");
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;
                if (IsWalkable(targetPos))
                {
                    StartCoroutine(Move(targetPos));
                }
            }
        }
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            if(currWeapon == 1)
            {
                animator.SetBool("isMovingRifle", true);
            }
            else if(currWeapon == 2)
            {
                animator.SetBool("isMovingShotgun", true);
            }
            else if (currWeapon == 3)
            {
                animator.SetBool("isMovingHandgun", true);
            }
            
            

            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        animator.SetBool("isMovingRifle", false);
        animator.SetBool("isMovingHandgun", false);
        animator.SetBool("isMovingShotgun", false);
        isMoving = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if(Physics2D.OverlapCircle(targetPos, 0.3f, solidObjectsLayer) != null)
        {
            return false;
        }
        return true;
    }
}
