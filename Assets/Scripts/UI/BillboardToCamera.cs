using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Moneybag.UI
{
    [ExecuteAlways]
    public class BillboardToCamera : MonoBehaviour
    {
        private enum BillboardMode { CameraDirection, CameraPosition }
        [SerializeField] private BillboardMode billboardMode;
        
        [SerializeField] private bool billboardTowardsEditorCamera = true;
        
        private void Update()
        {
            Transform cameraTransform = Camera.main.transform;

#if UNITY_EDITOR
            if (billboardTowardsEditorCamera && !Application.isPlaying)
            {
                cameraTransform = SceneView.lastActiveSceneView.camera.transform;
            }
#endif

            switch (billboardMode)
            {
                case BillboardMode.CameraDirection:
                    transform.LookAt(transform.position + cameraTransform.forward);
                    break;
                case BillboardMode.CameraPosition:
                    Vector3 cameraPos = cameraTransform.position;
                    transform.LookAt(cameraPos + 1.1f * (transform.position - cameraPos));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}