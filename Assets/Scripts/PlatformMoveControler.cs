using System;
using UnityEngine;

public class PlatformMoveControler : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed;

    private Vector3 nextPosition;
    private Vector3 startPo;
    private Vector3 endPo;

    private void Start()
    {
        nextPosition = startPoint.position;
        startPo = startPoint.position;
        endPo = endPoint.position;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, nextPosition);
        if (distance < 0.01f)
        {
            if (nextPosition == startPo)
            {
                nextPosition = endPo;
            }
            else
            {
                nextPosition = startPo;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
      collision.transform.SetParent(transform);
      
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
