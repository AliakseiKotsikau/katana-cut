using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    public const float RESOLUTION = 0.1f;

    [SerializeField]
    private Line linePrefab;

    private Camera cam;

    private Line currentLine;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            currentLine = Instantiate(linePrefab, mousePos, Quaternion.identity);
        }
        
        if(Input.GetMouseButton(0))
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            currentLine.AddPoint(mousePos);
        }

        if(Input.GetMouseButtonUp(0))
        {
            currentLine.Simplify();
        }
    }
}
