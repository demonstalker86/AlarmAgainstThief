using UnityEngine;

public class AlarmBinder : MonoBehaviour
{
    [Header("—сылки")]
    [SerializeField] private HouseTrigger _houseTrigger;
    [SerializeField] private AlarmSound _alarmSound;

    private void Awake()
    {
        if (_houseTrigger == null)
        {
            Debug.LogError($"{name}: HouseTrigger не назначен!");

            enabled = false;
            return;
        }

        if (_alarmSound == null)
        {
            Debug.LogError($"{name}: AlarmSound не назначен!");

            enabled = false;
            return;
        }

        _houseTrigger.ThiefPresenceChanged += _alarmSound.SetAlarmState;
    }


    private void OnDestroy()
    {
        if (_houseTrigger != null)
        {
            _houseTrigger.ThiefPresenceChanged -= _alarmSound.SetAlarmState;
        }            
    }
}