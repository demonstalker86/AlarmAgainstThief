using UnityEngine;

public class AlarmBinder : MonoBehaviour
{
    [Header("—сылки")]
    [SerializeField] private HouseTrigger houseTrigger;
    [SerializeField] private AlarmSound alarmSound;

    private void Awake()
    {
        if (houseTrigger == null)
        {
            Debug.LogError($"{name}: HouseTrigger не назначен!");

            enabled = false;
            return;
        }

        if (alarmSound == null)
        {
            Debug.LogError($"{name}: AlarmSound не назначен!");

            enabled = false;
            return;
        }

        houseTrigger.ThiefPresenceChanged += alarmSound.SetAlarmState;
    }

    private void OnDestroy()
    {
        if (houseTrigger != null)
            houseTrigger.ThiefPresenceChanged -= alarmSound.SetAlarmState;
    }
}