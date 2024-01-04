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

    // Grab Interactor���� ���� �� �ִ� ���� ���� ���Դٸ� ������ ��û
    [System.Obsolete]
    protected override void OnSelectEntering(XRBaseInteractor interactor)
    {
        photonView.RequestOwnership();
        base.OnSelectEntering(interactor);
    }
}
