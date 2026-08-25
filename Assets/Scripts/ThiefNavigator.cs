using UnityEngine;
using UnityEngine.AI;

public class ThiefNavigator : MonoBehaviour
{
    [Header("Настройки движения")]
    [SerializeField] private float _arrivalThreshold = 0.5f;

    private NavMeshAgent _agent;
    private Transform _currentTarget;
    private bool _hasTarget = false;

    public event System.Action TargetReached;

    private void Awake()
    {
        if (TryGetComponent(out _agent) == false)
        {
            Debug.LogError($"{name}: NavMeshAgent не найден!");

            enabled = false;
        }
    }

    private void Update()
    {
        if (_hasTarget == false || _agent == null || _agent.isOnNavMesh == false || _agent.enabled == false)
        {
            return;
        }            

        if (_agent.remainingDistance <= _arrivalThreshold)
        {
            _hasTarget = false;

            TargetReached?.Invoke();
        }
    }

    public void SetDestination(Transform target)
    {
        if (target == null)
        {
            Debug.LogWarning($"{name}: Попытка установить null цель");

            return;
        }

        if (_agent == null || _agent.isOnNavMesh == false || _agent.enabled == false)
        {
            Debug.LogWarning($"{name}: агент не на NavMesh");

            return;
        }

        _currentTarget = target;
        _hasTarget = true;

        _agent.SetDestination(target.position);
    }

    public void StopMovement()
    {
        _hasTarget = false;

        _agent.ResetPath();
    }
}