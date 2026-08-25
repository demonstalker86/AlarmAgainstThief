using UnityEngine;
using System.Collections;

public class AlarmSound : MonoBehaviour
{
    [Header("Компоненты")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioDistortionFilter _distortionFilter;

    [Header("Параметры звука")]
    [SerializeField] private float _maxVolume = 1f;
    [SerializeField] private float _minVolume = 0f;
    [SerializeField] private float _maxDistortion = 0.8f;
    [SerializeField] private float _minDistortion = 0f;

    [Header("Скорость изменения (единиц в секунду)")]
    [SerializeField] private float _volumeChangeSpeed = 0.8f;
    [SerializeField] private float _distortionChangeSpeed = 1.2f;

    private float _targetVolume;
    private float _targetDistortion;
    private Coroutine _changeCoroutine;


    private void Awake()
    {
        if (_audioSource == null && TryGetComponent(out _audioSource) == false)
        {
            Debug.LogError($"{name}: AudioSource не найден!");

            enabled = false;
            return;
        }

        if (_distortionFilter == null && TryGetComponent(out _distortionFilter) == false)
        {
            Debug.LogError($"{name}: AudioDistortionFilter не найден!");

            enabled = false;
            return;
        }

        _audioSource.volume = _minVolume;
        _distortionFilter.distortionLevel = _minDistortion;
        _targetVolume = _minVolume;
        _targetDistortion = _minDistortion;
    }


    public void SetAlarmState(bool isActive)
    {
        _targetVolume = isActive ? _maxVolume : _minVolume;
        _targetDistortion = isActive ? _maxDistortion : _minDistortion;

        if (_changeCoroutine != null)
        {
            StopCoroutine(_changeCoroutine);
        }            

        _changeCoroutine = StartCoroutine(SmoothChange());
    }


    private IEnumerator SmoothChange()
    {
        while (Mathf.Approximately(_audioSource.volume, _targetVolume) == false ||
               Mathf.Approximately(_distortionFilter.distortionLevel, _targetDistortion) == false)
        {
            float newVolume = Mathf.MoveTowards(
                _audioSource.volume,
                _targetVolume,
                _volumeChangeSpeed * Time.deltaTime
            );

            float newDistortion = Mathf.MoveTowards(
                _distortionFilter.distortionLevel,
                _targetDistortion,
                _distortionChangeSpeed * Time.deltaTime
            );

            _audioSource.volume = newVolume;
            _distortionFilter.distortionLevel = newDistortion;
            yield return null;
        }

        _changeCoroutine = null;
    }
}