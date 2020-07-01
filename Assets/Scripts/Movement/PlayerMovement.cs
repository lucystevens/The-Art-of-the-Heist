using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Movement {
    public class PlayerMovement : MonoBehaviour {
        
        public float moveSpeed = 5f;
        public float rotateSpeed = 5f;

        public KeyCode leftButton = KeyCode.A;
        public KeyCode rightButton = KeyCode.D;
        public KeyCode upButton = KeyCode.W;
        public KeyCode downButton = KeyCode.S;
        
        private Space _mode;
        private Animator _anim;
        private KeyCode _lastDirection;
        private int _animTrigger;
        private bool _isMoving = false;

        private void Start() {
            _anim = GetComponent<Animator>();
            _animTrigger = Animator.StringToHash("isMoving");
        }

        private void Update() {
            var amount = moveSpeed * Time.deltaTime;
            _isMoving = false;
            var movingInLastDirection = false;

            // We check the last direction first
            if (Input.GetKey(_lastDirection)) {
                MoveInDirection(_lastDirection, amount);
                movingInLastDirection = true;
                _isMoving = true;
            }
            
            // Then check all direction buttons
            CheckMovement(leftButton, amount, movingInLastDirection);
            CheckMovement(rightButton, amount, movingInLastDirection);
            CheckMovement(upButton, amount, movingInLastDirection);
            CheckMovement(downButton, amount, movingInLastDirection);
            

            _anim.SetBool(_animTrigger, _isMoving);
        }

        private void CheckMovement(KeyCode direction, float amount, bool movingInLastDirection) {
            if (_lastDirection != direction && Input.GetKey(direction)) {
                MoveInDirection(direction, amount);
                CheckRotation(direction, movingInLastDirection);
                _isMoving = true;
            }
        }

        private void MoveInDirection(KeyCode direction, float amount) {
            if (direction == leftButton)
                transform.Translate(-amount, 0f, 0f, Space.World);
            else if(direction == rightButton)
                transform.Translate(amount, 0f, 0f, Space.World);
            else if(direction == upButton)
                transform.Translate(0f, amount, 0f, Space.World);
            else if(direction == downButton)
                transform.Translate(0f, -amount, 0f, Space.World);
        }

        private void CheckRotation(KeyCode direction, bool movingInLastDirection) {
            if (_lastDirection != direction && !movingInLastDirection) {
                //Rotate(direction);
                var angle = direction == upButton ? 0f :
                    direction == leftButton ? 90f :
                    direction == downButton ? 180f :
                    270f;
        
                var targetAngle = Quaternion.AngleAxis(angle, Vector3.forward);
                StartCoroutine(
                    RotateOverTime(
                        transform.rotation,
                          targetAngle, 
                             1f / rotateSpeed));
                
                _lastDirection = direction;
            }
        }

        private void Rotate(KeyCode nextDirection) {
            var angle = nextDirection == upButton ? 0f :
                nextDirection == rightButton ? 90f :
                nextDirection == downButton ? 180f :
                270f;
        
            var targetAngle = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * rotateSpeed);
        }
        
        IEnumerator RotateOverTime (Quaternion originalRotation, Quaternion finalRotation, float duration) {
            if (duration > 0f) {
                var startTime = Time.time;
                var endTime = startTime + duration;
                transform.rotation = originalRotation;
                yield return null;
                while (Time.time < endTime) {
                    float progress = (Time.time - startTime) / duration;
                    // progress will equal 0 at startTime, 1 at endTime.
                    transform.rotation = Quaternion.Slerp (originalRotation, finalRotation, progress);
                    yield return null;
                }
            }
            transform.rotation = finalRotation;
        }
    }
}
