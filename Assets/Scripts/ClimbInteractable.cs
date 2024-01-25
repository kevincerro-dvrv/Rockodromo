using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable {
    public Climber climber;

    protected override void Awake() {
        base.Awake();
        colliders.Add(GetComponentInChildren<SphereCollider>());
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args) {
        base.OnSelectEntered(args);

        Debug.Log("ClimbInteractable.OnSelectEntered");
        
        MonoBehaviour interactor = (MonoBehaviour)args.interactorObject;
        climber.SetClimbingHand(interactor, true);
    }

    protected override void OnSelectExited(SelectExitEventArgs args) {
        base.OnSelectExited(args);

        Debug.Log("ClimbInteractable.OnSelectExited");
        
        MonoBehaviour interactor = (MonoBehaviour)args.interactor;
        climber.SetClimbingHand(interactor, false);
    }



}
