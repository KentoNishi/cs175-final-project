using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal targetPortal;
        public Transform normalVisible;
    public Transform normalInvisible;
    public Camera portalCamera;
    public Renderer viewthroughRenderer;

    private RenderTexture viewthroughRenderTexture;
    private Material viewthroughMaterial;
    private Camera mainCamera;
    private Vector4 vectorPlane;

    public static Vector3 getVirtPos(Portal target, Vector3 position)
    {
        return target.normalInvisible.TransformPoint(this.normalVisible.InverseTransformPoint(position));
    }

    public static Quaternion getVirtQuat(Portal target, Quaternion rotation) {
        return target.normalInvisible.rotation * Quaternion.Inverse(this.normalVisible.rotation) * rotation;
    }

    private void Start()
    {
        viewthroughRenderTexture = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.DefaultHDR);
        viewthroughRenderTexture.Create();
        viewthroughMaterial = viewthroughRenderer.material;
        viewthroughMaterial.mainTexture = viewthroughRenderTexture;
        portalCamera.targetTexture = viewthroughRenderTexture;
        mainCamera = Camera.main;
        var plane = new Plane(normalVisible.forward, transform.position);
        vectorPlane = new Vector4(plane.normal.x, plane.normal.y, plane.normal.z, plane.distance);
    }

    private void LateUpdate()
    {
        var virtPos = getVirtPos(targetPortal, mainCamera.transform.position);
        var virtQuat = getVirtQuat(targetPortal, mainCamera.transform.rotation);
        portalCamera.transform.SetPositionAndRotation(virtPos, virtQuat);
        var clipThroughSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(portalCamera.worldToCameraMatrix)) * targetPortal.vectorPlane;
        var obliqueProjectionMatrix = mainCamera.CalculateObliqueMatrix(clipThroughSpace);
        portalCamera.projectionMatrix = obliqueProjectionMatrix;
        portalCamera.Render();
    }

    private void OnDestroy()
    {
        viewthroughRenderTexture.Release();
        Destroy(viewthroughMaterial);
        Destroy(viewthroughRenderTexture);
    }
}
