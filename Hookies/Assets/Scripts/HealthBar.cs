using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour { 

public Image ImgHealthBar;
public Text TxtHealth;
public int Min;
public int Max;
private int mCurentValue;
private float mCurrentPercent;

public void SetHealth(int health)
{
    if (health != mCurentValue)
    {
        if (Max - Min == 0)
        {
            mCurentValue = 0;
            mCurrentPercent = 0;
        }
        else
        {
            mCurentValue = health;
            mCurrentPercent = (float)mCurentValue / (float)(Max - Min);
        }
    }

    TxtHealth.text = string.Format("{0} %", Mathf.RoundToInt(mCurrentPercent * 100));
    ImgHealthBar.fillAmount = mCurrentPercent;
}

public float CurrentPercent
{
    get
    {
        return mCurrentPercent;
    }
}

public int CurrentValue
{
    get
    {
        return mCurentValue;
    }
}

    // Start is called before the first frame update
    void Start()
{
        
}

}
