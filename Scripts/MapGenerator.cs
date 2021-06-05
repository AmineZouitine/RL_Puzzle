using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[DisallowMultipleComponent]
public class MapGenerator : MonoBehaviour
{
    #region Inspector Variables




    [Header("Map Information")]
    [SerializeField, Range(1, 100)] private int m_minMapSize;
    [SerializeField, Range(1, 100)] private int m_maxMapSize;
    [SerializeField, Range(0, 0.2f)] private float m_minObstacleFactor;
    [SerializeField, Range(0, 0.2f)] private float m_maxObstacleFactor;
    [SerializeField, Range(1, 10)] private float m_objectSize;
    [SerializeField, Range(0, 5)] private float m_distanceBetweenTiles;

    [Header("Prefabs")]
    [SerializeField] private GameObject m_tilePrefab;
    [SerializeField] private GameObject m_agentPrefab;
    [SerializeField] private GameObject m_obstaclePrefab;
    [SerializeField] private GameObject m_victoryPrefab;

    [Header("Pos")]
    [SerializeField] private Transform m_spawnpos;

    [Header("Transform Content")]
    [SerializeField] private Transform m_tileContent;
    [SerializeField] private Transform m_obstacleContent;
    [SerializeField] private Transform m_agentContent;
    [SerializeField] private Transform m_victoryContent;
    #endregion


    public List<GameObject> m_tiles { get; private set; }
    #region Private Variables

    private Vector2Int m_mapSize;
    private Vector3 m_startPos;
    private List<Position> disponibleTilesPos = new List<Position>();
    private List<GameObject> allObjects = new List<GameObject>();
    private Position m_vicoryPoint1;
    private Position m_vicoryPoint2;
    private Map m_map;

    #endregion

    private void Awake()
    {
        m_map = GetComponent<Map>();
        m_tiles = new List<GameObject>();
    }


    public void Generate()
    {
        Clear();
        SetInitialReferences();
        SpawnVictoryRandom();
        GenerateTiles(m_mapSize.x, m_mapSize.y);
        SpawnObstaclesRandom();
        SpawPlayersRandom();
        m_map.m_spawnPos = m_spawnpos.position;
        m_map.m_mapSize = m_mapSize;
        m_map.m_distanceBetweenTiles = m_distanceBetweenTiles;
    }
    public void Clear()
    {
        ClearAllObjects();
        m_map.m_obstacles.Clear();
        disponibleTilesPos.Clear();
        m_startPos = m_spawnpos.position;
    }



    private void SetInitialReferences()
    {

        int xMapsize = Random.Range(m_minMapSize, m_maxMapSize + 1);
        int yMapsize = Random.Range(m_minMapSize, m_maxMapSize + 1);
        m_mapSize = new Vector2Int(xMapsize, yMapsize);
        GetDisponiblesPosRandom();
    }


    private void ClearAllObjects()
    {
        foreach (GameObject obj in allObjects.ToList())
        {
            Destroy(obj);
        }
        allObjects.Clear();
    }

    private void SpawnVictoryRandom()
    {

        m_vicoryPoint1 = GetRandomPosition();
        m_vicoryPoint2 = GetRandomPosition();
        GameObject currentVictoryPoint1 = Spawn(m_victoryPrefab, m_vicoryPoint1, Vector3.zero);
        GameObject currentVictoryPoint2 = Spawn(m_victoryPrefab, m_vicoryPoint2, Vector3.zero);
        currentVictoryPoint1.transform.parent = m_victoryContent;
        currentVictoryPoint2.transform.parent = m_victoryContent;

        ObjectPosition[] victory = new ObjectPosition[] { new ObjectPosition(m_vicoryPoint1, currentVictoryPoint1), new ObjectPosition(m_vicoryPoint2, currentVictoryPoint2) };
        m_map.m_victoryPoints = victory;
        allObjects.Add(currentVictoryPoint1);
        allObjects.Add(currentVictoryPoint2);
    }

