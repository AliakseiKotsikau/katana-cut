using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
    protected LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
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

    public virtual void Simplify()
    {
        lineRenderer.Simplify(0.3f);
    }
}
