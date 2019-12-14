﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DemonDialogUI : MonoBehaviour {
	public DemonDialog demon;

	[SerializeField] CraftPanel craftPanel;
	[SerializeField] Inventory inventory;
	[SerializeField] PlayerHp hp;

	[SerializeField] CanvasGroup canvasGroup;
	[SerializeField] Image image;
	[SerializeField] TextMeshProUGUI dialogText;

	private void Awake() {
		canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
		canvasGroup.alpha = 0.0f;
	}

	public void ShowDialog() {
		herowalking.isCanMove = false;
		canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
		canvasGroup.alpha = 1.0f;

		image.sprite = demon.sprite;
		dialogText.text = demon.dialogText;
	}

	public void CloseDialog() {
		herowalking.isCanMove = true;
		canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
		canvasGroup.alpha = 0.0f;
	}

	public void CorrectGift(Item item, ItemType reward) {
		if (craftPanel.items.Contains(item))
			craftPanel.items.Remove(item);

		if (inventory.items.Contains(item))
			inventory.items.Remove(item);

		Item createdItem = craftPanel.CreateItem(reward);
		inventory.AddItem(createdItem);

		Destroy(item.gameObject);
		Destroy(createdItem.gameObject);

		dialogText.text = "Дяяяяяя.";
		demon.isGifted = true;

		LeanTween.delayedCall(1.0f, ()=> CloseDialog());
	}

	public void WrongGift() {
		dialogText.text = "Ніт \n" + demon.dialogText;
		--hp.CurrHp;
	}
}