using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableObject : MonoBehaviour
{
	public string achievementName;

	// Clickable if the object has collider
	void OnMouseDown()
	{
		if (!IsPointerOverUIObject())
		{
			AchievementManager.Instance.EarnAchievement(achievementName);
		}
	}

	public bool IsPointerOverUIObject()
	{
		PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
		{
			position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
		};
		List<RaycastResult> results = new List<RaycastResult>();
		EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
		return results.Count > 0;
	}
}
