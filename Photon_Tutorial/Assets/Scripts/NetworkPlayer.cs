using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class NetworkPlayer : MonoBehaviour
{
    public Transform head;
    public Transform leftHand;
    public Transform rightHand;

    // ��Ʈ��ũ�� ����ȭ ���ִ� photonview ����
    private PhotonView photonview;

    void Start()
    {
        photonview = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (photonview.IsMine==true)
        {
            head.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
            rightHand.gameObject.SetActive(false);

            MapPosition(head, XRNode.Head);
            MapPosition(leftHand, XRNode.LeftHand);
            MapPosition(rightHand, XRNode.RightHand);
        }
    }


    // Ư�� ������Ʈ�� ��ǥ�� ������(enumeration) ��带 �Է��ϸ�,
    // TryGetFeatureValue �Լ�(�پ��� XR ���鿡 ������ ����)�� ���ؼ� position, rotation�� ��ȯ��
    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;
    }
}
