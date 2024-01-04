using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;
using Unity.XR.CoreUtils;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    private Transform headRig;
    private Transform leftHandRig;
    private Transform rightHandRig;

    // 네트워크를 동기화 해주는 photonview 생성
    private PhotonView photonview;

    // 애니메이터 추가
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    void Start()
    {
        photonview = GetComponent<PhotonView>();

        XROrigin rig = FindObjectOfType<XROrigin>();
        headRig = rig.transform.Find("Camera Offset/Main Camera");
        leftHandRig = rig.transform.Find("Camera Offset/Left Controller");
        rightHandRig = rig.transform.Find("Camera Offset/Right Controller");

        // 애니메이터 추가
        if (photonview.IsMine == true)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;    // 자식으로 있는 Renderer들에 대해서 enable을 false로
            }
        }
    }

    void Update()
    {
        if (photonview.IsMine==true)
        {
            MapPosition(head, headRig);
            MapPosition(leftHand, leftHandRig);
            MapPosition(rightHand, rightHandRig);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
        }
    }


    // 애니메이터 추가
    void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }


    // 특정 오브젝트의 좌표와 열거형(enumeration) 노드를 입력하면,
    // TryGetFeatureValue 함수(다양한 XR 장비들에 적용이 좋음)를 통해서 position, rotation을 반환함
    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}