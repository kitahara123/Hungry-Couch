using UnityEngine;

public class MouseShooter : MonoBehaviour
{

    [SerializeField] private Projectile projectile;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var proj = Instantiate(projectile);
            var tmp = proj.transform.rotation;
            proj.transform.rotation = transform.rotation * tmp;
        }
    }
}