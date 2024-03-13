using UnityEngine;

public class PlayerInto : MonoBehaviour
{
    public Transform mCameraPosition;
    private CameraFollow2D cameraFollowScript;

    private void Start()
    {
        // 获取 CameraFollow2D 脚本的引用
        cameraFollowScript = Camera.main.GetComponent<CameraFollow2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 禁用 CameraFollow2D 脚本
            if (cameraFollowScript != null)
            {
                cameraFollowScript.enabled = false;
            }
            // 然后设置摄像机位置到 mCameraPosition 指定的位置，确保 Z 保持不变
            Camera.main.transform.position = new Vector3(mCameraPosition.position.x, mCameraPosition.position.y, Camera.main.transform.position.z);

            // 还需要移动玩家到指定的位置，如果需要的话
            collision.transform.position = new Vector3(mCameraPosition.position.x, mCameraPosition.position.y, collision.transform.position.z);
            EnableCameraFollow();
        }
    }
    public void EnableCameraFollow()
    {
        if (cameraFollowScript != null)
        {
            cameraFollowScript.enabled = true;
        }
    }
}
