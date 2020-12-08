using UnityEngine;

public class GPUGraph : MonoBehaviour
{
    [SerializeField, Range(10, 200)]
    int resolution = 10;

    [SerializeField]
    FunctionLibaray.FunctionName function = default;

    [SerializeField, Min(0f)]
    float functionDuration = 1f, transitionDuration = 1f;

    public enum TransitionMode { Cycle, Random }
    [SerializeField]
    TransitionMode transitionMode = TransitionMode.Cycle;
    
    float duration;
    bool transitioning;
    FunctionLibaray.FunctionName transitionFunction;

    ComputeBuffer positionsBuffer;

    private void OnEnable()
    {
        positionsBuffer = new ComputeBuffer(resolution * resolution, 3 * 4);
    }

    private void OnDisable()
    {
        positionsBuffer.Release();
        positionsBuffer = null;
    }
    void Update()
    {
        duration += Time.deltaTime;

        if (transitioning)
        {
            if (duration >= transitionDuration)
            {
                duration -= transitionDuration;
                transitioning = false;
            }
        }
        else if (duration >= functionDuration)
        {
            duration -= functionDuration;

            transitioning = true;
            transitionFunction = function;

            PickNextFunction();
        }
        
    }

    void PickNextFunction()
    {
        function = transitionMode == TransitionMode.Cycle ?
            FunctionLibaray.GetNextFunctionName(function) :
            FunctionLibaray.GetRandomFunctionNameOtherThan(function);
    }

}
