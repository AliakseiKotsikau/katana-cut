using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DrawManager))]
public class InputManager : MonoBehaviour
{
    private DrawManager drawManager;

    // Start is called before the first frame update
    void Start()
    {
        drawManager = GetComponent<DrawManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessLeftClick();
        ProcessRightClick();
    }

    private void ProcessLeftClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            drawManager.CreateColliderLine(Input.mousePosition);
        }else if (Input.GetMouseButton(0))
        {
            drawManager.AddPointToColliderLine(Input.mousePosition);
        }else if (Input.GetMouseButtonUp(0))
        {
            drawManager.FinishColliderLine();
        }
    }
    
    private void ProcessRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            drawManager.CreateSlashLine(Input.mousePosition);
        }else if (Input.GetMouseButton(1))
        {
            drawManager.AddPointToSlashLine(Input.mousePosition);
        }else if (Input.GetMouseButtonUp(1))
        {
            drawManager.FinishSlashLine();
        }
    }
}
