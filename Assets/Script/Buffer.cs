using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JSNodeMap;
using System;

public class Buffer : MonoBehaviour
{
 
    //Variables for Buffer
    [SerializeField] private Node node;
    [SerializeField] private int bufferLimit;
    List<Agent> agents = new List<Agent>();
    [SerializeField] private Image imagePrefab;
    [SerializeField] private Transform imageParent;
    private Image[] images = new Image[0];
    public float agentStopDuration;
    public Color defaultImageColor;
    public AgentType[] agentTypes = new AgentType[0];
    public Dictionary<string, AgentType> agentTypesDict = new Dictionary<string, AgentType>();




    void Start(){
        images = new Image[bufferLimit];
        for (int i = 0; i < bufferLimit; i++)
        {
        images[i]= Instantiate(imagePrefab,imageParent);
        }
        for (int i = 0; i < agentTypes.Length; i ++)
        {
            AgentType agentType = agentTypes[i];
            agentTypesDict.Add(agentType.name, agentType);
        }
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Packet")
        {
            Agent agent=other.GetComponent<Agent>();
            if(agents.Count +1 < bufferLimit)
            {
                agents.Add(agent);
                agent.Pause();
                Image image = images[agents.Count-1];
                image.color = agent.GetComponentInChildren<MeshRenderer>().material.color;
                StartCoroutine(AgentStopRoutine (agent));
            }
            else
            {
                // string goName= other.gameObject.name;
                // if (!goName.Contains("Video"))
                // {

                // }
                // Node goSource = other.gameObject.GetComponent<DragDrop>().destination;
                // SendNack(goName, goSource);
                Destroy(other.gameObject);
                DragDrop dragDrop = other.GetComponent<DragDrop>();
                Instantiate(agentTypesDict[other.name].agentPrefab, dragDrop.spawnLocationTrs.position, Quaternion.identity);
            }
        }
    }

    IEnumerator AgentStopRoutine (Agent agent)
    {
        yield return new WaitForSeconds(agentStopDuration);
        Image image = images[agents.Count - 1];
        image.color = defaultImageColor;
        agent.Play();
        agents.Remove(agent);
    }
    
    // private void SendNack(string goName, Node goSource)
    // {
    //     var agent = map.CreateAgent();
    //     agent.name = goName;
    //     agent.tag = "NACK";
    //     agent.MoveToTarget(goSource);

    // }
   
   [Serializable]
   public struct AgentType
   {
       public GameObject agentPrefab;
       public string name;
   }
}
