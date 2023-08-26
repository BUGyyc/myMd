using UnityEngine;

public class CullingGroupExample : MonoBehaviour
{
    private CullingGroup cullingGroup;

    private void Start()
    {
        // 创建CullingGroup并设置相机
        cullingGroup = new CullingGroup();
        cullingGroup.targetCamera = Camera.main;

        // 设置回调函数
        cullingGroup.onStateChanged += OnStateChanged;

        // 添加需要进行可见性剔除的物体
        GameObject[] objectsToCull = GameObject.FindGameObjectsWithTag("Cullable");
        for (int i = 0; i < objectsToCull.Length; i++)
        {
            // 获取物体的边界信息
            Renderer renderer = objectsToCull[i].GetComponent<Renderer>();
            Bounds bounds = renderer.bounds;

            // 添加物体到CullingGroup
            CullingGroupEntry entry = new CullingGroupEntry();
            entry.renderer = renderer;
            entry.bounds = bounds;
            cullingGroup.AddEntry(entry, CullingGroupDefaultState.Visible);
        }

        // 更新CullingGroup
        cullingGroup.SetBoundingSpheres();
        cullingGroup.SetBoundingSphereCount(objectsToCull.Length);
    }

    private void OnStateChanged(CullingGroupEvent ev)
    {
        // 物体的可见性状态发生变化时的回调函数
        if (ev.isVisible)
        {
            // 物体可见，启用渲染组件
            ev.renderer.enabled = true;
        }
        else
        {
            // 物体不可见，禁用渲染组件
            ev.renderer.enabled = false;
        }
    }

    private void Update()
    {
        // 更新CullingGroup
        cullingGroup.SetDistanceReferencePoint(Camera.main.transform.position);
        cullingGroup.SetBoundingSpheres();
        cullingGroup.SetBoundingSphereCount(cullingGroup.targetCount);

        // 执行渲染操作
        // ...
    }

    private void OnDestroy()
    {
        // 销毁CullingGroup
        cullingGroup.Dispose();
    }
}
