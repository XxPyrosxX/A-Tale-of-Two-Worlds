using UnityEngine;

public class CameraController : MonoBehaviour
{

    //want to tell the camera what it should be following
    public GameObject target;

    public float aheadDistance;
    public float aboveDistance;

    public bool following;

    public float slideInTime;

    private Vector3 targetPos;

    void Start()
    {
        following = true;
    }

    void FixedUpdate()
    {
        if (following)
        {
            targetPos = new Vector3(target.transform.position.x, target.transform.position.y + aboveDistance, transform.position.z);

            if (target.transform.localScale.x > 0f)
            {
                targetPos = new Vector3(targetPos.x + aheadDistance, targetPos.y, targetPos.z);
            }
            else
            {
                targetPos = new Vector3(targetPos.x - aheadDistance, targetPos.y, targetPos.z);
            }

            transform.position = Vector3.Lerp(transform.position, targetPos, slideInTime * Time.deltaTime);
        }
    }
}
