using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class RewindHandler : MonoBehaviour
{
    [Tooltip("Length of rewind in seconds. (Min. 0, Max. 10).")]
    [Range(1, 10)]
    [SerializeField] private int _rewindDuration;
    [Tooltip("Number of frames to record per second.")]
    [SerializeField] private int _frameRate;

    public enum RewindState { Record, Retrieve };
    public RewindState CurrentState { private set; get; }
    public List<RewindPoint> _rewindablePoints;
    
    private float _timerRef = 0;
    private int _maxSize;
    private float _frameRateAsDelta;


    private void Start()
    {
        _frameRateAsDelta = 1 / (float)_frameRate;
        _rewindablePoints = new List<RewindPoint>();
        _maxSize = _rewindDuration * _frameRate;
        CurrentState = RewindState.Record;
    }

    private void Update()
    {
        switch(CurrentState)
        {
            case RewindState.Record:
                Debug.Log("Recording!!");
                _timerRef += Time.deltaTime;
                if (_timerRef >= _frameRateAsDelta)
                {
                    RewindPoint newPoint = new RewindPoint();
                    newPoint.position = transform.position;

                    if (_rewindablePoints.Count == _maxSize)
                    {
                        _rewindablePoints.RemoveAt(0);
                    }
                    _rewindablePoints.Add(newPoint);
                    _timerRef = 0;
                }
                break;

            case RewindState.Retrieve:
                Debug.Log("Rewinding!!");
                if (_timerRef == _frameRateAsDelta)
                {
                    if (_rewindablePoints.Count > 0)
                    {
                        transform.position = _rewindablePoints[_rewindablePoints.Count - 1].position;
                        _rewindablePoints.RemoveAt(_rewindablePoints.Count - 1);
                    }
                }

                _timerRef -= Time.deltaTime;

                if (_rewindablePoints.Count == 0)
                {
                    Debug.Log("Empty!");
                    StartRecording();
                }

                if (_timerRef <= 0)
                {
                    _timerRef = _frameRateAsDelta;
                }
                break;
        }
    }

    public void StartRecording()
    {
        CurrentState = RewindState.Record;
    }

    public void StopRecording()
    {
        CurrentState = RewindState.Retrieve;
        _timerRef = _frameRateAsDelta;
    }
}
