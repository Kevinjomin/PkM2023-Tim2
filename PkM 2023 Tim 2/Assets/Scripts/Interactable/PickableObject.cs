using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    [SerializeField] public GameObject HighlightVisual;
    //[SerializeField] public int ScoreValue;
    [SerializeField] public int typeIndex;
    /*
    Trash type index :
    - organic = 1
    - non organic = 2
    - B3 = 3
    */

    private ScoreSystem scoreSystem;
    private Rigidbody rb;

    private void Start()
    {
        scoreSystem = FindObjectOfType<ScoreSystem>();
        rb = GetComponent<Rigidbody>();
    }
    
    public void PickUp(Transform newParent)
    {
        deactivateRigidbody();
        transform.SetParent(newParent);
        transform.position = newParent.position;
        transform.rotation = newParent.rotation;
    }

    public void Drop()
    {
        activateRigidbody();
        transform.SetParent(null);
    }

    public void throwAwayCorrect()
    {
        scoreSystem.AddScore(); //
        Destroy(gameObject);

        this.GetComponent<SpawnObject>().data.UpdateCollected(); //
    }
    public void throwAwayIncorrect()
    {
        scoreSystem.addWrong();
        Destroy(gameObject);
    }

    private void deactivateRigidbody()
    {
        rb.isKinematic = true;
    }
    private void activateRigidbody()
    {
        rb.isKinematic = false;
    }
}
