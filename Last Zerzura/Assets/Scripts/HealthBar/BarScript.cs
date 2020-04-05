using System;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour
{
    /// Indicates if this bar will change color
    [SerializeField]
    private bool lerpColors;

    /// A reference to the content of the bar(the colored bar)
    [SerializeField]
    private Image content;

    /// A reference to the text on the bar for example. Health: 100
    [SerializeField]
    private Text valueText;

    /// This movement speed of the bar
    [SerializeField]
    private float lerpSpeed;

    /// The current fill amount of the bar

    private float fillAmount;


    /// The color that the bar will have when it is full
    /// This is only in use if lerpColors is enabled

    [SerializeField]
    private Color fullColor;


    /// The color that the bar will have when it is low
    /// This is only in use if lerpColors is enabled

    [SerializeField]
    private Color lowColor;


    /// Inidcates the max value of the bar, this can reflect the player's max health etc.

    public float MaxValue { get; set; }


    /// A property for setting a the bar's value
    /// It makes sure to update the text on the bar and generete a new fill amount.

    public float Value
    {
        set
        {
            fillAmount = Map(value, 0, MaxValue, 0, 1);
        }
    }

    void Start()
    {
        if (lerpColors) //Sets the standard color
        {
            content.color = fullColor;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        //Makes sure that handle bar is called.
        HandleBar();

    }


    /// Updates the bar

    private void HandleBar()
    {
        if (fillAmount != content.fillAmount) //If we have a new fill amount then we know that we need to update the bar
        {
            //Lerps the fill amount so that we get a smooth movement
            content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);

            if (lerpColors) //If we need to lerp our colors
            {   
                //Lerp the color from full to low
                content.color = Color.Lerp(lowColor, fullColor, fillAmount);
            }
           
        }
    }
    public void Reset()
    {
        content.fillAmount = 1;
        Value = MaxValue;
    }
    /// <summary>
    /// This method maps a range of number into another range
    /// </summary>
    /// <param name="value">The value to evaluate</param>
    /// <param name="inMin">The minimum value of the evaluated variable</param>
    /// <param name="inMax">The maximum value of the evaluated variable</param>
    /// <param name="outMin">The minum number we want to map to</param>
    /// <param name="outMax">The maximum number we want to map to</param>
    /// <returns></returns>
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
