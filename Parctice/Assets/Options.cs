using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{

    //Метод, отвечающий в настройках за полный экран
    public void FullScreenToggle()
    {
        bool isFullScreen = !Screen.fullScreen;
        Screen.fullScreen = isFullScreen;
    }

    // За музыку
    public AudioMixer am;

    public void AudioVolume(float sliderValue)
    {
        am.SetFloat("masterVolume", sliderValue);
    }

    //Настройки графики Edit-ProgectSettings-Quality указываются разные настройки графики в Unity
    public void Quality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }

    //Разрешение
    Resolution[] rsl;
    List<string> resolutions;
    public Dropdown dropdown;

    public void Awake()
    {
        resolutions = new List<string>();
        rsl = Screen.resolutions;
        foreach (var i in rsl)
        {
            resolutions.Add(i.width + "x" + i.height);
        }
        dropdown.ClearOptions();
        dropdown.AddOptions(resolutions);
    }

    public void Resolution(int r)
    {
        if(r == 0)
        {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
        else if (r == 1)
        {
            Screen.SetResolution(1366, 768, Screen.fullScreen);
        }
        else if (r == 2)
        {
            Screen.SetResolution(1024, 768, Screen.fullScreen);
        }
    }
}
