using UnityEngine;
using UnityEngine.AI;

public class ThiefNavigator : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float arrivalThreshold = 0.5f;

    private NavMeshAgent agent;
    private Transform currentTarget;
    private bool hasTarget = false;

    public event System.Action TargetReached;

    private void Awake()
    {
        if (TryGetComponent(out agent) == false)
        {
            Debug.LogError($"{name}: NavMeshAgent не найден!");

            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (hasTarget == false || agent == null || !agent.isOnNavMesh || agent.enabled == false) return;

        if (agent.pathPending) return;

        if (agent.remainingDistance <= arrivalThreshold)
        {
            hasTarget = false;

            TargetReached?.Invoke();
        }
    }

    public void SetDestination(Transform target)
    {
        if (target == null) return;

        if (agent == null || agent.isOnNavMesh == false || agent.enabled == false)
        {
            Debug.LogWarning($"{name}: агент не на NavMesh, цель не установлена");

            return;
        }

        currentTarget = target;
        hasTarget = true;

        agent.SetDestination(target.position);
    }

    public void StopMovement()
    {
        hasTarget = false;

        agent.ResetPath();
    }
}