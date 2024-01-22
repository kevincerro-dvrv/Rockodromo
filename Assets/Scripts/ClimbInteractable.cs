using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable {
    public Climber climber;

    protected override void Awake()
    {
        base.Awake();
        colliders.Add(GetComponentInChildren<SphereCollider>());
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        MonoBehaviour interactor = (MonoBehaviour)args.interactor;
        climber.SetClimbingHand(interactor, true);
    }

        protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        
        MonoBehaviour interactor = (MonoBehaviour)args.interactor;
        climber.SetClimbingHand(interactor, false);
    }
}
