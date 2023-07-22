using UnityEngine;

public class MoveImageUpDown : MonoBehaviour
{
    private bool isMovedUp = false;
    private RectTransform imageRectTransform;
    private Vector2 originalPosition;

    private void Start()
    {
        imageRectTransform = GetComponent<RectTransform>();
        originalPosition = imageRectTransform.anchoredPosition;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isMovedUp)
            {
                MoveImageDown();
            }
            else
            {
                MoveImageUp();
            }
        }
    }

    private void MoveImageUp()
    {
        Vector2 newPosition = imageRectTransform.anchoredPosition + new Vector2(0f, 267f);
        imageRectTransform.anchoredPosition = newPosition;
        isMovedUp = true;
    }

    private void MoveImageDown()
    {
        imageRectTransform.anchoredPosition = originalPosition;
        isMovedUp = false;
    }
}
