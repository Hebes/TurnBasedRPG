using System;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    private Camera myCamera;
    private Func<Vector3> GetCameraFollowPositionFunc;
    private Func<float> GetCameraZoomFunc;

    private float cameraMoveSpeed = 10f;//相机移动速度

    protected void Awake() => myCamera = GetComponent<Camera>();

    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    /// <summary>
    /// 设置
    /// </summary>
    /// <param name="GetCameraFollowPositionFunc">获得相机跟随位置函数</param>
    /// <param name="GetCameraZoomFunc">获得相机缩放函数</param>
    /// <param name="teleportToFollowPosition">传送跟踪位置</param>
    /// <param name="instantZoom">即时放大</param>
    public void Setup(Func<Vector3> GetCameraFollowPositionFunc, Func<float> GetCameraZoomFunc, bool teleportToFollowPosition, bool instantZoom)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
        this.GetCameraZoomFunc = GetCameraZoomFunc;

        if (teleportToFollowPosition)
        {
            Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
            cameraFollowPosition.z = transform.position.z;
            transform.position = cameraFollowPosition;
        }

        if (instantZoom)
            myCamera.orthographicSize = GetCameraZoomFunc();
    }

    /// <summary>
    /// 设置相机跟随位置
    /// </summary>
    /// <param name="cameraFollowPosition"></param>
    public void SetCameraFollowPosition(Vector3 cameraFollowPosition)
    {
        SetGetCameraFollowPositionFunc(() => cameraFollowPosition);
    }

    /// <summary>
    /// 设置摄像机跟随位置的委托
    /// </summary>
    /// <param name="GetCameraFollowPositionFunc"></param>
    public void SetGetCameraFollowPositionFunc(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    /// <summary>
    /// 设置相机变焦
    /// </summary>
    /// <param name="cameraZoom"></param>
    public void SetCameraZoom(float cameraZoom)
    {
        SetGetCameraZoomFunc(() => cameraZoom);
    }

    /// <summary>
    /// 设置相机变焦委托
    /// </summary>
    /// <param name="GetCameraZoomFunc"></param>
    public void SetGetCameraZoomFunc(Func<float> GetCameraZoomFunc)
    {
        this.GetCameraZoomFunc = GetCameraZoomFunc;
    }


    /// <summary>
    /// 相机处理运动
    /// </summary>
    private void HandleMovement()
    {
        if (GetCameraFollowPositionFunc == null) return;
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);


        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
                // Overshot the target
                newCameraPosition = cameraFollowPosition;

            transform.position = newCameraPosition;
        }
    }


    /// <summary>
    /// 相机变焦处理
    /// </summary>
    private void HandleZoom()
    {
        if (GetCameraZoomFunc == null) return;
        float cameraZoom = GetCameraZoomFunc();

        float cameraZoomDifference = cameraZoom - myCamera.orthographicSize;
        float cameraZoomSpeed = 1f;

        myCamera.orthographicSize += cameraZoomDifference * cameraZoomSpeed * Time.deltaTime;

        if (cameraZoomDifference > 0)
        {
            if (myCamera.orthographicSize > cameraZoom)
                myCamera.orthographicSize = cameraZoom;
        }
        else
        {
            if (myCamera.orthographicSize < cameraZoom)
                myCamera.orthographicSize = cameraZoom;
        }
    }
}
