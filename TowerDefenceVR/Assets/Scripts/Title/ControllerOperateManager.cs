using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerOperateManager : MonoBehaviour
{
    [SerializeField] private Transform controller;
    private LineRenderer lineRenderer;
    private Animator animator;
    private Vector3 start;
    private Vector3 end;

    private void Start()
    {
        controller = GetComponent<Transform>();
        lineRenderer = GetComponent<LineRenderer>();
        animator = GetComponent<Animator>();
        
        start = controller.position;
        end = controller.position + controller.forward * 5.0f;
        end.y = start.y;

        Vector3[] positions = new Vector3[2]
        {
            start,
            end
        };

        lineRenderer.SetPositions(positions);
        animator.enabled = true;
    }

    private void Update()
    {
        end = controller.position + controller.forward * 5.0f;
        end.y = start.y;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
