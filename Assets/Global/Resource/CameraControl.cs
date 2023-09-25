using Game.GlobalSystems;
using UnityEngine;
using GameGrid = Game.GridSystem.GameGrid;

namespace Game
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] private Vector2 _border;
        [SerializeField] private float _cameraSpeed;
        private Transform _camera;
        private Vector3 _displacementVector;
        private Vector2 _cameraSize;
        private Vector2 _borderAxisX;
        private Vector2 _borderAxisY;
        private bool isCameraMovementBlocked = true;
        [SerializeField] private GameObject activeTechnologies;

        public void CalculateCameraMovementBorder(Vector2 mapSize, Vector2 hexSize){
            
            _camera = transform;
            _cameraSize = new Vector2(Screen.width, Screen.height);
            var orthographicSize = Camera.main.orthographicSize;
            var screenProportions = _cameraSize.x / _cameraSize.y * orthographicSize;
            _borderAxisX = new Vector2(screenProportions - hexSize.x / 2, mapSize.x - screenProportions + hexSize.x);
            _borderAxisY = new Vector2(orthographicSize - hexSize.y / 2 - 2.8f, mapSize.y - orthographicSize + hexSize.y / 2);
            isCameraMovementBlocked = false;
        }

        private void FixedUpdate()
        {
            if (isCameraMovementBlocked) return;
            if (activeTechnologies.activeSelf) return;

            _displacementVector = Vector3.zero;
            var _mousePosition = Input.mousePosition;

            if (_mousePosition.x < _border.x) _displacementVector.x -= _cameraSpeed * Time.fixedDeltaTime;
            if (_mousePosition.x > _cameraSize.x - _border.x) _displacementVector.x += _cameraSpeed * Time.fixedDeltaTime;
            if (_mousePosition.y < _border.y) _displacementVector.y -= _cameraSpeed * Time.fixedDeltaTime;
            if (_mousePosition.y > _cameraSize.y - _border.y) _displacementVector.y += _cameraSpeed * Time.fixedDeltaTime;
            
            var newCamPos = _camera.position + _displacementVector;
            newCamPos.x = Mathf.Clamp(newCamPos.x, _borderAxisX.x, _borderAxisX.y);
            newCamPos.y = Mathf.Clamp(newCamPos.y, _borderAxisY.x, _borderAxisY.y);
            _camera.position = newCamPos;
        }
    }
}