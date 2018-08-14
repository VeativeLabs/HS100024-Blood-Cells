using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseMenu : MonoBehaviour
{
    public static ReverseMenu Instance;
    Transform[] _MenuItem;
    Vector3[] _MenuItemPositions;

    int _Count;

    void Awake()
    {
        Instance = this;
        _Count = transform.childCount;
        _MenuItem = new Transform[_Count];
        _MenuItemPositions = new Vector3[_Count];

        for (int i = 0; i < _Count; i++)
        {
            _MenuItem[i] = transform.GetChild(i);
            _MenuItemPositions[i] = _MenuItem[i].position;
        }
    }

    void Start()
    {
        HandleSyncButton.Instance.Swap_Btns();
        Reverse();
    }

    public void Reverse()
    {
        if (LanguageHandler.instance.IsLeftToRight)
        {
            for (int i = 0; i < _Count; i++)
            {
                _MenuItem[i].position = _MenuItemPositions[i];
            }
        }
        else
        {
            for (int i = 0; i < _Count; i++)
            {
                _MenuItem[i].position = _MenuItemPositions[_Count - 1 - i];
            }
        }
    }
}
