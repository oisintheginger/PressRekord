using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class InputManager : MonoBehaviour
{
    public static InputManager inputManager;
    public Controls PInput;
    public float InputDelay = 0.1f;
    public float Input;
    public Vector2 playerInput, rotInput;

    public Vector2 attackDirection;

    public float AttackDistance;

    public Vector2 LastMoveDirection;

    public static bool GameIsPaused = false;

    public static bool PlayerInDialogue = false;

    private void Awake()
    {
        if(inputManager!=this)
        {
            inputManager = this;
        }

        RegisterInput();
    }
    public void RegisterInput()
    {
        PInput = new Controls();
        PInput.PlayerInputControls.XYMovement.performed += ctx => playerInput = ctx.ReadValue<Vector2>();
        PInput.PlayerInputControls.Rotation.performed += ctx => rotInput = ctx.ReadValue<Vector2>();

        PInput.PlayerInputControlsKeyboard.XYMovement.performed += ctx => playerInput = ctx.ReadValue<Vector2>();
        PInput.PlayerInputControlsKeyboard.Rotation.performed += ctx => rotInput = ctx.ReadValue<Vector2>();

        
    }

    private void OnEnable()
    {
        PInput.Enable();
    }
    private void OnDisable()
    {
        PInput.Disable();
    }


    PlayerInput PI;

    void Start()
    {
        PI = this.GetComponent<PlayerInput>();

        GameEventsSystem.gameEventsSystem.DialEvents.StartDialogueEvent += SetInDialogueTrue;
        GameEventsSystem.gameEventsSystem.DialEvents.EndDialogueEvent += SetInDialogueFalse;
    }

    IEnumerator StopVibingCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        StopVibing();
    }


    public void StartVibing(float low, float high, float duration)
    {
        StopCoroutine("StopVibingCoroutine");
        var gamepad = GetGamepad();
        if (gamepad == null)
        {
            return;
        }
        gamepad.SetMotorSpeeds(low, high);

        StartCoroutine(StopVibingCoroutine());
    }

    public void StopVibing()
    {
        var gamepad = GetGamepad();
        if (gamepad == null)
        {
            return;
        }
        gamepad.SetMotorSpeeds(0f, 0f);
    }
    Gamepad GetGamepad()
    {
        return Gamepad.all.FirstOrDefault(gp => PI.devices.Any(d => d.deviceId == gp.deviceId));
    }

    void SetInDialogueFalse()
    {
        PlayerInDialogue = false;
    }

    void SetInDialogueTrue(DialogueObject UNUSED)
    {
        PlayerInDialogue = true;

    }

}
