using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class TouchInputProvider : MonoBehaviour
{
    public Vector2? P1Pos { get; private set; }   // jugador de abajo
    public Vector2? P2Pos { get; private set; }   // jugador de arriba

    private int idP1 = -1, idP2 = -1;

    private Vector2 lastP1, lastP2;

    private static Vector2 ClampToScreen(Vector2 p) => new Vector2(Mathf.Clamp(p.x, 0f, Screen.width), Mathf.Clamp(p.y, 0f, Screen.height));

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
#if UNITY_EDITOR
        TouchSimulation.Enable();
#endif
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        TouchSimulation.Disable();
#endif
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        P1Pos = null;
        P2Pos = null;
        float half = Screen.height * 0.5f;

        foreach (var t in Touch.activeTouches)
        {
            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                if (t.touchId == idP1) idP1 = -1;
                if (t.touchId == idP2) idP2 = -1;
                continue;
            }

            Vector2 pos = t.screenPosition;
            bool valid = float.IsFinite(pos.x) && float.IsFinite(pos.y);

            if (t.phase == TouchPhase.Began && valid)
            {
                bool bottom = pos.y < half;
                if (bottom && idP1 < 0) { idP1 = t.touchId; lastP1 = ClampToScreen(pos); }
                else if (!bottom && idP2 < 0) { idP2 = t.touchId; lastP2 = ClampToScreen(pos); }
            }

            if (t.touchId == idP1)
            {
                if (valid) lastP1 = ClampToScreen(pos);
                P1Pos = lastP1;
            }
            if (t.touchId == idP2)
            {
                if (valid) lastP2 = ClampToScreen(pos);
                P2Pos = lastP2;
            }
        }
    }
}