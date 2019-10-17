using Liminal.SDK.VR;
using Liminal.SDK.VR.Input;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInputExample : MonoBehaviour
{
    public Text InputText; 

    private void Update()
    {
        var device = VRDevice.Device;
        if (device != null && device.PrimaryInputDevice != null)
        {
            StringBuilder inputStringBuilder = new StringBuilder("");
            inputStringBuilder.AppendFormat("Primary Button: {0} \n", device.PrimaryInputDevice.GetButton(VRButton.Primary));
            inputStringBuilder.AppendFormat("Touching: {0} \n", device.PrimaryInputDevice.IsTouching);
            inputStringBuilder.AppendFormat("Axis2D-One: {0} \n", device.PrimaryInputDevice.GetAxis2D(VRAxis.One));
            inputStringBuilder.AppendFormat("Axis2D-OneRaw: {0}", device.PrimaryInputDevice.GetAxis2D(VRAxis.OneRaw));
            InputText.text = inputStringBuilder.ToString();
        }
    }
}
