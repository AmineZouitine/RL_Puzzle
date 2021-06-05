using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using System.Linq;

public class RobotAgent : Agent
{

    [SerializeField] private MapGenerator m_mapGenerator;
    [SerializeField] private Map m_map;

    public override void Initialize()
    {
        base.Initialize();
    }
    public override void OnEpisodeBegin()
    {
        m_map.Clear();
        m_mapGenerator.Generate();
    }
    public override void OnActionReceived(float[] vectorAction)
    {

        
        m_map.GetNextPositionInstante((int)vectorAction[0]);
        if (m_map.IsVictory())
            Finish();  
        else
            AddReward(0f);

    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(m_map.m_mapSize.x); 
        sensor.AddObservation(m_map.m_mapSize.y); 
        sensor.AddObservation(m_map.m_agents[0].position.x); 
        sensor.AddObservation(m_map.m_agents[0].position.y); 
        sensor.AddObservation(m_map.m_agents[1].position.x);
        sensor.AddObservation(m_map.m_agents[1].position.y);
        sensor.AddObservation(m_map.m_victoryPoints[0].position.x);
        sensor.AddObservation(m_map.m_victoryPoints[0].position.y);
        sensor.AddObservation(m_map.m_victoryPoints[1].position.x);
        sensor.AddObservation(m_map.m_victoryPoints[1].position.y);
        for (int i = 0; i < m_map.m_obstacles.Count; i++)
        {
            sensor.AddObservation(m_map.m_obstacles[i].position.x);
            sensor.AddObservation(m_map.m_obstacles[i].position.y);
        }
        for (int i = 0; i < 6 - m_map.m_obstacles.Count; i++)
        {
            sensor.AddObservation(-1);
            sensor.AddObservation(-1);
        }
    }
    public void Finish()
    {
        AddReward(1.0f);
        EndEpisode();
    }




}
