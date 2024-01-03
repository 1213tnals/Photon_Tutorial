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

    // 네트워크를 동기화 해주는 photonview 생성
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


    // 특정 오브젝트의 좌표와 열거형(enumeration) 노드를 입력하면,
    // TryGetFeatureValue 함수(다양한 XR 장비들에 적용이 좋음)를 통해서 position, rotation을 반환함
    void MapPosition(Transform target, XRNode node)
    {
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 position);
        InputDevices.GetDeviceAtXRNode(node).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion rotation);

        target.position = position;
        target.rotation = rotation;
    }
}
