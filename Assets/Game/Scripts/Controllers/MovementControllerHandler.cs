public class MovementControllerHandler
{
    private PlayerInput _playerInput;
    private ClickToMoveController _playerMoveController;
    private DirectionalMovableAutoPatrolController _playerAutoPatrolController;
    private float _idleBehaviourSwitchTime;

    private float _idleTimer;
    private bool _isUsingAutoPatrol;

    public MovementControllerHandler(PlayerInput playerInput,
                                     ClickToMoveController playerMoveController,
                                     DirectionalMovableAutoPatrolController playerAutoPatrolController,
                                     float idleBehaviourSwitchTime)
    {
        _playerInput = playerInput;
        _playerMoveController = playerMoveController;
        _playerAutoPatrolController = playerAutoPatrolController;
        _idleBehaviourSwitchTime = idleBehaviourSwitchTime;

        _playerAutoPatrolController.Disable();
    }

    public void Update(float deltaTime)
    {
        if (_playerInput.LeftMouseButtonDown)
        {
            _idleTimer = 0f;

            if (_isUsingAutoPatrol)
                SetClickController();
        }
        else
        {
            _idleTimer += deltaTime;

            if (_idleTimer >= _idleBehaviourSwitchTime && !_isUsingAutoPatrol)
                SetAutoController();
        }        
    }

    private void SetClickController()
    {
        _playerAutoPatrolController.Disable();
        _playerMoveController.Enable();
        _isUsingAutoPatrol = false;
    }

    private void SetAutoController()
    {
        _playerMoveController.Disable();
        _playerAutoPatrolController.Enable();
        _isUsingAutoPatrol = true;
    }
}
