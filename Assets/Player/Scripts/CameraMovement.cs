using UnityEngine;
using System.Collections;

namespace Game.Player
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private Vector2 _border;
        [SerializeField] private float _cameraSpeed;
        private Vector3 _mousePosition;
        private Transform _camera;
        private Vector3 _displacementVector;
        private Vector2 _cameraSize;
        private Vector2 _borderAxisX;
        private Vector2 _borderAxisY;

        private bool isCameraMovementBlocked = true;


        private void Start()
        {
            StartCoroutine(Setup());
        }

        private IEnumerator Setup()
        {
            yield return new WaitForSeconds(0.01f);
            _camera = GetComponent<Transform>();
            _cameraSize = new Vector2(Screen.width, Screen.height);
            var orthographicSize = Camera.main.orthographicSize;
            var screenProportions = _cameraSize.x / _cameraSize.y;
            var grid = FindObjectOfType<Game.Grid.GridManager>();
            var mapSize = grid.globalSize;
            var hexSize = grid.hexSize;
            Debug.Log(mapSize);
            _borderAxisX = new Vector2(orthographicSize * screenProportions - hexSize.x / 2, mapSize.x - orthographicSize * screenProportions + hexSize.x);
            _borderAxisY = new Vector2(orthographicSize - hexSize.y / 2, mapSize.y - orthographicSize + hexSize.y / 2);
            isCameraMovementBlocked = true;
        }

        private void Update()
        {
            if (!isCameraMovementBlocked) return;

            _displacementVector = Vector3.zero;
            _mousePosition = Input.mousePosition;

            if (_mousePosition.x < _border.x) _displacementVector.x -= _cameraSpeed;
            if (_mousePosition.x > _cameraSize.x - _border.x) _displacementVector.x += _cameraSpeed;
            if (_mousePosition.y < _border.y) _displacementVector.y -= _cameraSpeed;
            if (_mousePosition.y > _cameraSize.y - _border.y) _displacementVector.y += _cameraSpeed;
            var newCamPos = _camera.position + _displacementVector;

            newCamPos.x = Mathf.Clamp(newCamPos.x, _borderAxisX.x, _borderAxisX.y);
            newCamPos.y = Mathf.Clamp(newCamPos.y, _borderAxisY.x, _borderAxisY.y);

            _camera.position = newCamPos;
        }
    }
}
