using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
class Stat
{

    /// A reference to the bar that this stat is controlling

    [SerializeField]
    private BarScript bar;


    /// The max value of the stat

    [SerializeField]
    private float maxVal;


    /// The current value of the stat

    [SerializeField]
    private float currentVal;


    /// A Property for accessing and setting the current value

    public float CurrentValue
    {
        get
        {
            return currentVal;
        }
        set
        {
            //Clamps the current value between 0 and max
            this.currentVal = Mathf.Clamp(value, 0, MaxVal);

            //Updates the bar
            bar.Value = currentVal;
        }
    }

    /// A proprty for accessing the max value
    public float MaxVal
    {
        get
        {
            return maxVal;
        }
        set
        {
            //Updates the bar's max value
            bar.MaxValue = value;

            //Sets the max value
            this.maxVal = value;
        }
    }
    public BarScript Bar
    {
        get
        {
            return bar;
        }
    }

    /// Initializes the stat
    /// This function needs to be called in awake
    public void Initialize()
    {
        //Updates the bar
        this.MaxVal = maxVal;
        this.CurrentValue = currentVal;
    }
}

