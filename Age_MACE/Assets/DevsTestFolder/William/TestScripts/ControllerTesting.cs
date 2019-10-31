using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ControllerTesting : MonoBehaviour
{
    public Text GazeText;
    public Text TouchText;

    public void OnClick()
    {
        var device = VRDevice.Device;
        if (device.PrimaryInputDevice.GetButton(VRButton.Primary))
        {
            TouchText.text = "Touched: T";
        }
        else
        {
            GazeText.text = "Gazed: T";
        }
    }
}
