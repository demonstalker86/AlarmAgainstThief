using UnityEngine;
using System.Collections.Generic;

public class ThiefJourney : MonoBehaviour
{
    [Header("Маршрут")]
    [SerializeField] private List<Transform> _waypoints;

    [Header("Пауза в центре")]
    [SerializeField] private float _pauseDuration = 2f;

    [Header("Индекс центральной точки")]
    [SerializeField] private int _centerWaypointIndex = 1;

    private ThiefNavigator _navigator;
    private int _currentWaypointIndex = 0;
    private float _pauseTimer = 0f;
    private bool _isPausing = false;

    private void Awake()
    {
        if (TryGetComponent(out _navigator) == false)
        {
            Debug.LogError($"{name}: ThiefNavigator не найден!");

            enabled = false;
            return;
        }

        _navigator.TargetReached += HandleTargetReached;
    }

    private void Start()
    {
        if (_waypoints.Count == 0)
        {
            Debug.LogWarning($"{name}: Маршрут пуст!");

            return;
        }

        NavigateToNextPoint();
    }

    private void Update()
    {
        if (_isPausing == false)
        {
            return;
        }

        _pauseTimer -= Time.deltaTime;

        if (_pauseTimer <= 0f)
        {
            _isPausing = false;

            NavigateToNextPoint();
        }
    }

    private void HandleTargetReached()
    {
        if (_currentWaypointIndex == _centerWaypointIndex)
        {
            _isPausing = true;
            _pauseTimer = _pauseDuration;
        }
        else
        {
            NavigateToNextPoint();
        }
    }

    private void NavigateToNextPoint()
    {
        if (_waypoints.Count == 0)
        {
            return;
        }

        Transform target = _waypoints[_currentWaypointIndex];

        _navigator.SetDestination(target);

        _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Count;
    }

    private void OnDestroy()
    {
        if (_navigator != null)
        {
            _navigator.TargetReached -= HandleTargetReached;
        }            
    }
}