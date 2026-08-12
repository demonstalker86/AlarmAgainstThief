using UnityEngine;
using UnityEngine.AI;

public class ThiefNavigator : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float arrivalThreshold = 0.5f;

    private NavMeshAgent agent;
    private Transform currentTarget;
    private bool hasTarget = false;

    public event System.Action OnTargetReached;

    private void Awake()
    {
        if (!TryGetComponent(out agent))
        {
            Debug.LogError($"{name}: NavMeshAgent не найден!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (!hasTarget || agent == null || !agent.isOnNavMesh || !agent.enabled) return;
        if (agent.pathPending) return;
        if (agent.remainingDistance <= arrivalThreshold)
        {
            hasTarget = false;
            OnTargetReached?.Invoke();
        }
    }

    public void SetDestination(Transform target)
    {
        if (target == null) return;
        if (agent == null || !agent.isOnNavMesh || !agent.enabled)
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