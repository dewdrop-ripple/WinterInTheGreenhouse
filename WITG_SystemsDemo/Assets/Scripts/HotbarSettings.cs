using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSettings : MonoBehaviour
{
    [SerializeField] private List<GameObject> hotbarSlots;
    [SerializeField] private GameObject hotbar;
    [SerializeField] private Canvas hudCanvas;

    [SerializeField] private float screenPercentWidth;
    [SerializeField] private float screenPercentBottomGap;
    [SerializeField] private float screenPercentHeight;
    [SerializeField] private float screenPercentDistanceBetweenSlots;

    [SerializeField] private Color baseSlotColor;
    [SerializeField] private Color targetSlotColor;

    private int targetSlot;

    private void Start()
    {
        SetTargetHotbarSlot(0);
    }

    private void Update()
    {
        UpdateHotbarSize();

        ProcessInput();
    }

    private void UpdateHotbarSize()
    {
        // Get canvas size
        float screenWidth = hudCanvas.GetComponent<RectTransform>().sizeDelta.x;
        float screenHeight = hudCanvas.GetComponent<RectTransform>().sizeDelta.y;

        // Calculate hotbar size and position
        int hotbarWidth = (int)(screenWidth * screenPercentWidth);
        int hotbarHeight = (int)(screenHeight * screenPercentHeight);
        int hotbarY = (int)((screenHeight * screenPercentBottomGap) - (screenHeight * 0.5f) + (hotbarHeight * 0.5f));

        // Resize hotbar
        hotbar.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, hotbarY);
        hotbar.GetComponent<RectTransform>().sizeDelta = new Vector2(hotbarWidth, hotbarHeight);

        // Determine slot sizes
        int distanceBetweenSlots = (int)(screenWidth * screenPercentDistanceBetweenSlots);
        int slotWidth = (int)((hotbarWidth - (distanceBetweenSlots * (hotbarSlots.Count + 1))) / hotbarSlots.Count);
        int slotHeight = (int)(hotbarHeight - (distanceBetweenSlots * 2));

        // Set slot sizes
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            int targetX = (distanceBetweenSlots * (i + 1)) + (slotWidth * i) + (int)(slotWidth * 0.5f) - (int)(hotbarWidth / 2.0f);
            hotbarSlots[i].GetComponent<RectTransform>().anchoredPosition3D = new Vector2(targetX, 0);
            hotbarSlots[i].GetComponent<RectTransform>().sizeDelta = new Vector2(slotWidth, slotHeight);
        }
    }

    public void SetTargetHotbarSlot(int newTarget)
    {
        if (newTarget < 0)
        {
            newTarget += hotbarSlots.Count;
        }

        targetSlot = newTarget % hotbarSlots.Count;
        UpdateSlotColors();
    }

    private void UpdateSlotColors()
    {
        for (int i = 0; i < hotbarSlots.Count; i++)
        {
            if (i == targetSlot)
            {
                hotbarSlots[i].GetComponent<Image>().color = targetSlotColor;
            }
            else
            {
                hotbarSlots[i].GetComponent<Image>().color = baseSlotColor;
            }
        }
    }

    private void ProcessInput()
    {
        // Sroll Wheel
        {
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            if (scrollInput < 0)
            {
                SetTargetHotbarSlot(targetSlot - 1);
            }
            else if (scrollInput > 0)
            {
                SetTargetHotbarSlot(targetSlot + 1);
            }
        }

        // Number pad
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetTargetHotbarSlot(0);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetTargetHotbarSlot(1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SetTargetHotbarSlot(2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SetTargetHotbarSlot(3);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SetTargetHotbarSlot(4);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SetTargetHotbarSlot(5);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                SetTargetHotbarSlot(6);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                SetTargetHotbarSlot(7);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                SetTargetHotbarSlot(8);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SetTargetHotbarSlot(9);
            }
        }
    }
}
