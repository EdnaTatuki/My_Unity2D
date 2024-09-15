using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float moveSpeed;
    public LayerMask SolidLayer;
    private bool isMoving;

    private Vector2 input;


    private Animator animator;



    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         if(!isMoving) {
            
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");
            
            //不斜向移动，斜向移动加单独按键
            if(input.x != 0) input.y = 0;

            //按格子移动
            if (input.x > 0 && input.x < 1) input.x += 1;
            if (input.x < 0 && input.x > -1) input.x -= 1;
            if (input.y > 0 && input.y < 1) input.y += 1;
            if (input.y < 0 && input.y > -1) input.y -= 1;

            if (input != Vector2.zero){

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                var targetPos = transform.position;
                targetPos.x += (int)input.x;
                targetPos.y += (int)input.y;

                if(IsWalkbale(targetPos))
                 StartCoroutine(Move(targetPos));
            }
        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;
    }


    private bool IsWalkbale(Vector3 targetpos)
    {
        if (Physics2D.OverlapCircle(targetpos,0.1f,SolidLayer) != null) 
        {
            return false;
        }
        return true;
    }
}
