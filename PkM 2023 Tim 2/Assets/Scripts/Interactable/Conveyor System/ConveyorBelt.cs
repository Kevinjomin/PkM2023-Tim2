using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    [SerializeField] private List<GameObject> onBelt;

    private void FixedUpdate()
    {
        for(int i=0; i<onBelt.Count; i++)
        {
            if (!onBelt[i])
            {
                onBelt.RemoveAt(i);
                continue;
            }
            //add velocity to gameObject on the belt
            onBelt[i].GetComponent<Rigidbody>().velocity = speed * (transform.forward * 100) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    { //when something is on belt
        onBelt.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        onBelt.Remove(collision.gameObject);
    }
}
