using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanObject : MonoBehaviour
{
    [SerializeField] public GameObject HighlightVisual;
    [SerializeField] public int typeIndex;
    /*
    Trash can type index :
    - organic = 1
    - non organic = 2
    - B3 = 3
    */

    private void Update()
    {
        // when highlight is active
        if (HighlightVisual != null && HighlightVisual.activeSelf)
        {
            HighlightVisual.SetActive(false);
        }

    }
}
