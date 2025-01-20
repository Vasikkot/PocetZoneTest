using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Image background; // Фон джойстика
    public Image handle;     // Ручка джойстика
    public Vector2 inputVector; // Нормализованный вектор ввода

    private float radius; // Радиус круга (фон джойстика)

    void Start()
    {
        // Вычисляем радиус как половину ширины или высоты фона (в зависимости от соотношения)
        radius = background.rectTransform.sizeDelta.x / 2f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData); // Обрабатываем нажатие как перетаскивание
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Преобразуем экранные координаты в локальные относительно фона джойстика
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            background.rectTransform, // RectTransform фона
            eventData.position, // Позиция мыши
            eventData.pressEventCamera, // Камера UI
            out localPosition
        );

      
       

        // Ограничиваем движение внутри круга радиусом
        if (localPosition.magnitude > radius)
        {
            localPosition = localPosition.normalized * radius; // Ограничиваем по радиусу
        }

        // Нормализуем вектор ввода
        inputVector = localPosition / radius;

        // Перемещаем ручку
        handle.rectTransform.localPosition = localPosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Сбрасываем вектор ввода и возвращаем ручку в центр
        inputVector = Vector2.zero;
        handle.rectTransform.localPosition = Vector2.zero;
    }

    // Удобные свойства для получения ввода
    public float Horizontal => inputVector.x;
    public float Vertical => inputVector.y;
}