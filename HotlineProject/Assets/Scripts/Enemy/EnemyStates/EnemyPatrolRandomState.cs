using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolRandomState : IState
{

    Enemy _enemy;
    UnityEngine.AI.NavMeshAgent agent;
    FSM<EnemyStates> _fsm;
    Vector3 newPosition;
    bool isPatrolling;


    public EnemyPatrolRandomState(FSM<EnemyStates> fsm, Enemy enemy)
    {
        _enemy = enemy;

        _fsm = fsm;
        agent = _enemy.agent;

    }
    public void OnEnter()
    {
        newPosition = NewPosition();
        if(Vector2.Distance(_enemy.transform.position, _enemy.originPosition) > 20f)
            _fsm.ChangeState(EnemyStates.Return);
        isPatrolling = false;
        //Debug.Log(newPosition);
    
    }

    public void OnUpdate()
    {
        if(Vector2.Distance(_enemy.transform.position, newPosition) < _enemy.patrolMinRadius && !isPatrolling)
        {
            newPosition = NewPosition();
            //Debug.Log(newPosition);
        }
        else if(newPosition.x < -49.5f || newPosition.x > 57.5f || newPosition.y < -28f || newPosition.y > 51f)
        {
            newPosition = NewPosition();
        }
        else
        {
            isPatrolling = true;
            _enemy._view.Rotate(newPosition);
            agent.SetDestination(new Vector3(newPosition.x, newPosition.y, _enemy.transform.position.z));
            Quaternion rotTarget = Quaternion.LookRotation(newPosition - _enemy.transform.position);
            _enemy.transform.rotation = Quaternion.RotateTowards(_enemy.transform.rotation, rotTarget, _enemy.rotationSpeed * Time.deltaTime);
        }
    }

    public void OnFixedUpdate()
    {
        if(Vector2.Distance(_enemy.transform.position, agent.destination) < 1 && isPatrolling)
        {
            _fsm.ChangeState(EnemyStates.Idle);
        }
        else
        {
            if(_enemy.CheckEnemiesInRange())
                _fsm.ChangeState(EnemyStates.Attack);
        }
    }

    public void OnExit()
    {

    }

    private Vector3 NewPosition()
    {
        return new Vector3(Random.Range(_enemy.transform.position.x - _enemy.patrolMaxRadius, _enemy.transform.position.x + _enemy.patrolMaxRadius), Random.Range(_enemy.transform.position.y - _enemy.patrolMaxRadius, _enemy.transform.position.y + _enemy.patrolMaxRadius), _enemy.transform.position.z);
    }
}
