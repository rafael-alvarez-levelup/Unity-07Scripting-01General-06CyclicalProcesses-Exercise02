using System.Collections;
using UnityEngine;

/// <summary>
/// This class scales a game object through interpolation.
/// </summary>
public class ObjectScaler : MonoBehaviour
{
    #region Private Fields

    [SerializeField] private int loops = 3;
    [SerializeField] private float scaleMultiplier = 2;
    [SerializeField] private float transition = 1;

    private PointsMovement pointsMovement;
    private int currentLoop;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        pointsMovement = GetComponent<PointsMovement>();
    }

    private void Start()
    {
        currentLoop = 0;

        StartCoroutine(CheckRoutine());
    }

    #endregion

    #region Coroutines

    private IEnumerator CheckRoutine()
    {
        yield return new WaitUntil(() => pointsMovement.Loops - currentLoop >= loops);

        yield return StartCoroutine(PingPongRoutine());

        currentLoop = pointsMovement.Loops;

        StartCoroutine(CheckRoutine());
    }

    private IEnumerator PingPongRoutine()
    {
        Vector3 current = transform.localScale;
        Vector3 next = current * scaleMultiplier;

        yield return StartCoroutine(TransitionRoutine(current, next));
        yield return StartCoroutine(TransitionRoutine(next, current));
    }

    private IEnumerator TransitionRoutine(Vector3 current, Vector3 next)
    {
        float transitionStep = 0;

        while (transition > transitionStep)
        {
            transitionStep += Time.deltaTime;

            float step = transitionStep / transition;

            transform.localScale = Vector3.Lerp(current, next, step);

            yield return null;
        }
    }

    #endregion
}