using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    public GameObject movingObject;
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed;
    public Vector3 currentTargetPos;
    
    void Start()
    {
        currentTargetPos = endPoint.position;
    }

    
    void Update()
    {
        movingObject.transform.position = Vector3.MoveTowards(movingObject.transform.position, currentTargetPos, moveSpeed * Time.deltaTime);

        if(movingObject.transform.position == endPoint.position)
        {
            currentTargetPos = startPoint.position;
        }
        if(movingObject.transform.position == startPoint.position)
        {
            currentTargetPos = endPoint.position;
        }
    }
}
