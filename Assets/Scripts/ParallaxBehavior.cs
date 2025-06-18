using UnityEngine;

public class ParallaxBehavior : MonoBehaviour
{
    private float length, startPosX, startPosY;
    [SerializeField] private GameObject target;
    [SerializeField, Range(0f, 1f)] float parallaxStr;
    private float temporary, distanceX, distanceY;

    private void OnEnable()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;

        if (GetComponent<SpriteRenderer>())
        {
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }
    }

    private void OnDisable()
    {
        transform.position = new Vector3(startPosX, startPosY, transform.position.z);
    }

    private void Update()
    {
        temporary = (target.transform.position.x * (1 - parallaxStr));
        distanceX = (target.transform.position.x * parallaxStr);
        distanceY = (target.transform.position.y * parallaxStr);

        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);

        if (temporary > startPosX + length)
            startPosX += length;
        if (temporary < startPosX - length)
            startPosX -= length;
    }
}
