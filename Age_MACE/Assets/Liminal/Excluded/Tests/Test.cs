using Liminal.SDK.Core;
using Liminal.SDK.Extensions;
using Liminal.SDK.VR.Avatars.Controllers;
using Liminal.SDK.VR.Input;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class SomeSerializableData
{
    public float FloatValue;
    public string StringValue;
}

public class Test : MonoBehaviour
{
    [Serializable]
    public class SomeNestedSerializableData
    {
        public bool BooleanValue;
        public string StringValue;
    }

    [SerializeField] private SomeSerializableData m_ExternalData = new SomeSerializableData();
    [SerializeField] private SomeNestedSerializableData m_NestedData = new SomeNestedSerializableData();
    [SerializeField] private TestUnityEvent m_Event = new TestUnityEvent();
    [SerializeField] private GameObject m_Prefab = null;
    [SerializeField] private VRControllerVisualProxy m_TutorialController = null;
    [SerializeField] private GameObject m_TutorialCanvas = null;

    public UnityEvent m_NormalUnityEvent;

    private void Start()
    {
        m_Event.Invoke(5,6);
        m_NormalUnityEvent.Invoke();
    }

    public void PrintData()
    {
        Debug.Log("m_ExternalData.FloatValue=" + m_ExternalData.FloatValue);
        Debug.Log("m_ExternalData.StringValue=" + m_ExternalData.StringValue);
        Debug.Log("m_NestedData.BooleanValue=" + m_NestedData.BooleanValue);
        Debug.Log("m_NestedData.StringValue=" + m_NestedData.StringValue);

        m_Event.Invoke(1234, 4321);
    }

    public void InstantiateSomething() 
    {
        var p = Instantiate(m_Prefab);
        p.transform.position = new Vector3(-2.74f, 1.56f, 6.43f);
    }

    public void EndTheApp()
    {
        ExperienceApp.End();
	}

    public void DoSomething(float a, float b)
    {
        Debug.LogFormat("Event args: a={0}, b={0}", a, b);
    }
}

[Serializable]
public class TestUnityEvent : UnityEvent<float, float>
{

}