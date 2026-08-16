using UnityEngine;
using System.Collections.Generic;

public class ThiefJourney : MonoBehaviour
{
    [Header("Маршрут (по порядку)")]
    [SerializeField] private List<Transform> waypoints;

    [Header("Пауза в центре дома")]
    [SerializeField] private float pauseDuration = 2f;

    [Header("Индекс центральной точки (для паузы)")]
    [SerializeField] private int centerWaypointIndex = 1;

    private ThiefNavigator navigator;
    private int currentWaypointIndex = 0;
    private float pauseTimer = 0f;
    private bool isPausing = false;

    private void Awake()
    {
        if (TryGetComponent(out navigator) == false)
        {
            Debug.LogError($"{name}: ThiefNavigator не найден!");

            enabled = false;
            return;
        }

        navigator.TargetReached += HandleTargetReached;
    }

    private void Start()
    {
        if (waypoints.Count == 0)
        {
            Debug.LogWarning($"{name}: Маршрут пуст!");

            return;
        }

        NavigateToNextPoint();
    }

    private void Update()
    {
        if (isPausing == false) return;

        pauseTimer -= Time.deltaTime;

        if (pauseTimer <= 0f)
        {
            isPausing = false;

            NavigateToNextPoint();
        }
    }

    private void HandleTargetReached()
    {
        if (currentWaypointIndex == centerWaypointIndex)
        {
            isPausing = true;
            pauseTimer = pauseDuration;
        }
        else
        {
            NavigateToNextPoint();
        }
    }

    private void NavigateToNextPoint()
    {
        if (waypoints.Count == 0) return;

        var target = waypoints[currentWaypointIndex];

        navigator.SetDestination(target);

        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
    }

    private void OnDestroy()
    {
        if (navigator != null)
            navigator.TargetReached -= HandleTargetReached;
    }
}