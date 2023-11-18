using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationToSupportNPC : MonoBehaviour
{
    public float maxDistanceToShowLine = 10f; // ラインを表示する最大距離
    public float linePointSpacing = 0.2f; // ラインの各点の間隔
    [SerializeField] private Transform player;
    [SerializeField] private Transform npc;

    private NavMeshAgent navMeshAgent;
    private LineRenderer lineRenderer;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    
    private void Update()
    {
        float distanceToTarget = Vector3.Distance(player.position, npc.position);

        if (distanceToTarget <= maxDistanceToShowLine)
        {
            if(lineRenderer.enabled == true) return;

            // NPCとの距離が一定以上の場合、ラインを表示
            lineRenderer.enabled = true;
            StartCoroutine(UpdateLineRenderer());
        }
        else
        {
            // NPCとの距離が一定以内の場合、ラインを非表示
            lineRenderer.enabled = false;
            StopCoroutine(UpdateLineRenderer());
        }
    }

    private IEnumerator UpdateLineRenderer()
    {
        while(true)
        {
            NavMeshPath path = new NavMeshPath();
            yield return new WaitUntil(() => navMeshAgent.CalculatePath(npc.position, path));

            int linePointCount = path.corners.Length;
            Vector3[] linePositions = new Vector3[linePointCount];

            for (int i = 0; i < linePointCount; i++)
            {
                linePositions[i] = path.corners[i];
            }

            lineRenderer.positionCount = linePointCount;
            lineRenderer.SetPositions(linePositions);
        }
    }
}
