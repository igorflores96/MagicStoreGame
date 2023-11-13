using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IScalable
{
    public void Scale(float scalingValue);
    public bool IsScalingUp { get; set; }
    public bool IsScalingDown { get; set; }
}
