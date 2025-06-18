using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionsBehaviour : MonoBehaviour
{
    private IInteracteable _target;
    private Transform _targetPosition;
    [SerializeField] private Transform _interactCheck;
    [SerializeField] private LayerMask _interactLayer;
    [SerializeField] private float _radiusInteract;
    private Collider2D[] _colliders = new Collider2D[3];
    private int _interactFound;

    [SerializeField] private GameObject _popUpBubble;
    [SerializeField] private TextMeshProUGUI _textPrompt;

    protected GameObject _canvas;
    protected InkManager _ink;

    private void Start()
    {
        EventManager.OnEndDialog += DisableNPC;
        _canvas = GameObject.Find("Canvas Dialogs");
        _ink = _canvas.gameObject.GetComponent<InkManager>();
    }

    private void OnDisable()
    {
        EventManager.OnEndDialog -= DisableNPC;
    }

    private void Update()
    {
        // ������� �������� � ���������� ����� ��������� � ������ ������
        _interactFound = Physics2D.OverlapCircleNonAlloc(_interactCheck.position, _radiusInteract, _colliders, _interactLayer);

        Interaction();
    }

    private void DisableNPC()
    {
        if (_colliders[0].GetComponent<NPCBehavior>()._isDisableable)
        {
            Destroy(_colliders[0].gameObject);
        }
    }

    // ���� �������������� � ��������� ��������
    private void Interaction()
    {
        // ���� ������� � ���� �������������� ������ 0
        if (_interactFound > 0)
        {
            // �� ��������� �� ������, � �������� ���� ���� ���������
            _target = _colliders[0].GetComponent<IInteracteable>();
            _targetPosition = _colliders[0].GetComponent<Transform>();

            // ��������� ������� ������� � ����
            if (_target != null)
            { 
                // �������� ���� ������� �� �������������� �� ������
                if (_colliders[0].CompareTag("Interact"))
                {
                    // ������� ���� � ���������� ��� ��������
                    _popUpBubble.SetActive(true);
                    _textPrompt.text = _target.PromptText;

                    if (_colliders[0].transform.GetChild(0))
                    {
                        Transform child = _colliders[0].transform.GetChild(0);
                        if (child.GetComponent<SpriteRenderer>())
                        {
                            float size = child.GetComponent<SpriteRenderer>().bounds.size.y;
                            _popUpBubble.transform.position = new Vector3(_targetPosition.transform.position.x, _targetPosition.transform.position.y + size,
                                _targetPosition.transform.position.z);
                        }
                    }

                    if (Input.GetButtonDown("Interact") && !_ink.BlockInteractions)
                    {
                        // ��������� ���������� ���������� ��������������
                        _target.GetInteracted(this);
                        // ������� ����
                        ClosePopUp();
                    }
                }
                // �������� ���� ������� �� �� ��� ��� ����� ������ ���������
                if (_colliders[0].CompareTag("CanCollect"))
                {
                    // ��������� ���������� ����������
                    _target.GetInteracted(this);
                }
            }
        }
        else
        {
            // ���� ������ ��� ��� �� ����
            if (_target != null)
            {
                // ������� ����
                ClosePopUp();
                _target = null;
            }
        }
    }

    private void ClosePopUp()
    {
        //_target.PopUpPrompt(false);
        _popUpBubble.SetActive(false);
    }
}
