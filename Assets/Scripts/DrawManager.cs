using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public const float RESOLUTION = 0.1f;

    [SerializeField]
    private Line colliderLinePrefab;
    [SerializeField]
    private Line slashLinePrefab;

    private Camera cam;

    private Line slashLine;
    private Line colliderLine;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    public void CreateColliderLine(Vector3 mousePosition)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(mousePosition);
        colliderLine = Instantiate(colliderLinePrefab, mousePos, Quaternion.identity);
    }

    public void AddPointToColliderLine(Vector3 mousePosition)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(mousePosition);
        colliderLine.AddPoint(mousePos);
    }

    public void FinishColliderLine()
    {
        colliderLine.Simplify();
    }

    public void CreateSlashLine(Vector3 mousePosition)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(mousePosition);
        slashLine = Instantiate(slashLinePrefab, mousePos, Quaternion.identity);
    }
    
    public void AddPointToSlashLine(Vector3 mousePosition)
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(mousePosition);
        slashLine.AddPoint(mousePos);
    }

    public Vector3[] FinishSlashLine()
    {
        slashLine.Simplify();

        return slashLine.GetLinePoints();
    }
}
