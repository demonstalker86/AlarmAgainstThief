using UnityEngine;

public class AlarmSound : MonoBehaviour
{
    [Header("Компоненты")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioDistortionFilter distortionFilter;

    [Header("Параметры звука")]
    [SerializeField] private float maxVolume = 1f;
    [SerializeField] private float minVolume = 0f;
    [SerializeField] private float maxDistortion = 0.8f;
    [SerializeField] private float minDistortion = 0f;

    [Header("Скорость изменения")]
    [SerializeField] private float volumeChangeSpeed = 3f;
    [SerializeField] private float distortionChangeSpeed = 3f;

    private float targetVolume;
    private float targetDistortion;

    private void Awake()
    {
        if (audioSource == null && !TryGetComponent(out audioSource))
        {
            Debug.LogError($"{name}: AudioSource не найден!");
            enabled = false;
            return;
        }

        if (distortionFilter == null && !TryGetComponent(out distortionFilter))
        {
            Debug.LogError($"{name}: AudioDistortionFilter не найден!");
            enabled = false;
            return;
        }
    }

    public void SetAlarmState(bool isActive)
    {
        targetVolume = isActive ? maxVolume : minVolume;
        targetDistortion = isActive ? maxDistortion : minDistortion;
    }

    private void Update()
    {
        audioSource.volume = Mathf.MoveTowards(
            audioSource.volume,
            targetVolume,
            volumeChangeSpeed * Time.deltaTime
        );

        distortionFilter.distortionLevel = Mathf.MoveTowards(
            distortionFilter.distortionLevel,
            targetDistortion,
            distortionChangeSpeed * Time.deltaTime
        );
    }
}
