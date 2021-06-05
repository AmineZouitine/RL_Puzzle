using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Map : MonoBehaviour
{
    [SerializeField, Range(1, 50)] private float m_speed;
    [SerializeField, Range(0, 2)] private float m_pauseTime;


    public Vector3 m_spawnPos { get; set; }
    public Vector2Int m_mapSize { get; set; }
    public List<ObjectPosition> m_obstacles { get; set; }
    public ObjectPosition[] m_agents { get; set; }
    public ObjectPosition[] m_victoryPoints { get; set; }
    public float m_distanceBetweenTiles { get; set; }


    public bool m_canMove
    {
        get { return m_mouvementPlayer1 && m_mouvementPlayer2; }
        private set
        {
            m_mouvementPlayer1 = value;
            m_mouvementPlayer2 = value;
        }
    }

    private bool m_mouvementPlayer1 = true;
    private bool m_mouvementPlayer2 = true;

    public Map()
    {
        m_obstacles = new List<ObjectPosition>();

    }
    public void Clear()
    {
        StopAllCoroutines();
        m_canMove = true;
    }

    public bool IsVictory() => (m_agents[0].position.Equals(m_victoryPoints[0].position) && m_agents[1].position.Equals(m_victoryPoints[1].position)) || (m_agents[1].position.Equals(m_victoryPoints[0].position) && m_agents[0].position.Equals(m_victoryPoints[1].position));



    private bool CheckBorders(Position _pos) => _pos.x < 0 || _pos.x >= m_mapSize.x || _pos.y < 0 || _pos.y >= m_mapSize.y;
    public bool CheckObstacles(Position _pos)
    {
        foreach (ObjectPosition obstacle in m_obstacles)
        {
            if (obstacle.position.Equals(_pos))
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckPlayer(Position _pos) => m_agents[0].position == _pos || m_agents[1].position == _pos;
    public bool CheckVictory(Position _pos) =>  m_victoryPoints[0].position == _pos || m_victoryPoints[1].position == _pos;
    private Position TransformLimitePos(Position _pos)
    {
        if (_pos.x < 0)
        {
            return new Position(m_mapSize.x - 1, _pos.y);
        }
        else if (_pos.x >= m_mapSize.x)
        {
            return new Position(0, _pos.y);
        }
        else if (_pos.y < 0)
        {
            return new Position(_pos.x, m_mapSize.y - 1);
        }
        else if (_pos.y >= m_mapSize.y)
        {
            return new Position(_pos.x, 0);
        }
        return null;
    }
    private Vector3 GetPosition(Position _pos) => new Vector3(m_spawnPos.x + _pos.x + (_pos.x * m_distanceBetweenTiles), 0, m_spawnPos.z + _pos.y + (_pos.y * m_distanceBetweenTiles));
    private (Position, bool) GetNextPos(Position _pos, Direction _direction)
    {
        Position pos = _pos + DirectionToPosition(_direction);
        return CheckBorders(pos) ? (TransformLimitePos(pos), true) : (pos, false);
    }

    private Position DirectionToPosition(Direction _direction)
    {
        switch (_direction)
        {
            case Direction.Top:
                return new Position(1, 0);
            case Direction.Down:
                return new Position(-1, 0);
            case Direction.Right:
                return new Position(0, -1);
            case Direction.Left:
                return new Position(0, 1);
            default:
                return new Position(0, 0);
        }
    }

    private Direction IntToDirection(int _value)
    {
        switch (_value)
        {
            case 0:
                return Direction.Top;
            case 1:
                return Direction.Down;
            case 2:
                return Direction.Left;
            case 3:
                return Direction.Right;
            default:
                return Direction.Top;
        }
    }

    public void GetNextPosition(Direction _direction)
    {
        if (m_mouvementPlayer1 && m_mouvementPlayer2)
        {
            //Test
            float agentHeight = m_spawnPos.y + 0.2f;
            //Test
            ObjectPosition agent1 = m_agents[0];
            ObjectPosition agent2 = m_agents[1];
            (Position nextPos1, bool isTeleport1) = GetNextPos(agent1.position, _direction);
            (Position nextPos2, bool isTeleport2) = GetNextPos(agent2.position, _direction);
            if (CheckObstacles(nextPos1))
            {
                nextPos1 = agent1.position;
            }
            if (CheckObstacles(nextPos2))
            {
                nextPos2 = agent2.position;

            }
            if (nextPos1.Equals(nextPos2))
            {
                nextPos1 = agent1.position;
                nextPos2 = agent2.position;

            }

            m_agents[0].position = nextPos1;
            m_agents[1].position = nextPos2;
            if (!isTeleport1)
                StartCoroutine(MakeMouvement(agent1.obj.transform, GetPosition(nextPos1) + new Vector3(0, agentHeight, 0), 0));
            else
                agent1.obj.transform.position = GetPosition(nextPos1) + new Vector3(0, agentHeight, 0);
            if (!isTeleport2)
                StartCoroutine(MakeMouvement(agent2.obj.transform, GetPosition(nextPos2) + new Vector3(0, agentHeight, 0), 1));
            else
                agent2.obj.transform.position = GetPosition(nextPos2) + new Vector3(0, agentHeight, 0);


        }
    }

    public IEnumerator MakeMouvement(Transform _agent, Vector3 _endPos, int _index)
    {
        if (_index == 0) { m_mouvementPlayer1 = false; }
        else { m_mouvementPlayer2 = false; }
        Vector3 startPos = _agent.transform.position;
        float interpolation = 0;
        while (interpolation < 1)
        {
            interpolation += Time.deltaTime * m_speed;
            _agent.transform.position = Vector3.Lerp(startPos, _endPos, interpolation);
            yield return null;
        }
        if (_index == 0) { m_mouvementPlayer1 = true; }
        else { m_mouvementPlayer2 = true; }
    }

    public void GetNextPositionInstante(int _direction)
    {

        Direction direction = IntToDirection(_direction);
        float agentHeight = m_spawnPos.y + 0.2f;
        ObjectPosition agent1 = m_agents[0];
        ObjectPosition agent2 = m_agents[1];
        (Position nextPos1, bool isTeleport1) = GetNextPos(agent1.position, direction);
        (Position nextPos2, bool isTeleport2) = GetNextPos(agent2.position, direction);
        if (CheckObstacles(nextPos1))
        {
            nextPos1 = agent1.position;
        }
        if (CheckObstacles(nextPos2))
        {
            nextPos2 = agent2.position;

        }
        if (nextPos1.Equals(nextPos2))
        {
            nextPos1 = agent1.position;
            nextPos2 = agent2.position;

        }

        m_agents[0].position = nextPos1;
        m_agents[1].position = nextPos2;
        agent1.obj.transform.position = GetPosition(nextPos1) + new Vector3(0, agentHeight, 0);
        agent2.obj.transform.position = GetPosition(nextPos2) + new Vector3(0, agentHeight, 0);



    }
    public void GetNextPosition(int _direction)
    {
        if (m_mouvementPlayer1 && m_mouvementPlayer2)
        {
            Direction direction = IntToDirection(_direction);
            float agentHeight = m_spawnPos.y + 0.2f;
            ObjectPosition agent1 = m_agents[0];
            ObjectPosition agent2 = m_agents[1];
            (Position nextPos1, bool isTeleport1) = GetNextPos(agent1.position, direction);
            (Position nextPos2, bool isTeleport2) = GetNextPos(agent2.position, direction);
            if (CheckObstacles(nextPos1))
            {
                nextPos1 = agent1.position;
            }
            if (CheckObstacles(nextPos2))
            {
                nextPos2 = agent2.position;

            }
            if (nextPos1.Equals(nextPos2))
            {
                nextPos1 = agent1.position;
                nextPos2 = agent2.position;

            }

            m_agents[0].position = nextPos1;
            m_agents[1].position = nextPos2;
            if (!isTeleport1)
                StartCoroutine(MakeMouvement(agent1.obj.transform, GetPosition(nextPos1) + new Vector3(0, agentHeight, 0), 0));
            else
                agent1.obj.transform.position = GetPosition(nextPos1) + new Vector3(0, agentHeight, 0);
            if (!isTeleport2)
                StartCoroutine(MakeMouvement(agent2.obj.transform, GetPosition(nextPos2) + new Vector3(0, agentHeight, 0), 1));
            else
                agent2.obj.transform.position = GetPosition(nextPos2) + new Vector3(0, agentHeight, 0);


        }
    }


}
