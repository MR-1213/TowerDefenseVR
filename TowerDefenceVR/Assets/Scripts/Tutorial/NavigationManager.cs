using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavigationManager : MonoBehaviour
{
    [SerializeField] private TutorialManager tutorialManager;
    public float maxShowDistance = 10f; // ラインを表示する閾値
    public float minShowDistance = 5f; // ラインを非表示にする閾値
    public float linePointSpacing = 0.2f; // ラインの各点の間隔
    [SerializeField] private Transform player;
    [SerializeField] private Transform npc;
    [SerializeField] private Transform[] tutorialPoints;

    private NavMeshAgent navMeshAgent;
    private LineRenderer lineRenderer;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
    }
    
    private void Update()
    {
        float distanceToTarget;
        if(tutorialManager.PTPCoroutineStarted)
        {
            distanceToTarget = Vector3.Distance(player.position, tutorialPoints[0].gameObject.transform.position);
        }
        else if(tutorialManager.AdditionalCoroutineStarted)
        {
            distanceToTarget = Vector3.Distance(player.position, tutorialPoints[1].gameObject.transform.position);
        }
        else
        {
            distanceToTarget = Vector3.Distance(player.position, npc.position);
        }

        // Debug.Log(distanceToTarget);

        if (distanceToTarget >= maxShowDistance)
        {
            if(lineRenderer.enabled == true) return;

            // NPCとの距離が一定以上の場合、ラインを表示
            lineRenderer.enabled = true;
            StartCoroutine(UpdateLineRenderer());
        }
        else if(lineRenderer.enabled == true && distanceToTarget <= minShowDistance)
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

            if(tutorialManager.PTPCoroutineStarted)
            {
                yield return new WaitUntil(() => navMeshAgent.CalculatePath(tutorialPoints[0].position, path));
            }
            else
            {
                yield return new WaitUntil(() => navMeshAgent.CalculatePath(player.position, path));
            }

            int linePointCount = path.corners.Length;
            Vector3[] linePositions = new Vector3[linePointCount];

            for (int i = 0; i < linePointCount; i++)
            {
                linePositions[i] = path.corners[i];
                linePositions[i].y = 0.2f;
            }

            lineRenderer.positionCount = linePointCount;
            lineRenderer.SetPositions(linePositions);
        }
    }
}
