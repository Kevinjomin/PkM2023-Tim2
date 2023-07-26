using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanObject : MonoBehaviour
{
    [SerializeField] public GameObject HighlightVisual;
    [SerializeField] public int typeIndex;
    [SerializeField] public bool isFilled = false;
    [SerializeField] public bool isCorrect = false;
    private GameObject openMesh;
    private GameObject closedMesh;
    /*
    Cardboard Box type index :
    - organic = 1
    - non organic = 2
    - B3 = 3
    */
    private void Start()
    {
        openMesh = transform.Find("Visuals/Open").gameObject;
        closedMesh = transform.Find("Visuals/Closed").gameObject;
    }

    private void Update()
    {
        if (isFilled && openMesh.activeSelf)
        {
            openMesh.SetActive(false);
            closedMesh.SetActive(true);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Truck"))
        {
            Destroy(gameObject);
        }
    }
}
