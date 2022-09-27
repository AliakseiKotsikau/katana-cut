using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ColliderLine : Line
{
    [SerializeField]
    private EdgeCollider2D edgeCollider;

    // Start is called before the first frame update
    void Start()
    {
        edgeCollider.transform.position -= transform.position;
    }

    public override void Simplify()
    {
        lineRenderer.Simplify(SIMPLIFICATION_FACTOR);

        SetColliderPoints();
    }

    private void SetColliderPoints()
    {
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);

        edgeCollider.points = points.Select(point => (Vector2)point).ToArray();
    }
}
