using Unity.Cinemachine;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public CinemachineCamera camera;

    private void OnTriggerEnter(Collider other)
    {
        camera.Prioritize();

    }
}
