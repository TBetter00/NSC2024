using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public KeyCode SwordSkill = KeyCode.Q;
    public KeyCode SwordAttack = KeyCode.E;
    public KeyCode ShieldSkill = KeyCode.Keypad3;
    public KeyCode ShieldAttack = KeyCode.Keypad1;
    [HideInInspector] public KeyCode SwordInteract;
    [HideInInspector] public KeyCode ShieldInteract;
    
    [HideInInspector] public KeyCode MainInteraction;
    [HideInInspector] public KeyCode NotMainInteraction;
    [SerializeField]Main main = new Main();
    [SerializeField]MainInteract mainInteract = new MainInteract();
    [SerializeField]NotMainInteract secondInteract = new NotMainInteract();
    
    
    enum Main{
        Sword,
        Shield
    }
    enum MainInteract{
        E,
        F
        
    }
    enum NotMainInteract{
        Num1,
        Num2,
        Num3,
        Num4,
        Num5,
        Num6,
        Num7,
        Num8,
        Num9,
        Num0
    };

    void Start()
    {
        MainInteraction = ConvertMainInteractToKeyCode(mainInteract);
        NotMainInteraction = ConvertNotMainInteractToKeyCode(secondInteract);
        switch(main){
            case Main.Sword:
            SwordInteract = MainInteraction;
            ShieldInteract = NotMainInteraction;
            break;
            case Main.Shield:
            ShieldInteract = MainInteraction;
            SwordInteract = NotMainInteraction;
            break;
        }
    }

    KeyCode ConvertMainInteractToKeyCode(MainInteract mainInteract)
    {
        switch (mainInteract)
        {
            case MainInteract.E:
                return KeyCode.E;
            case MainInteract.F:
                return KeyCode.F;
            default:
                throw new ArgumentOutOfRangeException(nameof(mainInteract), mainInteract, null);
        }
    }

    KeyCode ConvertNotMainInteractToKeyCode(NotMainInteract notMainInteract)
    {
        switch (notMainInteract)
        {
            case NotMainInteract.Num1:
                return KeyCode.Keypad1;
            case NotMainInteract.Num2:
                return KeyCode.Keypad2;
            case NotMainInteract.Num3:
                return KeyCode.Keypad3;
            case NotMainInteract.Num4:
                return KeyCode.Keypad4;
            case NotMainInteract.Num5:
                return KeyCode.Keypad5;
            case NotMainInteract.Num6:
                return KeyCode.Keypad6;
            case NotMainInteract.Num7:
                return KeyCode.Keypad7;
            case NotMainInteract.Num8:
                return KeyCode.Keypad8;
            case NotMainInteract.Num9:
                return KeyCode.Keypad9;
            case NotMainInteract.Num0:
                return KeyCode.Keypad0;
            default:
                throw new ArgumentOutOfRangeException(nameof(notMainInteract), notMainInteract, null);
        }
    }
}
