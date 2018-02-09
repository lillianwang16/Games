using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x020000D7 RID: 215
[RequireComponent(typeof(EventTrigger))]
public class BtnHoldChecker : MonoBehaviour
{
	// Token: 0x060005DD RID: 1501 RVA: 0x00023930 File Offset: 0x00021D30
	public void Start()
	{
		EventTrigger component = base.GetComponent<EventTrigger>();
		component.triggers = new List<EventTrigger.Entry>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener(delegate(BaseEventData eventData)
		{
			this.PointerDown();
		});
		component.triggers.Add(entry);
		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerUp;
		entry.callback.AddListener(delegate(BaseEventData eventData)
		{
			this.PointerUp();
		});
		component.triggers.Add(entry);
	}

	// Token: 0x060005DE RID: 1502 RVA: 0x000239AF File Offset: 0x00021DAF
	public void PointerDown()
	{
		this.pushFlag = true;
	}

	// Token: 0x060005DF RID: 1503 RVA: 0x000239B8 File Offset: 0x00021DB8
	public void PointerUp()
	{
		this.pushFlag = false;
		this.pushTimer = 0f;
	}

	// Token: 0x060005E0 RID: 1504 RVA: 0x000239CC File Offset: 0x00021DCC
	public void Update()
	{
		if (this.pushFlag)
		{
			this.pushTimer += Time.deltaTime;
			if (this.pushTimer >= this.exeLimit && this.pushTimer - Time.deltaTime < this.exeLimit)
			{
				this.HoldEvent.Invoke();
			}
		}
	}

	// Token: 0x04000519 RID: 1305
	[SerializeField]
	private float exeLimit = 1f;

	// Token: 0x0400051A RID: 1306
	[SerializeField]
	private float pushTimer;

	// Token: 0x0400051B RID: 1307
	[SerializeField]
	private bool pushFlag;

	// Token: 0x0400051C RID: 1308
	[SerializeField]
	private UnityEvent HoldEvent = new UnityEvent();
}
