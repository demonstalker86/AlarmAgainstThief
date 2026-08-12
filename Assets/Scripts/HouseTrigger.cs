using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    public event System.Action<bool> OnThiefPresenceChanged;

    private bool isThiefInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Thief>(out _)) return;

        isThiefInside = true;
        OnThiefPresenceChanged?.Invoke(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Thief>(out _)) return;

        isThiefInside = false;
        OnThiefPresenceChanged?.Invoke(false);
    }

    public bool IsThiefInside()
    {
        return isThiefInside;
    }
}