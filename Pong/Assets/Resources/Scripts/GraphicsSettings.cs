using UnityEngine;
using UnityEngine.UI;

public class GraphicsSettings : MonoBehaviour
{
    [SerializeField]
    private Toggle _vsyncToggle;

    private void Start()
    {
        Application.targetFrameRate = 300; //Limit the framerate internally to 300.
        if(PlayerPrefs.HasKey("VsyncCount"))
        {
            QualitySettings.vSyncCount = PlayerPrefs.GetInt("VsyncCount");

            if(QualitySettings.vSyncCount == 1)
            {
                _vsyncToggle.isOn = true;
            } else
            {
                _vsyncToggle.isOn = false;
            }
        }
    }

    public void ToggleVsync(bool newVsyncBool)
    {
        if(newVsyncBool)
        {
            QualitySettings.vSyncCount = 1;
            
        } else
        {
            QualitySettings.vSyncCount = 0;            
        }

        PlayerPrefs.SetInt("VsyncCount", QualitySettings.vSyncCount);
    }
}
