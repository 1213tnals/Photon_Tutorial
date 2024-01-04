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

    // ��Ʈ��ũ�� ����ȭ ���ִ� photonview ����
    private PhotonView photonview;

    // �ִϸ����� �߰�
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;

    void Start()
    {
        photonview = GetComponent<PhotonView>();

        XROrigin rig = FindObjectOfType<XROrigin>();
        headRig = rig.transform.Find("Camera Offset/Main Camera");
        leftHandRig = rig.transform.Find("Camera Offset/Left Controller");
        rightHandRig = rig.transform.Find("Camera Offset/Right Controller");

        // �ִϸ����� �߰�
        if (photonview.IsMine == true)
        {
            foreach (var item in GetComponentsInChildren<Renderer>())
            {
                item.enabled = false;    // �ڽ����� �ִ� Renderer�鿡 ���ؼ� enable�� false��
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


    // �ִϸ����� �߰�
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


    // Ư�� ������Ʈ�� ��ǥ�� ������(enumeration) ��带 �Է��ϸ�,
    // TryGetFeatureValue �Լ�(�پ��� XR ���鿡 ������ ����)�� ���ؼ� position, rotation�� ��ȯ��
    void MapPosition(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
    }
}