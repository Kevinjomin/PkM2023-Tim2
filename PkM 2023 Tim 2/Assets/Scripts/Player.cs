using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float pickupRange = 2f;
    [SerializeField] private GameObject holdItemContainer;

    private void Update()
    {
        pickupObject();
     /*   if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("test");
            pickupObject();
        }
*/
        Vector2 inputVector = new Vector2(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += +1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x += -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y += -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += +1;
        }
        inputVector = inputVector.normalized;
        Vector3 moveDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        // change playerSize based on the scale of player visual mesh
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 1f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);
        
        if (!canMove) // when player cannot move
        {
            // attempt movement on x axis
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);
            if (canMove)
            {
                moveDirection = moveDirectionX; //move only on x axis
            }
            else
            {
                // attempt movement on z axis
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);
                if (canMove)
                {
                    moveDirection = moveDirectionZ; //move only on z axis
                }
                else
                {
                    //cannot move in all direction
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        // rotate animation
        float rotateSpeed = 8f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
    }

    private void pickupObject()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange);
        PickableObject nearestPickable = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            
            PickableObject pickable = collider.GetComponent<PickableObject>();
            // if there is a pickable object
            if (pickable != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                // only select the nearest object if there are multiple pickable object
                if(distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestPickable = pickable;
                }
            }
        }
        if(nearestPickable != null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                nearestPickable.PickUp(holdItemContainer.transform);
            }
        }
    }

    private void OnDrawGizmos() //to see pickup range
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
