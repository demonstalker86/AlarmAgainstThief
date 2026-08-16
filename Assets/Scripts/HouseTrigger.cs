using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public event System.Action<bool> ThiefPresenceChanged;

    private bool isThiefInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _) == false) return;

        isThiefInside = true;

        ThiefPresenceChanged?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Thief>(out _) == false) return;

        isThiefInside = false;

        ThiefPresenceChanged?.Invoke(false);
    }

    public bool IsThiefInside()
    {
        return isThiefInside;
    }
}