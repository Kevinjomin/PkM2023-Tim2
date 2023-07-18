using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Selector")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                other.gameObject.GetComponent<Level3DDisplayer>().SwitchScene();
            }
        }
    }
}
