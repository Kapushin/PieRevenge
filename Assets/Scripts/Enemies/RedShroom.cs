using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedShroom : EnemyBehavior
{
    [SerializeField] private GameObject _sporeAttackParticles;
    [SerializeField] private GameObject _attackingZone;

    public override void Move()
    {
        base.Move();

        // ����� ��������� �� ����� �� ������ ������ ������ ��������� ��������, ���� ������� �� �������
        if (Vector2.Distance(transform.position, _target.position) < _rangeToChasing &&
            Vector2.Distance(transform.position, _target.position) > _rangeToSrandingNearPlayer)
        {
            Motion(_target);
        }

        // ���� ���������� �� ������ ������ ��������� ��������,
        // �� ���� �������. �������, ����� ���� �� ������ �� ������.
        else if (Vector2.Distance(transform.position, _target.position) < _rangeToSrandingNearPlayer)
        {
            Attack();
            _moveDistance = new Vector2(0f, 0f);
        }

        _rb.velocity = new Vector2(_moveDistance.x, 0f);
    }

    // �����
    private IEnumerator SporeAttackCoroutine()
    {
        _animator.Play("Attack Red Shroom");
        _rb.velocity = new Vector2(0f, 0f);
        _canAttack = false;
        _isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        // �� �������� ��-�� ������ ��������, ���������� ������ ����� ������, ����� ���� �� ���������
        _rb.WakeUp();
        _sporeAttackParticles.SetActive(true);
        // ��������� ���� ����� ���������� �� ����� ����, ��� ����������� �����.
        _attackingZone.SetActive(true);
        yield return new WaitForSeconds(_attackTime);
        _isAttacking = false;
        _sporeAttackParticles.SetActive(false);
        _attackingZone.SetActive(false);
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    // ���������� �����
    public override void Attack()
    {
        StartCoroutine(SporeAttackCoroutine());
    }

    public override void Dead()
    {
        base.Dead();
        _rb.velocity = new Vector2(0f, 0f);
    }
}
