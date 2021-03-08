using UnityEngine;

namespace MHLab.Patch.Launcher.Scripts.UI
{
    public sealed class ProgressBar : MonoBehaviour
    {
        public RectTransform InnerProgressBar;
        [Range(0, 1)]
        public float Value;
        
        private void Update()
        {
            InnerProgressBar.anchorMax = new Vector2(Value, InnerProgressBar.anchorMax.y);
        }
    }
}
