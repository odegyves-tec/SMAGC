using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerComunicationExample : MonoBehaviour
{
    [Serializable]
    struct Agent
    {
        public Vector3 position;
        public Vector3 rotation;
    };

    [Serializable]
    struct SimulationData
    {
        public List<Agent> agents;
    };


    private IEnumerator Start()
    {
        yield return InitializeSimulation();

        yield return GetAgentsData();
    }

    private IEnumerator InitializeSimulation()
    {
        string url = "http://127.0.0.1:5000/init";
        string postData = "{ \"agent_count\":" + 8 + " }";
        string contentType = "application/json";

        using (UnityWebRequest postRequest = UnityWebRequest.Post(url, postData, contentType))
        {
            yield return postRequest.SendWebRequest();

            if (postRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Simulation was initialized");
            }
            else
            {
                Debug.Log("There was an error comunicating with the server");
            }
        }
    }

    private IEnumerator GetAgentsData()
    {
        string url = "http://127.0.0.1:5000/get-agents-data";

        using (UnityWebRequest getRequest = UnityWebRequest.Get(url))
        {
            yield return getRequest.SendWebRequest();

            if (getRequest.result == UnityWebRequest.Result.Success)
            {
                string response = getRequest.downloadHandler.text;
                Debug.Log(response);
                SimulationData simulationData = JsonUtility.FromJson<SimulationData>(response);
                Debug.Log("Agent count: " + simulationData.agents.Count);
                Debug.Log("Agent 0 position: " + simulationData.agents[0].position);
            }
            else
            {
                Debug.Log("There was an error with the server comunication");
            }
        }
    }
}
