using System.Collections;
using UnityEngine;

namespace UI
{
    public class FpsCounter : MonoBehaviour
    {
        private GUIStyle _guiStyle;
        private float count;
    
        private IEnumerator Start()
        {
            _guiStyle = new GUIStyle
            {
                fontSize = 50,
            };
            
            GUI.depth = 2;
            while (true)
            {
                count = 1f / Time.unscaledDeltaTime;
                yield return new WaitForSeconds(0.1f);
            }
        }
    
        private void OnGUI()
        {
            GUI.Label(new Rect(10, 40, 500, 125), "FPS: " + Mathf.Round(count), _guiStyle);
        }
    }
}