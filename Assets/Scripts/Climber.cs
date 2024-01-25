using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Climber : MonoBehaviour {
    public LocomotionSystem locomotionSystem;

    ActionBasedController climbingHand;
    ActionBasedController secondaryHand;

    Velocimeter climbingHandVelocimeter;

    CharacterController charController;

    Vector3 velocity;

    float terminalSpeed = 60f;
    float sqrTerminalSpeed;

    // Start is called before the first frame update
    void Start() {
        charController = GetComponent<CharacterController>();  
        sqrTerminalSpeed = terminalSpeed * terminalSpeed;
    }

    // Update is called once per frame
    void Update() {
        if(climbingHandVelocimeter != null) {
            charController.Move( - climbingHandVelocimeter.GetVelocity() * Time.deltaTime);
        } else {
            velocity += Physics.gravity * Time.deltaTime;
            //Limitamos a velocidade de caída á terminalSpeed.
            //Deste xeito evitamos a posibilidade de atravesar o chan por caer demasiado rápido
            float sqrSpeed = velocity.sqrMagnitude;
            if(sqrSpeed > sqrTerminalSpeed) {
                velocity = velocity.normalized * terminalSpeed;
            }
            charController.Move(velocity * Time.deltaTime);

            if(charController.isGrounded) {
                velocity = Vector3.zero;
            }
        }
        
    }


    public void SetClimbingHand(MonoBehaviour interactor, bool grab) {
        // Se o evento é un agarre, basta pasar a climbingHand a o papel secundario
        // e poñer a man do novo agarre como principal
        if(grab) {
            secondaryHand = climbingHand;
            climbingHand = interactor.GetComponent<ActionBasedController>();
            climbingHandVelocimeter = climbingHand.GetComponent<Velocimeter>();
            locomotionSystem.enabled =false;
        } else {
            //Se o evento é soltar unha man, o primeiro é mirar se hai secondaryHand ou non
            if(secondaryHand == null) {
                velocity = - climbingHandVelocimeter.GetVelocity();
                climbingHand = null;
                climbingHandVelocimeter = null;
                locomotionSystem.enabled = true;
            } else if(secondaryHand.name == interactor.name) {
                //A man que se soltou é a secundaria, nos esquecemos dela sen ter que facer nada máis
                secondaryHand = null;
            } else {
                //A man que se soltou é a primaria, a secundaria pasa ó seu papel
                climbingHand = secondaryHand;
                secondaryHand = null;
                climbingHandVelocimeter = climbingHand.GetComponent<Velocimeter>();
            }
        }

    }
}
