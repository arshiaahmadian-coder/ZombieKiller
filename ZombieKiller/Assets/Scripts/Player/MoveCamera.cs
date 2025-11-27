using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] Transform cameraPos;

    private void Update()
    {
        transform.position = cameraPos.position;
    }
}
