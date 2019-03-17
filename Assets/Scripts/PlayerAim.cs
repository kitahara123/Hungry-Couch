using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    private void Update()
    {
        var mouse = Input.mousePosition;

        var mouseDirection = mouse -  Camera.main.WorldToScreenPoint(transform.position);

        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0,0, angle);
    }
}