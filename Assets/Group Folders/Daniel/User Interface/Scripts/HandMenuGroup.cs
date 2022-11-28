using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;

public class HandMenuGroup : MonoBehaviour
{
    private PokeInteractable[] _pokeInteractables;
    private HandMenuItem[] _handMenuItems;
    private HandMenuItem _selectedMenuItem;
    void Awake()
    {
        _pokeInteractables = GetComponentsInChildren<PokeInteractable>();
        _handMenuItems = GetComponentsInChildren<HandMenuItem>();
    }

    private void OnEnable()
    {
        foreach (var pokeInteractable in _pokeInteractables)
        {
            pokeInteractable.WhenStateChanged += SetSelectedMenuItem;
        }
    }

    private void OnDisable()
    {
        foreach (var pokeInteractable in _pokeInteractables)
        {
            pokeInteractable.WhenStateChanged -= SetSelectedMenuItem;
        }
    }

    private void SetSelectedMenuItem(InteractableStateChangeArgs args)
    {
        if (args.NewState != InteractableState.Select) return;

        StartCoroutine(Test());
    }

    private void UpdateMenuItemSelections()
    {
        //HandMenuItem[] selectedHandMenuItems = _handMenuItems.Where(item => item._isSelected).ToArray();
        foreach (var menuItem in _handMenuItems)
        {
            if (menuItem._currentState == InteractableState.Select)
            {
                continue;
            }
            menuItem.m_isSelected = false;
            menuItem.UpdateSelection();
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForEndOfFrame();
        UpdateMenuItemSelections();
    }
}