    private void SpawPlayersRandom()
    {
        Position player1Pos = GetRandomPosition();
        Position player2Pos = GetRandomPosition();
        GameObject currentPlayer1 = Spawn(m_agentPrefab, player1Pos, new Vector3(0, 0.2f, 0));
        GameObject currentPlayer2 = Spawn(m_agentPrefab, player2Pos, new Vector3(0, 0.2f, 0));
        currentPlayer1.transform.parent = m_agentContent;
        currentPlayer2.transform.parent = m_agentContent;
        ObjectPosition[] agents = new ObjectPosition[] { new ObjectPosition(player1Pos, currentPlayer1), new ObjectPosition(player2Pos, currentPlayer2) };
        m_map.m_agents = agents;
        allObjects.Add(currentPlayer1);
        allObjects.Add(currentPlayer2);
    }
    private void SpawnObstaclesRandom()
    {
        float randomObstacleFactor = Random.Range(m_minObstacleFactor, m_maxObstacleFactor);
        int obstacleNumber = Mathf.Clamp((int)((m_mapSize.x * m_mapSize.y) * randomObstacleFactor),0,9) +1;
        for (int i = 0; i < obstacleNumber; i++)
        {

            Vector3 offSetObstacle = Vector3.up/3.5f;
            Position currentPos = GetRandomPosition();
            GameObject currentObstacle = Spawn(m_obstaclePrefab, currentPos, offSetObstacle);
            currentObstacle.transform.parent = m_obstacleContent;
            m_map.m_obstacles.Add(new ObjectPosition(currentPos, currentObstacle));
            allObjects.Add(currentObstacle);
        }
    }


    public void GenerateTiles(int _row, int _column)
    {
        Vector3 posToSpawn = m_startPos;
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _column; j++)
            {
                if (!(new Position(i, j).Equals(m_vicoryPoint1)) && !(new Position(i, j).Equals(m_vicoryPoint2)))
                {
                    GameObject currentTileSpawn = Instantiate(m_tilePrefab, posToSpawn, Quaternion.identity);
                    currentTileSpawn.transform.parent = m_tileContent;
                    currentTileSpawn.name = $"Tile[{i},{j}]";
                    allObjects.Add(currentTileSpawn);
                }
                posToSpawn += new Vector3(0, 0, m_objectSize + m_distanceBetweenTiles);
            }
            posToSpawn = m_startPos + new Vector3(m_objectSize * (i + 1) + m_distanceBetweenTiles * (i+1), 0, 0);
        }
    }
   


    private GameObject Spawn(GameObject _gameobject, Position _pos, Vector3 _offSet)
    {
        Vector3 posToSpawn = new Vector3(m_startPos.x + (m_objectSize+m_distanceBetweenTiles) * _pos.x, m_startPos.y, m_startPos.z + (m_objectSize + m_distanceBetweenTiles) * _pos.y) + _offSet;
        GameObject currentObject = Instantiate(_gameobject, posToSpawn, Quaternion.identity);
        allObjects.Add(currentObject);
        return currentObject;
    }

    private void GetDisponiblesPosRandom()
    {
        for (int i = 0; i < m_mapSize.x; i++)
        {
            for (int j = 0; j < m_mapSize.y; j++)
            {
                disponibleTilesPos.Add(new Position(i, j));
            }
        }
        Shuffle();
    }
    private void Shuffle()
    {
        Position myPos;
        System.Random _random = new System.Random();
        int n = disponibleTilesPos.Count;
        for (int i = 0; i < n; i++)
        {
            int r = i + (int)(_random.NextDouble() * (n - i));
            myPos = disponibleTilesPos[r];
            disponibleTilesPos[r] = disponibleTilesPos[i];
            disponibleTilesPos[i] = myPos;
        }
    }
    private Position GetRandomPosition()
    {
        int RandomIndex = Random.Range(0, disponibleTilesPos.Count);
        Position elemTemp = disponibleTilesPos[RandomIndex];
        Position elem = new Position(elemTemp.x, elemTemp.y);
        disponibleTilesPos.Remove(elemTemp);
        return elem;
    }



}
