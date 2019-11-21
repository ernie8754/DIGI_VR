using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// 抛物线脚本
/// </summary>

public class ParabolaSys : MonoBehaviour
{
    private Transform CurrentHand; //当前触发的手
    private List<Vector3> pointList; //曲线点集合
    private Quaternion lastRotation; //上一个移动的角度
    private Vector3 lastPostion; //上一个位置
    private Vector3 lastPos; //上一个点，为了优化将一个临时变量做成全局的，节省内存开销
    private Vector3 nextPos;//下一个点，理由同上
    private event Action OnChangeTransform;//一个事件，用来检测手柄位置和角度变化的
    public Material material;//渲染射线的材质球
    private Vector3 HitPoint;//抛物线的碰撞点
    private Ray ray;
    private bool canJump = false;//
 
    public GameObject PointEffect;//一个特效，就是在射线的终点放置一个光柱什么的，大家可以自己做这个特效
 
    public int MaxPoint;  //生成曲线的点最大数量
    public float Distence;//水平位移
    public float Grity;//垂直位移
    public float CheckRange;//检测位置是否存在障碍物
    public Vector3 hitPoint { get { return HitPoint; } }
 
    public void Awake()
    {
        SetData();
    }
 
    public void Start()
    {
        pointList = new List<Vector3>();
        OnChangeTransform += OnChangeTransformCallBack;
        HitPoint = -Vector3.one;
        ray = new Ray();
 
 
    }
    public void Update()
    {
        //当手柄按下触摸键同时角度合适时触发事件开始计算点
        if (CurrentHand != null && ((CurrentHand.eulerAngles.x > 275 && CurrentHand.eulerAngles.x <= 360) || (CurrentHand.eulerAngles.x >= -0.01f && CurrentHand.eulerAngles.x < 85)))
        {
            if (OnChangeTransform != null) OnChangeTransform();
        }
        else
        {
            pointList.Clear();
            PointEffect.SetActive(false);
        }
    }
    public void paraStart(Transform obj)
    {
        CurrentHand = obj;
    }
    /// <summary>
    /// 计算抛物线的点
    /// 此方法已经优化过性能
    ///
    /// </summary>
    private void OnChangeTransformCallBack()
    {
        if (lastRotation != CurrentHand.rotation || lastPostion != CurrentHand.position)
        {
            lastPos = nextPos = CurrentHand.position;
            int i = 0;
            while (nextPos.y > 8 && (i < MaxPoint))
            {
                if (pointList.Count <= i)
                {
                    pointList.Add(nextPos);
                }
                else
                {
                    pointList[i] = nextPos;
                }
                if (lastRotation == CurrentHand.rotation && lastPostion != CurrentHand.position && i < pointList.Count - 1)
                {
                    nextPos = pointList[i + 1] + CurrentHand.position - lastPostion;
                }
                else
                {
                    nextPos = lastPos + CurrentHand.rotation * Vector3.forward * Distence + Vector3.up * Grity * 0.1f * i * Time.fixedDeltaTime;
                }
                lastPos = nextPos;
                i++;
            }
            if (pointList.Count > i)
            {
                pointList.RemoveRange(i, pointList.Count - i);
            }
            lastRotation = CurrentHand.rotation;
            lastPostion = CurrentHand.position;
            if (pointList.Count > 1)
            {
                HitPoint = pointList[pointList.Count - 1];
                PointEffect.SetActive(true);
                PointEffect.transform.position = HitPoint;
                //print(HitPoint);
            }
            else
            {
                HitPoint = -Vector3.one;
                PointEffect.SetActive(false);
            }
        }
    }
 
    /*public void Enable()
    {
        SteamVR_InitManager.Instance.OnLeftDeviceActive += OnHandActive;
        SteamVR_InitManager.Instance.OnRightDeviceActive += OnHandActive;
        OnChangeTransform += OnChangeTransformCallBack;
    }*/
    /*public void OnHandActive(SteamVR_TrackedObject obj)
    {
        DeviceInput device = obj.GetComponent<DeviceInput>();
        device.OnPressDownPadV3 += OnPressDownPad;
        device.OnPressUpPad += OnPressUpPadAction;
    }*/
 
    /*public void OnHandDis(SteamVR_TrackedObject obj)
    {
        if (obj && obj.gameObject.activeSelf)
        {
            DeviceInput device = obj.GetComponent<DeviceInput>();
            device.OnPressDownPadV3 -= OnPressDownPad;
            device.OnPressUpPad -= OnPressUpPadAction;
        }
    }*/
 
    /*public void Disable()
    {
        SteamVR_InitManager.Instance.OnLeftDeviceActive -= OnHandActive;
        SteamVR_InitManager.Instance.OnRightDeviceActive -= OnHandActive;
        OnHandDis(SteamVR_InitManager.Instance.LeftObject);
        OnHandDis(SteamVR_InitManager.Instance.LeftObject);
        OnChangeTransform -= OnChangeTransformCallBack;
    }*/
 
    public void SetData()
    {
        if (PointEffect)
            PointEffect.SetActive(false);
    }
 
    /// <summary>
    /// 抬起触摸板时，计算落脚点
    /// </summary>
    private void OnPressUpPadAction()
    {
        if (CurrentHand == null) return;
        canJump = true;
        ray.origin = CurrentHand.position;
        Vector3 dir = HitPoint - CurrentHand.position;
        ray.direction = dir;
        if (Physics.CheckSphere(HitPoint, CheckRange, ~(1 << 8)))
        {
            canJump = false;
        }
        if (canJump)
        {
            JumpPoint(HitPoint);
        }
        CurrentHand = null;
    }
    public Vector3 hitPointCompute()
    {
        ray.origin = CurrentHand.position;
        Vector3 dir = HitPoint - CurrentHand.position;
        ray.direction = dir;

            return HitPoint;
        throw new System.ArgumentException("Not hit the floor", "Wrong Call");
    }
    /// <summary>
    /// 跳到指定的点
    /// </summary>
    /// <param name="point"></param>
    public void JumpPoint(Vector3 point)
    {
        point.y = transform.position.y;
        transform.position = point;
    }
 
    private void OnPressDownPad(Transform parent)
    {
 
        CurrentHand = parent;
 
    }
 
    /// <summary>
    /// 使用GL来绘制曲线
    /// 将点绘制出来
    /// </summary>
    void OnRenderObject()
    {
        material.SetPass(0);
        if (pointList == null) return;
        GL.Begin(GL.LINES);
        for (int i = 0; i < pointList.Count; i++)
        {
            GL.Vertex(pointList[i]);
        }
        GL.End();
    }

    /// <summary>
    /// 一个额外的附加方法，即用一个曲线来绘制抛物线，性能较低，因为点数比较多
    /// 感兴趣的可以把此方法添加到Update中更新
    /// </summary>


    public void ShowLineByRender()
    {
 
        LineRenderer line = GetComponent<LineRenderer>();
        if (line)
        {
            line.positionCount = pointList.Count;
            for (int i = 0; i < pointList.Count; i++)
            {
                line.SetPosition(i, pointList[i]);
            }
        }
    }
}
