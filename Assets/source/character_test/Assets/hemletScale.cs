using UnityEngine; // ��� Transform �Ҳ���������

public class HelmetScale : MonoBehaviour
{
    public Transform helmetFront; // ͷ��ǰ�˿���������
    public float scrollSpeed = 1f;
    public float minLength = 1f;
    public float maxLength = 3f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector3 scale = helmetFront.localScale;
            scale.z = Mathf.Clamp(scale.z + scroll * scrollSpeed, minLength, maxLength);
            helmetFront.localScale = scale;
        }
    }
}
