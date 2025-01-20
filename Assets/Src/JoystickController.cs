using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Image background; // ��� ���������
    public Image handle;     // ����� ���������
    public Vector2 inputVector; // ��������������� ������ �����

    private float radius; // ������ ����� (��� ���������)

    void Start()
    {
        // ��������� ������ ��� �������� ������ ��� ������ ���� (� ����������� �� �����������)
        radius = background.rectTransform.sizeDelta.x / 2f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // ������������ ������� ��� ��������������
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ����������� �������� ���������� � ��������� ������������ ���� ���������
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background.rectTransform, // RectTransform ����
            eventData.position, // ������� ����
            eventData.pressEventCamera, // ������ UI
            out localPosition
        );

      
       

        // ������������ �������� ������ ����� ��������
        if (localPosition.magnitude > radius)
        {
            localPosition = localPosition.normalized * radius; // ������������ �� �������
        }

        // ����������� ������ �����
        inputVector = localPosition / radius;

        // ���������� �����
        handle.rectTransform.localPosition = localPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // ���������� ������ ����� � ���������� ����� � �����
        inputVector = Vector2.zero;
        handle.rectTransform.localPosition = Vector2.zero;
    }

    // ������� �������� ��� ��������� �����
    public float Horizontal => inputVector.x;
    public float Vertical => inputVector.y;
}