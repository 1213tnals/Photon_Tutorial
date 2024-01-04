using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class XRGrabNetworkInteractable : XRGrabInteractable
{
    private PhotonView photonView;
    
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Grab Interactor에서 잡을 수 있는 범위 내로 들어왔다면 소유권 요청
    [System.Obsolete]
    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        photonView.RequestOwnership();
        base.OnSelectEntering(interactor);
    }
}
