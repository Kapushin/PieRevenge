using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    private AudioClip[] _stepGround, _stepWater, _stepWood, _stepRock;
    private AudioClip _stepClip;
    private AudioClip[] _jumpGround, _jumpWater, _jumpWood, _jumpRock;
    private AudioClip _jumpClip;
    private AudioClip[] _dashes;
    private AudioClip _dashClip;
    private AudioClip[] _hurts;
    private AudioClip _hurtClip;
    private AudioClip[] _slashes;
    private AudioClip _slashClip;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Animator _animator;
    private float _lowPitch = 0.7f;

    // ����� ����� �� ����� ������� ���� ������������
    private CharacterControl _charactercontrol;

    // �������� ����� ���� � ��������
    private string _mainFolder = "Character", 
        _stepsFolder = "Footsteps", _jumpsFolder = "Jumps", _dashesFolder = "Dashes", _hurtesFolder = "Hurt",
        _slashesFolder = "Sword Slashes",
        _groundFolder = "Ground", _waterFolder = "Water", _woodFolder = "Wood", _rockFolder = "Rock";

    private void Start()
    {
        _charactercontrol = GetComponent<CharacterControl>();
        // ��������� �������
        LoadSounds();
    }

    // ��������� �� ���������� ����� ������� � ������, ����� �������� ������ ���������� ����� 
    private void LoadSounds()
    {
        _stepGround = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _stepsFolder + "/" + _groundFolder);
        _stepWater = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _stepsFolder + "/" + _waterFolder);
        _stepWood = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _stepsFolder + "/" + _woodFolder);
        _stepRock = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _stepsFolder + "/" + _rockFolder);

        _jumpGround = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _jumpsFolder + "/" + _groundFolder);
        _jumpWater = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _jumpsFolder + "/" + _waterFolder);
        _jumpWood = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _jumpsFolder + "/" + _woodFolder);
        _jumpRock = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _jumpsFolder + "/" + _rockFolder);

        _dashes = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _dashesFolder);

        _hurts = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _hurtesFolder);

        _slashes = Resources.LoadAll<AudioClip>(_mainFolder + "/" + _slashesFolder);
    }

    // ���� �����, ���������� ������� � ������������ ����� Player1_Run
    private void PlayStepsSound()
    {
        // � ����������� �� ���� �����������, �� ������� ����� �������� (��� ������� �� ������� CharacterControl)
        switch (_charactercontrol.tagSurface)
        {
            // ���� ***, �� � ���� ����� �������� ��������� ������� �� ������� ����� ��������������� �������� ����
            case "Ground":
                _stepClip = _stepGround[UnityEngine.Random.Range(0, _stepGround.Length)];
                break;
            case "Water":
                _stepClip = _stepWater[UnityEngine.Random.Range(0, _stepWater.Length)];
                break;
            case "Wood":
                _stepClip = _stepWood[UnityEngine.Random.Range(0, _stepWood.Length)];
                break;
            case "Rock":
                _stepClip = _stepRock[UnityEngine.Random.Range(0, _stepRock.Length)];
                break;
        }
        // �������� ������ ������� ��������� � �������� ��������� 
        _audioSource.pitch = UnityEngine.Random.Range(_lowPitch, 1f);
        // ��������� ������� 1 ���
        _audioSource.PlayOneShot(_stepClip);
    }

    // ���� ������, ���������� ��� ��������� � ������� �����������
    private void PlayJumpSound()
    {
        // � ����������� �� ���� �����������, �� ������� ����� �������� (��� ������� �� ������� CharacterControl)
        switch (_charactercontrol.tagSurface)
        {
            // ���� ***, �� � ���� ����� �������� ������� �� ����� ��������������� �������� ����
            case "Ground":
                _jumpClip = _jumpGround[0];
                break;
            case "Water":
                _jumpClip = _jumpWater[0];
                break;
            case "Wood":
                _jumpClip = _jumpWood[0];
                break;
            case "Rock":
                _jumpClip = _jumpRock[0];
                break;
        }
        // �������� ������ ������� ��������� � �������� ��������� 
        _audioSource.pitch = UnityEngine.Random.Range(_lowPitch, 1f);
        // ��������� ������� 1 ���
        _audioSource.PlayOneShot(_jumpClip);
        // ��� ��� � ���� ������� ���� ���������� ��������� � �������� �����, ��������� �������� �����������
        //_animator.Play("Player1_Landing");
        _animator.SetTrigger("IsLanding");
    }

    // ���� ����, ���������� �� ������� CharacterControl
    public void PlayDashSound()
    {
        _dashClip = _dashes[0];
        // ��������� ������� 1 ���
        _audioSource.PlayOneShot(_dashClip);
    }

    // ���� ��������� �����, ���������� ������� � ������������ ����� Player1_Hurt
    private void PlayHurtSound()
    {
        _hurtClip = _hurts[0];
        // ��������� ������� 1 ���
        _audioSource.PlayOneShot(_hurtClip);
    }

    private void PlaySlashesSound()
    {
        _slashClip = _slashes[UnityEngine.Random.Range(0, _slashes.Length)];
        _audioSource.pitch = UnityEngine.Random.Range(_lowPitch, 1f);
        _audioSource.PlayOneShot(_slashClip);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ����� ���������� ���� ������
        PlayJumpSound();
    }
}
