using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action<string> ShowDialogueEvent;

    public static void CallShowDialogueEvent(string dialogue)
    {
        ShowDialogueEvent?.Invoke(dialogue);
    }

    public static event Action OnDialogueContinue;

    public static void CallDialogueContinueEvent()
    {
        OnDialogueContinue?.Invoke();
    }

    public static event Action<GameState> GameStateChangeEvent;

    public static void CallGameStateChangeEvent(GameState gameState)
    {
        GameStateChangeEvent?.Invoke(gameState);
    }

    public static event Action<ItemDetails, int> UpdateUIEvent;

    public static void CallUpdateUIEvent(ItemDetails itemDetails, int index)//物品栏UI显示
    {
        UpdateUIEvent?.Invoke(itemDetails, index);
    }

    public static event Action BeforeSceneUnloadEvent;

    public static void CallBeforeSceneUnloadEvent()//用于更新场景中的物品
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event Action AfterSceneLoadedEvent;

    public static void CallAfterSceneLoadedEvent()//用于更新场景中的物品
    {
        AfterSceneLoadedEvent?.Invoke();
    }

    public static event Action<ItemDetails, bool> ItemSelectedEvent;

    public static void CallItemSelectedEvent(ItemDetails itemDetails, bool isSelected)
    {
        ItemSelectedEvent?.Invoke(itemDetails, isSelected);
    }

    public static event Action<int> ChangeItemEvent;//切换物品
    public static void CallChangeItemEvent(int index)
    {
        ChangeItemEvent?.Invoke(index);
    }

    public static event Action<ItemName> ItemUsedEvent;//切换物品
    public static void CallItemUsedEvent(ItemName itemName)
    {
        ItemUsedEvent?.Invoke(itemName);
    }
}
