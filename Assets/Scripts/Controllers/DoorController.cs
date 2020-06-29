using GameObjects;
using UnityEngine;

namespace Controllers {
    public class DoorController : MonoBehaviour {
        public Transform doorLeft;
        public Transform doorRight;
        public float openSpeed = 30;
        public Transform doorLock;

        private HingeJoint2D leftHinge;
        private HingeJoint2D rightHinge;
        private Transform player;
        private Plane plane;

        private void Start() {

            // Get hinges
            leftHinge = doorLeft.gameObject.GetComponent<HingeJoint2D>();
            rightHinge = doorRight.gameObject.GetComponent<HingeJoint2D>();

            player = Game.GetPlayer().transform;
            plane = doorLeft.rotation.z > 0f ? Plane.VERTICAL : Plane.HORIZONTAL;
        }

        public void OpenDoor() {
            doorLock.gameObject.SetActive(false);
            OpenHinge(leftHinge, DoorType.DoorLeft);
            OpenHinge(rightHinge, DoorType.DoorRight);
        }
        
        

        private int GetPlayerDirection() {
            if (plane == Plane.HORIZONTAL) {
                return doorLeft.position.y > player.position.y ? 1 : -1;
            }
            else {
                return doorLeft.position.x > player.position.x ? -1 : 1;
            }
        }

        private void OpenHinge(HingeJoint2D hinge, DoorType door) {
            float motorSpeed = (door == DoorType.DoorLeft ? -1 : 1) * openSpeed * GetPlayerDirection();
            JointMotor2D motor = hinge.motor;
            motor.motorSpeed = motorSpeed;
            hinge.motor = motor; 
        }
    }
}