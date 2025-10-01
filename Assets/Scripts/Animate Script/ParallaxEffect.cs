using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxStrength = 0.05f; // how much this layer moves
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        // Move slightly with mouse position
        Vector2 mousePos = Input.mousePosition;
        float moveX = (mousePos.x / Screen.width - 0.5f) * parallaxStrength * Screen.width;
        float moveY = (mousePos.y / Screen.height - 0.5f) * parallaxStrength * Screen.height;

        transform.localPosition = startPos + new Vector3(moveX, moveY, 0);
    }
}
