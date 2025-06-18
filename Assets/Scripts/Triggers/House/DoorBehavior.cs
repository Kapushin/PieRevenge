using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour, IInteracteable
{
    [SerializeField] private AudioSource _doorOpenedSound;
    private GameObject _mainCamera;
    // �������� �������� ������ (�������� ������� �������, � ������ ������ 18f).
    [SerializeField] private float _camOffset;
    private Vector3 _direction;
    private float _camMove = 0;

    [SerializeField] private GameObject _newPosition;
    private GameObject _character;

    [SerializeField] private string _textPrompt;

    public string PromptText => _textPrompt;

    private void Start()
    {
        _character = GameObject.FindGameObjectWithTag("Player");
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        _direction = transform.localScale;
    }

    private void FixedUpdate()
    {
        CameraOffset();
    }

    // ���������� ���������� �������������� � ���������
    public bool GetInteracted(InteractionsBehaviour target)
    {
        DoorInteraction();
        EnterPosition();
        return true;
    }

    private void DoorInteraction()
    {
        // ���� �������� ��������������� � ������, ��
        // ��������� ���� �������� �����
        _doorOpenedSound.Play();
        // ���� ����������� ��������� ������, �������� �������� _camMove
        _camMove = _camOffset;
    }

    // �������� ������ �� ����� �������, ���� ����� ��������
    private void CameraOffset()
    {
        if (_camMove > 0f)
        {
            _mainCamera.transform.position = new Vector3(_mainCamera.transform.position.x + (.5f * _direction.x),
                _mainCamera.transform.position.y, _mainCamera.transform.position.z);
            _camMove -= .5f;
        }
    }

    // ����������� ��������� � �������, � ������� �� �����
    private void EnterPosition()
    {
        _character.transform.position = new Vector3(_newPosition.transform.position.x, _newPosition.transform.position.y,
            _newPosition.transform.position.z);
    }
}
