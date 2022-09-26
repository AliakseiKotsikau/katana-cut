using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
    [SerializeField]
    private EdgeCollider2D edgeCollider;

    private LineRenderer lineRenderer;

    private List<Vector2> points = new List<Vector2>();

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        edgeCollider.transform.position -= transform.position;
    }

    public void AddPoint(Vector2 pos)
    {
        if (!CanAppend(pos)) return;

        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, pos);
    }

    private bool CanAppend(Vector2 pos)
    {
        if (lineRenderer.positionCount == 0) return true;

        return Vector2.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), pos) > DrawManager.RESOLUTION;
    }

    public void Simplify()
    {
        lineRenderer.Simplify(0.3f);

        SetColliderPoints();
    }

    private void SetColliderPoints()
    {
        Vector3[] points = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(points);

        edgeCollider.points = points.Select(point => (Vector2)point).ToArray();
    }
}
