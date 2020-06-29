using System;
using GameObjects;
using Misc;
using UnityEngine;

namespace Controllers {
    public class GuardController : MonoBehaviour {
        public Transform[] points;
        public float speed;
        public string animationParam = "IsMoving";

        private PatrolPoint[] _patrolPoints;
        private float _wait;
        private int _destPoint = 0;
        private Animator _anim;
        
        private PlayerController _playerController;
        private bool _playerCaught = false;
        private Transform _playerLocation;

        private GameController _gameController;

        private void Start () {
            _anim = GetComponent<Animator>();
            _anim.SetBool(animationParam, true);

            // Initialise array of properties
            _patrolPoints = new PatrolPoint[points.Length];
            for(var i = 0; i < points.Length; i++) {
                _patrolPoints[i] = points[i].gameObject.GetComponent<PatrolPoint>();
            }

            _playerController = Game.GetPlayer().GetComponent<PlayerController>();
            _wait = _patrolPoints[0].waitTime;

            _gameController = Game.GetGameController();
        }

        private bool IsAtPoint(Vector3 point) {
            return point.Equals(transform.position);
        }

        private bool IsCloseToPoint(Vector3 point) {
            var distance = Math.Abs((transform.position - point).magnitude);
            return distance < 1.1f;
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if (GameTag.Player.CheckObject(other)) {
                
                // Cast a ray towards the player
                var guardPosition = transform.position;
                var dir = (other.transform.position- guardPosition).normalized;
                var hit = Physics2D.Raycast(guardPosition, dir);

                // If it hits the player (and not the wall!)
                if (hit.collider != null && GameTag.Player.CheckObject(hit.collider)) {
                    _playerController.DisableMovement();
                    _playerCaught = true;
                    _playerLocation = other.transform;
                }
            }
        }
        
        private Transform GetNextPoint() {
            // Set the agent to go to the currently selected destination.
            Transform nextPoint = points[_destPoint];
            PatrolPoint patrolPoint = _patrolPoints[_destPoint];

            if (_playerCaught) {
                return _playerLocation;
            }
            else if (IsAtPoint(nextPoint.position)) {
                if (_wait > 0f) {
                    _wait -= Time.deltaTime;
                    _anim.SetBool(animationParam, false);
                }
                else {
                    _destPoint = (_destPoint + 1) % points.Length;
                    nextPoint = points[_destPoint];
                    patrolPoint = _patrolPoints[_destPoint];
                    _wait = patrolPoint.waitTime;
                    _anim.SetBool(animationParam, true);
                }
            }

            return nextPoint;
        }

        private bool HasRotated(Quaternion target) {
            var diff = transform.rotation.eulerAngles.z - target.eulerAngles.z;
            return Math.Abs(diff) < 1f;
        }

        private void Rotate(float angle) {
            var targetAngle = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * speed);
        }

        private void MoveTowardsPoint(Vector3 target) {
            var vectorToTarget = target - transform.position;
            var angle = (Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) - 90;
            var targetAngle = Quaternion.AngleAxis(angle, Vector3.forward);
            
            if (HasRotated(targetAngle)) {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            }
            else {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetAngle, Time.deltaTime * speed);
            }
        }

        private void Update () {
            // Stop moving if game ended
            if (_gameController.HasGameEnded()) {
                return;
            }
            
            // Otherwise calculate next point
            var target = GetNextPoint();
            if (!IsAtPoint(target.position)) {
                if (_playerCaught && IsCloseToPoint(_playerLocation.position)) {
                    _gameController.EndGameAsFailure();
                    _anim.SetBool(animationParam, false);
                }
                else {
                    MoveTowardsPoint(target.position);
                }
            }
        }
    }
}