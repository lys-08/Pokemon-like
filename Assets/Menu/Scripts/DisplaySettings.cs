using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySettings : MonoBehaviour
{
    private List<string> screenMode_ = new List<string> {"Fullscreen", "Windowed", "Borderless fullscreen"};
    
    
    public void SetScreenMode(int index)
    {
        var mode = screenMode_[index];
        
        Screen.fullScreenMode = mode switch
        {
            "Windowed" => FullScreenMode.Windowed,
            "Fullscreen" => FullScreenMode.ExclusiveFullScreen,
            _ => FullScreenMode.FullScreenWindow
        };

    }
}
