using System;
using UnityEngine;
using UnityEngine.Events;

public class PathRenderer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _drawingSpeed = 45f;

    private int _indexTargetPoint;
    private Vector3[] _path;
    private Vector3 _currentPosition;
    private PlayerMover _mover;
    private RenderState _renderState = RenderState.Stop;
    private RenderedPath _renderedPath = new RenderedPath();

    public Vector3 CurrentPosition => _currentPosition;
    public int IndexTargetPosition => _indexTargetPoint;

    public event UnityAction Drawed;

    private void Update()
    {
        if (_renderState == RenderState.Stop)
            return;

        if (_renderState == RenderState.Draw)
        {
            _currentPosition = Vector3.MoveTowards(_currentPosition, _path[_indexTargetPoint], _drawingSpeed * Time.deltaTime);
            TrySetTargetIndex();
        }
        if (_renderState == RenderState.Erase)
        {
            _currentPosition = _mover.Position;
            _indexTargetPoint = _mover.IndexCurrentPoint;            
            if (_mover.IsEnd == true)
            {
                _renderState = RenderState.Stop;
                return;
            }
        }
        Vector3[] path = _renderedPath.Bild(_path, _indexTargetPoint, _renderState, _currentPosition);
        RenderLine(path);
    }

    public void StartPathDrawing(Vector3[] pathPoint)
    {
        int indexPoint = 0;
        if (TrySetPath(pathPoint, indexPoint, pathPoint[indexPoint]))
            _renderState = RenderState.Draw;
    }

    public void InstantlyDrawPath(Vector3[] path)
    {
        int lastIndexOffset = 1;
        if (TrySetPath(path, path.Length, path[path.Length - lastIndexOffset]))
        {
            RenderedPath renderedPath = new RenderedPath();
            Vector3[] newPath = renderedPath.Bild(_path, _indexTargetPoint, RenderState.Draw, _currentPosition);
            RenderLine(path);
        }
    }

    public void ErasePatch(PlayerMover playerMover, Vector3[] pathPoint)
    {
        int indexPoint = 0;
        _mover = playerMover;
        if (TrySetPath(pathPoint, indexPoint, _mover.Position))
            _renderState = RenderState.Erase;
    }

    public void DeleteAllLines()
    {
        _renderState = RenderState.Stop;
        _path = new Vector3[0];
        RenderLine(_path);
    }

    private bool TrySetPath(Vector3[] path, int indexTargetPoint, Vector3 currentPosition)
    {
        if (path.Length > 0)
        {
            _path = path;
            _indexTargetPoint = indexTargetPoint;
            _currentPosition = currentPosition;
            return true;
        }
        DeleteAllLines();
        return false;
    }

    private void TrySetTargetIndex()
    {
        if (_currentPosition != _path[_indexTargetPoint])
            return;

        if (_indexTargetPoint < _path.Length - 1)
        {
            _indexTargetPoint++;
            return;
        }
        Drawed?.Invoke();
    }

    private void RenderLine(Vector3[] path)
    {
        _lineRenderer.positionCount = path.Length;
        _lineRenderer.SetPositions(path);
    }

    class RenderedPath
    {
        public Vector3[] Bild(Vector3[] path, int indexTargetPoint, RenderState mode, Vector3 currentPosition)
        {
            int indexOffset = 1;
            int startErasungPathIndex = 1;
            int pointsCount = GetValue(path.Length - indexTargetPoint + indexOffset, indexTargetPoint + indexOffset, mode);
            int pathStartIndex = GetValue(indexTargetPoint, 0, mode);
            int newPathStartIndex = GetValue(startErasungPathIndex, 0, mode);
            int indexCurrentPosition = GetValue(0, indexTargetPoint, mode);
            Vector3[] currentPath = GetPoints(pathStartIndex, pointsCount, path, newPathStartIndex);
            currentPath[indexCurrentPosition] = currentPosition;
            return currentPath;
        }

        private int GetValue(int erasingValue, int drawingValue, RenderState mode)
        {
            if (mode == RenderState.Erase) return erasingValue;
            if (mode == RenderState.Draw) return drawingValue;
            throw new ArgumentException(mode.ToString());
        }

        private Vector3[] GetPoints(int defaultPathStartIndex, int pointsCount, Vector3[] defaultPath, int newPathStartIndex = 0)
        {
            Vector3[] points = new Vector3[pointsCount];
            for (int i = defaultPathStartIndex; i < defaultPath.Length; i++)
            {
                points[newPathStartIndex] = defaultPath[i];
                newPathStartIndex++;
                if (newPathStartIndex >= pointsCount)
                    break;
            }
            return points;
        }
    }

    enum RenderState
    {
        Stop,
        Draw,
        Erase
    }
}