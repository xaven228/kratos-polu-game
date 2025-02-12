using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerManager : MonoBehaviour
{
    public Canvas canvas;
    public float referenceResolutionWidth = 1920f;
    public float referenceResolutionHeight = 1080f;
    public CanvasScaler.ScaleMode scaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
    public CanvasScaler.ScreenMatchMode screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
    public float matchWidthOrHeight = 0.5f;

    void Start()
    {
        if (canvas != null)
        {
            CanvasScaler scaler = canvas.GetComponent<CanvasScaler>();
            if (scaler != null)
            {
                scaler.uiScaleMode = scaleMode;
                scaler.referenceResolution = new Vector2(referenceResolutionWidth, referenceResolutionHeight);
                scaler.screenMatchMode = screenMatchMode;
                scaler.matchWidthOrHeight = matchWidthOrHeight;
            }
            else
            {
                Debug.LogError("CanvasScaler component is missing from the Canvas.");
            }
        }
        else
        {
            Debug.LogError("Canvas is not assigned.");
        }
    }
}
