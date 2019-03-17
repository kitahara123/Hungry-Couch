using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;

    private void Update()
    {
        var horInput = Input.GetAxis("Horizontal");
        var vertInput = Input.GetAxis("Vertical");

        transform.Translate(horInput * Time.deltaTime * speed,
            vertInput * Time.deltaTime * speed,
            0, Space.World);
    }
}