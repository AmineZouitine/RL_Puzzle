using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPosition 
{
  
    public Position position { get; set; }
    public GameObject obj { get; set; }
    public ObjectPosition(Position position, GameObject obj)
    {
        this.position = position;
        this.obj = obj;
    }



}
