using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMovement : MonoBehaviour
{
    public int Loops { get; private set; }

    [SerializeField]
    private List<Vector3> _values;

    [SerializeField]
    private float _transition = 2f;

    [SerializeField] private float checkInterval = 0.1f;

    private Vector3 _currentValue;

    private float _transitionStep;

    private int _valueIndex;

    private void Start()
    {
        _transitionStep = 0;

        _valueIndex = 0;

        Loops = 0;

        _currentValue = transform.position;

        StartCoroutine(PointsMovementRoutine());
    }

    private IEnumerator PointsMovementRoutine()
    {
        while (true)
        {
            if (_transition > _transitionStep)
            {
                _transitionStep += checkInterval;

                float step = _transitionStep / _transition;

                transform.position = Vector3.Lerp(_currentValue, _values[_valueIndex], step);
            }
            else
            {
                _transitionStep = 0;

                _currentValue = _values[_valueIndex];

                _valueIndex = (_valueIndex + 1) % _values.Count;

                Loops++;
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}