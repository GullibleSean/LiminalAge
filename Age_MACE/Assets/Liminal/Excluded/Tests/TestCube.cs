using UnityEngine;
using UnityEngine.EventSystems;

public class TestCube : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TestUnityEvent m_Event = new TestUnityEvent();

    public void OnPointerClick(PointerEventData eventData)
    {
        m_Event.Invoke(5678, 9876);
    }

    private void Update ()
    {
        transform.Rotate(Vector3.up, 20 * Time.smoothDeltaTime);
	}
}
