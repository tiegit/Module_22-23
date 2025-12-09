using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private GameObject _pointerView;

    private PlayerInput _playerInput;
    private IPointerTargetOwner _pointerTargetOwner;

    private bool _isVisible = true;

    public void Initialize(PlayerInput playerInput, IPointerTargetOwner pointerTargetOwner)
    {
        _playerInput = playerInput;
        _pointerTargetOwner = pointerTargetOwner;

        Hide();
    }

    private void Update()
    {
        if (transform.position != _pointerTargetOwner.TargetPosition)
            transform.position = _pointerTargetOwner.TargetPosition;

        if (_playerInput.LeftMouseButtonDown && _pointerTargetOwner.HasTarget)
            ShowAt();

        if (_pointerTargetOwner.HasTarget == false)
            Hide();
    }

    private void ShowAt()
    {
        if (_isVisible)
            return;

        _pointerView.gameObject.SetActive(true);

        _isVisible = true;
    }

    private void Hide()
    {
        if (_isVisible == false)
            return;

        _pointerView.gameObject.SetActive(false);

        _isVisible = false;
    }
}