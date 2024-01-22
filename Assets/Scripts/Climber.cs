using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour
{
    public ActionBasedController climbingHand;
    public ActionBasedController secondaryHand;

    public Velocimeter climbingHandVelocimeter;

    public CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (climbingHandVelocimeter != null) {
            characterController.Move(climbingHandVelocimeter.GetVelocity() * Time.deltaTime * -1);
        }
    }

    public void SetClimbingHand(MonoBehaviour interactor, bool grab)
    {
        if (grab) {
            secondaryHand = climbingHand;
            climbingHand = interactor.GetComponent<ActionBasedController>();
            climbingHandVelocimeter = climbingHand.GetComponent<Velocimeter>();
        } else {
            if (secondaryHand == null) {
                climbingHand = null;
                climbingHandVelocimeter = null;
            } else if (secondaryHand.name == interactor.name) {
                secondaryHand = null;
            } else {
                climbingHand = secondaryHand;
                climbingHandVelocimeter = climbingHand.GetComponent<Velocimeter>();
                secondaryHand = null;
            }
        }
    }
}
