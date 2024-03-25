using UnityEngine;
using UnityEngine.UI;

namespace MainMenu
{
    public class CanvasSetup : MonoBehaviour
    {
        private void Start()
        {
            CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080); 
            canvasScaler.matchWidthOrHeight = 0.5f; 
        }
    }
}