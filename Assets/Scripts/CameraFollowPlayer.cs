using UnityEngine;

namespace DefaultNamespace
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        [Tooltip("Отступ после которого камера начинает движение")] [SerializeField]
        private float margin = 1f; // 

        [Tooltip("Плавность, чем меньше тем плавнее")] [SerializeField]
        private float smooth = 8f;

        private Transform player;

        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            float targetX = transform.position.x;
            float targetY = transform.position.y;

            // Интерполяция для более плавного движения камеры
            if (Mathf.Abs(transform.position.x - player.position.x) > margin)
                targetX = Mathf.Lerp(transform.position.x, player.position.x, smooth * Time.deltaTime);

            if (Mathf.Abs(transform.position.y - player.position.y) > margin)
                targetY = Mathf.Lerp(transform.position.y, player.position.y, smooth * Time.deltaTime);

            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }
}