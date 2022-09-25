using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform followObject;
    [SerializeField]
    private Vector2 followOffset;
    [SerializeField]
    private float speed = 5f;

    private Vector2 threshold;

    private Rigidbody2D objectRb;

    private void Start()
    {
        threshold = CalculateThreshold();
        objectRb = followObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 follow = followObject.transform.position;
        float xDifference = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDifference = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);

        Vector3 newPosition = transform.position;
        if (Mathf.Abs(xDifference) >= threshold.x)
        {
            newPosition.x = follow.x;
        }
        if (Mathf.Abs(yDifference) >= threshold.y)
        {
            newPosition.y = follow.y;
        }
        float moveSpeed = objectRb.velocity.magnitude > speed ? objectRb.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);
    }

    private Vector3 CalculateThreshold()
    {
        Rect aspect = Camera.main.pixelRect;
        Vector2 threshold = new Vector2(Camera.main.orthographicSize * aspect.width / aspect.height, Camera.main.orthographicSize);
        threshold.x -= followOffset.x;
        threshold.y -= followOffset.y;

        return threshold;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 border = CalculateThreshold();
        Gizmos.DrawWireCube(transform.position, new Vector3(border.x * 2, border.y * 2, 1));
    }
}
