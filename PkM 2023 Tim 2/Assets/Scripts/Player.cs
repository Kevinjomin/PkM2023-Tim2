using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float pickupRange = 2f;
    [SerializeField] private GameObject holdItemContainer;
    [SerializeField] private AudioSource walkSound, insertingTrashSound, pickOrganicSound, pickInorganicSound, pickB3Sound;
    [SerializeField] private GameStateManager gameManager;

    private ScoreSystem scoreSystem;
    private PickableObject heldObject;
    private bool isHolding = false;

    PickableObject previousPickable;
    TrashCanObject previousTrashCan;

    private void Update()
    {
        if(Time.timeScale == 0f)
        {
            walkSound.Stop();
        }

        if(gameManager != null && PauseMenuUI.isPaused == false && gameManager.gameState == GameStateManager.GameState.INGAME)
        {
            inputMovement();
            if (isHolding == false)
            {
                pickupObject();
            }
            else
            {
                throwObject();
            }
        }
    }

    private void inputMovement()
    {
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
            if((moveDirection.x != 0 || moveDirection.z != 0))
            {
                if(walkSound.isPlaying == false)
                {
                    walkSound.Play();
                }
            }
            else
            {
                walkSound.Stop();
            }
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
            if(previousPickable != null && previousPickable != nearestPickable)
            {
                previousPickable.HighlightVisual.SetActive(false); //unhighlight previous
            }
            //highlight nearest object
            nearestPickable.HighlightVisual.SetActive(true);
            previousPickable = nearestPickable; //update previous pickable

            if (Input.GetKeyDown(KeyCode.F))
            {
                isHolding = true;
                nearestPickable.PickUp(holdItemContainer.transform);
                nearestPickable.HighlightVisual.SetActive(false);
                heldObject = nearestPickable;
                if (nearestPickable.typeIndex == 1)
                {
                    pickOrganicSound.Play();
                }
                else if(nearestPickable.typeIndex == 2)
                {
                    pickInorganicSound.Play();
                }
                else
                {
                    pickB3Sound.Play();
                }
            }
        }
    }

    private void throwObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, pickupRange);
        TrashCanObject nearestTrashCan = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {

            TrashCanObject trashCan = collider.GetComponent<TrashCanObject>();
            // if there is a pickable object
            if (trashCan != null)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                // only select the nearest object if there are multiple pickable object
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    if(trashCan.isFilled == false) //only select if trash can is empty
                    {
                        nearestTrashCan = trashCan;
                    }
                }
            }
        }

        if (nearestTrashCan != null)
        {
            if (previousTrashCan != null && previousTrashCan != nearestTrashCan)
            {
                previousTrashCan.HighlightVisual.SetActive(false); // Disable the previous trash can's highlight
            }
            //highlight nearest trash can
            nearestTrashCan.HighlightVisual.SetActive(true);
            previousTrashCan = nearestTrashCan; //update previous trash can

            if (Input.GetKeyDown(KeyCode.F))
            {
                nearestTrashCan.isFilled = true;
                nearestTrashCan.HighlightVisual.SetActive(false);
                isHolding = false;
                insertingTrashSound.Play();
                if(heldObject.typeIndex == nearestTrashCan.typeIndex)
                {
                    nearestTrashCan.isCorrect = true;
                    heldObject.throwAwayCorrect();
                }
                else
                {
                    nearestTrashCan.isCorrect = false;
                    heldObject.throwAwayIncorrect();
                }
                
            }
        }
    }

    private void OnDrawGizmos() //to see pickup range
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
