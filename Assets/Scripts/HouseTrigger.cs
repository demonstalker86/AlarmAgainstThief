using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public event System.Action<bool> ThiefPresenceChanged;

    public bool IsThiefInside { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _) == false)
        {
            return;
        }

        IsThiefInside = true;

        ThiefPresenceChanged?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _) == false)
        {
            return;
        }

        IsThiefInside = false;

        ThiefPresenceChanged?.Invoke(false);
    }
}