using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// This class scales a game object through interpolation.
/// </summary>
public class ObjectScaler : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private int loops = 3;
    [SerializeField] private float scaleMultiplier = 2;
    [SerializeField] private float transition = 1;
    [SerializeField] private float checkInterval = 0.1f;

    private PointsMovement pointsMovement;
    private bool scale;
    private Vector3 currentValue;
    private float transitionStep;
    private int steps;
    private float direction = 1;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        pointsMovement = GetComponent<PointsMovement>();
    }

    private void Start()
    {
        scale = false;
        currentValue = transform.localScale;
        transitionStep = 0;
        steps = 0;

        StartCoroutine(ObjectScalerRoutine());
    }

    #endregion

    #region Coroutines

    private IEnumerator ObjectScalerRoutine()
    {
        while (true)
        {
            if (pointsMovement.Loops % loops == 0)
            {
                scale = true;
            }

            if (scale && steps < 1)
            {
                transitionStep += direction * checkInterval;

                float step = Math.Min(transitionStep / transition, 1);

                transform.localScale = Vector3.Lerp(currentValue, currentValue * scaleMultiplier, step);

                if (step >= 1)
                {
                    direction = -direction;
                }
                else if (step <= 0)
                {
                    direction = -direction;
                    scale = false;
                    transitionStep = 0;
                    steps = 0;
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    #endregion
}