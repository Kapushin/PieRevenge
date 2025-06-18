using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        EventManager.OnCursorOn += CursorOn;
        EventManager.OnCursorOff += CursorOff;
    }
    private void OnDisable()
    {
        EventManager.OnCursorOn -= CursorOn;
        EventManager.OnCursorOff -= CursorOff;
    }

    private void CursorOn()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
