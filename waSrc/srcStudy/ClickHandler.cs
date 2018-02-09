using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x020000D9 RID: 217
public class ClickHandler : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x060005ED RID: 1517 RVA: 0x00023CB5 File Offset: 0x000220B5
	public void OnPointerClick(PointerEventData eventData)
	{
		this.clickHandler.Invoke(base.gameObject);
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x00023CC8 File Offset: 0x000220C8
	public void AddClickHandler(UnityAction<GameObject> handler)
	{
		this.clickHandler.AddListener(handler);
	}

	// Token: 0x0400051E RID: 1310
	[SerializeField]
	private ClickHandler.ClickEvent clickHandler;

	// Token: 0x020000DA RID: 218
	[Serializable]
	public class ClickEvent : UnityEvent<GameObject>
	{
	}
}
