using UnityEngine;

[RequireComponent(typeof(PathBilder), typeof(PlayerMover))]
public class Player : MonoBehaviour
{
    [SerializeField] private PathRenderer _hookRenderer;
    [SerializeField] private PathRenderer _trajectoryRenderer;
    [SerializeField] private HookRetainer _hook;
    [SerializeField] private HandMover _handMover;

    private PathBilder _pathBilder;
    private PlayerMover _playerMover;
    private Vector3[] _path;

    private void Awake()
    {
        _pathBilder = GetComponent<PathBilder>();
        _playerMover = GetComponent<PlayerMover>();
        _pathBilder.enabled = false;
        _playerMover.enabled = false;
    }

    private void OnEnable()
    {
        _hookRenderer.Drawed += OnHookDrawed;
        _playerMover.PathPassed += OnPathPassed;
    }

    private void OnDisable()
    {
        _hookRenderer.Drawed -= OnHookDrawed;
        _playerMover.PathPassed -= OnPathPassed;
    }

    public void BuildTrajectoryMovement()
    {
        _pathBilder.enabled = true;
        _path = _pathBilder.PathPoint.ToArray();
        if (_path.Length > 0)
        {
            _trajectoryRenderer.InstantlyDrawPath(_path);
            _handMover.SetDirection(_path);
        }
    }

    public void ThrowHook()
    {
        _trajectoryRenderer.DeleteAllLines();
        _hookRenderer.StartPathDrawing(_path);
        _handMover.StartMove(_hookRenderer);
    }

    public void StartMovement()
    {
        _hook.Unfix();
        _playerMover.SetPath(_path);
        _hookRenderer.ErasePatch(_playerMover, _path);
        _pathBilder.enabled = false;
        _playerMover.enabled = true;
        _handMover.enabled = false;
    }

    private void OnHookDrawed()
    {
        StartMovement();
    }

    private void OnPathPassed()
    {
        int lenghtOffset = 1;
        _hook.Fix(_path[_path.Length - lenghtOffset]);
    }
}
