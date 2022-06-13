// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""PlayerInputControls"",
            ""id"": ""b7823ec0-b78c-4700-a0ca-fd243640df3e"",
            ""actions"": [
                {
                    ""name"": ""XYMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c75a5bcc-16d4-4e68-94ff-8ad808abc571"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Direction"",
                    ""type"": ""PassThrough"",
                    ""id"": ""df5f444c-39cc-4130-ab5f-4e10a7dd75f6"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""32398bb3-dab7-4951-a5c7-cb48415efded"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""3d8d663f-4b63-4cfb-9c47-fad4d7194bf2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HeavyAttack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3c77b894-09b6-4c62-97ce-29012a2e4b24"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""b7811fbd-356b-46bd-bfcc-582bd1f8b5f5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""df8d1fee-c887-421e-adf1-70a5d13a24a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""51121e36-f058-4818-9204-3fbb2bb25aae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""LightAttackAlt"",
                    ""type"": ""Button"",
                    ""id"": ""ae965790-f752-43f2-8688-42f33df9511e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""finallightattack"",
                    ""type"": ""Button"",
                    ""id"": ""0c83c134-623f-4f2c-90aa-7f3fa10fb4a1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""MultiTap(tapDelay=0.25,tapCount=3)""
                },
                {
                    ""name"": ""MapZoom"",
                    ""type"": ""Button"",
                    ""id"": ""bfcacace-d9f9-4947-b955-3230cbc61b08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MapMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6f57cec0-91bc-4f9e-9e0c-fccce9967337"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MapShortcut"",
                    ""type"": ""Button"",
                    ""id"": ""fa9cf725-e3c1-4a73-97a6-dde7f9d0d677"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""0339cf4d-4d54-47ba-b0e8-93f6f70c3807"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""RightJoystick"",
                    ""id"": ""8e9f6180-45e9-4dd3-af2f-0b8ced5085ba"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a4de92a2-6b58-494a-9ad5-acbf27d449b5"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""187fa438-a974-48ac-9c31-0201cdd38255"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9b862386-256b-43c8-8c55-5ec19259b2ab"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e1447957-0051-4377-a129-af2487950a59"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f96e2b3f-336d-4303-825b-64a7ad6b9493"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""LeftJoystick"",
                    ""id"": ""5fa97f69-ff65-4a27-ad5d-59175035079e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2b5ff46e-f87c-4c64-b91a-7b1c2d6ccdda"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8a5665c9-3f32-4439-a95f-87830c24f601"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b8bd06d4-3260-4b76-b80a-d9fc91b70e7d"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9689ed5a-823b-406a-9e0e-23ae8ceab879"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f1a4a1c1-e0b6-4d0e-a499-e623649f19f1"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99c376c3-4b94-42b4-b5ce-87f767ca62dd"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": ""Hold(duration=1.1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7fa908a9-0751-4a56-9575-05da19721b01"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": ""Tap(duration=0.35)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3b1870a5-3a0e-4b10-bb60-14a727a3565a"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""LeftJoystick"",
                    ""id"": ""0236aa5e-991f-4d45-95a7-9d460cc66add"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b8640528-8a21-4c5e-a7b3-d0af621722b3"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""db4bc007-75fc-4f23-a3fe-6b130cde285d"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ba74739b-47e7-48d4-b33e-46e8c261dd28"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b598f1cf-71f0-4287-8f37-19b7524b6944"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Direction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""d2420dab-af1a-4117-959f-c2feb44b2b5f"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""93bdb6cc-7e19-4864-a3d6-324720da5363"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cd5bf96-1c15-40b6-9519-cf1389a9c1c0"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": ""MultiTap(tapDelay=1.6,tapCount=3)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""finallightattack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d698172-2329-4caa-9016-196530cd1645"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": ""MultiTap(tapDelay=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightAttackAlt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""id"": ""a6412365-448f-489c-a100-a6dd2c304dc8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""35042993-827d-43d6-8843-5ebbf9453edb"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""cb1f6003-8f32-4eb6-add0-b8b8816c1e18"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a2616d85-f128-4308-b64f-4ca5b5270568"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""LeftJoystick"",
                    ""id"": ""70a2e52a-cf54-4dc0-b89b-cfbe3f276a40"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b2e3f315-ffb8-4313-8a28-08bf11a1ed4f"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6669b0c5-3409-41e5-ae98-d9f01ea2cc5f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bfe3add3-8fe5-42ab-9365-977e090dd0e3"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ab05f2bb-5e85-4745-9e8b-b4e2702111dc"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""412f7ea7-82e4-4a21-96ff-2f8023bb3746"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapShortcut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PlayerInputControlsKeyboard"",
            ""id"": ""48275ed4-fb86-4ca8-9ca5-bed2c790057a"",
            ""actions"": [
                {
                    ""name"": ""XYMovement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""fb835ea7-32f4-435a-976c-a3859bbc8f38"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Direction"",
                    ""type"": ""PassThrough"",
                    ""id"": ""b731f2e0-c536-4de5-8658-0acdb64895ec"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""ca5994d4-8ce3-4b4c-aad6-08f0a84265f3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""99a837a8-daef-44d2-bddf-45eb21beccaa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HeavyAttack"",
                    ""type"": ""PassThrough"",
                    ""id"": ""1bf14501-3989-4aaf-8391-9fcb71bd3a85"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LightAttack"",
                    ""type"": ""Button"",
                    ""id"": ""ec45683f-0824-43fd-9944-5863233719a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""6c4f9bc0-d5e8-48b4-89e4-646c4114e27f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""34d1959d-ed2c-4f6a-81b3-b6f900f4aa5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Tap""
                },
                {
                    ""name"": ""LightAttackAlt"",
                    ""type"": ""Button"",
                    ""id"": ""06532081-4d68-4bc0-ad51-964222c88785"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""finallightattack"",
                    ""type"": ""Button"",
                    ""id"": ""2024f4be-8f20-4f2d-bcae-e60f45728fe1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MapZoom"",
                    ""type"": ""Button"",
                    ""id"": ""df10da3b-b1d8-479a-b347-5abed8930e8b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MapMove"",
                    ""type"": ""PassThrough"",
                    ""id"": ""71dc87b1-980b-40b4-9c22-faec4972e1e5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MapMouseZoom"",
                    ""type"": ""Value"",
                    ""id"": ""76aad476-8243-4de4-833a-a7c354550f87"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""PassThrough"",
                    ""id"": ""92674ad8-4ee5-4e08-9f6c-13b464fb8006"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MapShortcut"",
                    ""type"": ""Button"",
                    ""id"": ""0ddd3360-46e7-4be2-94c2-e24ba07b357d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""ca9b9e70-78a0-47fb-b5e0-81503694f00c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""43d2be2b-6559-423f-88f7-cdad4d9946e0"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b4818fe5-65a2-4a24-8fa0-6a33f2c99c9d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""19226e75-3168-4ea7-8d10-b8a2193d9b03"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""97563c7e-8b96-4398-9df4-c976ae119d29"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c70f14c2-e416-4c01-bfcb-2c71bb3798bb"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""861819fd-da25-4a47-9a69-cece0a9963f0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""dc537695-d191-4c5f-9bad-c3c926aa2fe4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d4b67c5e-2878-4121-8949-21e33fd3a2fc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6d155223-8880-4e38-9628-2b89ea83f6e7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""910cef5f-50d4-48c7-a6be-bccdcdb96677"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""XYMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""26fd17b4-b071-4e1a-bd56-a50c43dca187"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""227e9f97-4e67-454b-acea-f95994423b86"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold(duration=1.1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HeavyAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""385a5721-4ae1-4c82-ab0b-c2fed97fa16e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Tap(duration=0.35)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f763dbdd-fa0c-4164-b3c5-fc5281dae4de"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a2517b8-d418-4dc8-a2ba-2b7eca5a7e65"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d764fc50-7b0c-497b-8078-deadf2424fba"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""MultiTap(tapDelay=1)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightAttackAlt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ed321474-b811-44d1-a923-1a16a464235b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""MultiTap(tapDelay=2,tapCount=3)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""finallightattack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""id"": ""3a6576d4-b2e2-43b7-9d4f-b03bb2f6fb5c"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""53ae81ac-df65-4a01-864d-08c6ee23a2a3"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c67ef5dd-224f-489b-ac08-d6551d5d8c00"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""31fa8e0c-e2ee-40a9-9445-a6ed3f093a24"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone(min=-0.5,max=-1)"",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""3ca3ebe3-eda7-490d-ae14-ffd4a99f8287"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone(min=0.5,max=1)"",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a9a4e943-2912-4cf4-9346-91f227fed8aa"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0ab73808-761c-4bea-95b4-2f5e471eb315"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e1a4ad98-4459-4dd7-916b-bcb0c9b7434e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""efddc525-5d33-46ce-a423-88d8c2f01246"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""46158d8a-fec0-4c36-af48-d6abbcb8bbbf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""eb7e699b-c521-454f-ad43-5fee2359b576"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""031dc2fb-c561-42b7-b8aa-f020053bc290"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1f1c356c-0fc7-4137-97a1-38a8e80393bd"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""MapMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b451e7d0-6ce8-4a88-a984-93ceb9af05b7"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": ""NormalizeVector2"",
                    ""groups"": """",
                    ""action"": ""MapMouseZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b856853-8d95-4045-be2d-4c2306621da4"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Hold(duration=0.4)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4978319-d048-4d2f-9611-a25bbd4eb4b9"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MapShortcut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""KeyboardAndMouse"",
            ""id"": ""65c9bd12-a8bb-4384-8b35-24026308383a"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""d61d25ac-cf49-466a-bd6e-f7afeb5daebe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""ec85ddf8-8ad3-4c4a-95a4-e010c9740545"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerInputControls
        m_PlayerInputControls = asset.FindActionMap("PlayerInputControls", throwIfNotFound: true);
        m_PlayerInputControls_XYMovement = m_PlayerInputControls.FindAction("XYMovement", throwIfNotFound: true);
        m_PlayerInputControls_Direction = m_PlayerInputControls.FindAction("Direction", throwIfNotFound: true);
        m_PlayerInputControls_Rotation = m_PlayerInputControls.FindAction("Rotation", throwIfNotFound: true);
        m_PlayerInputControls_Jump = m_PlayerInputControls.FindAction("Jump", throwIfNotFound: true);
        m_PlayerInputControls_HeavyAttack = m_PlayerInputControls.FindAction("HeavyAttack", throwIfNotFound: true);
        m_PlayerInputControls_LightAttack = m_PlayerInputControls.FindAction("LightAttack", throwIfNotFound: true);
        m_PlayerInputControls_Interaction = m_PlayerInputControls.FindAction("Interaction", throwIfNotFound: true);
        m_PlayerInputControls_PauseGame = m_PlayerInputControls.FindAction("PauseGame", throwIfNotFound: true);
        m_PlayerInputControls_LightAttackAlt = m_PlayerInputControls.FindAction("LightAttackAlt", throwIfNotFound: true);
        m_PlayerInputControls_finallightattack = m_PlayerInputControls.FindAction("finallightattack", throwIfNotFound: true);
        m_PlayerInputControls_MapZoom = m_PlayerInputControls.FindAction("MapZoom", throwIfNotFound: true);
        m_PlayerInputControls_MapMove = m_PlayerInputControls.FindAction("MapMove", throwIfNotFound: true);
        m_PlayerInputControls_MapShortcut = m_PlayerInputControls.FindAction("MapShortcut", throwIfNotFound: true);
        // PlayerInputControlsKeyboard
        m_PlayerInputControlsKeyboard = asset.FindActionMap("PlayerInputControlsKeyboard", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_XYMovement = m_PlayerInputControlsKeyboard.FindAction("XYMovement", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_Direction = m_PlayerInputControlsKeyboard.FindAction("Direction", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_Rotation = m_PlayerInputControlsKeyboard.FindAction("Rotation", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_Jump = m_PlayerInputControlsKeyboard.FindAction("Jump", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_HeavyAttack = m_PlayerInputControlsKeyboard.FindAction("HeavyAttack", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_LightAttack = m_PlayerInputControlsKeyboard.FindAction("LightAttack", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_Interaction = m_PlayerInputControlsKeyboard.FindAction("Interaction", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_PauseGame = m_PlayerInputControlsKeyboard.FindAction("PauseGame", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_LightAttackAlt = m_PlayerInputControlsKeyboard.FindAction("LightAttackAlt", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_finallightattack = m_PlayerInputControlsKeyboard.FindAction("finallightattack", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_MapZoom = m_PlayerInputControlsKeyboard.FindAction("MapZoom", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_MapMove = m_PlayerInputControlsKeyboard.FindAction("MapMove", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_MapMouseZoom = m_PlayerInputControlsKeyboard.FindAction("MapMouseZoom", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_Block = m_PlayerInputControlsKeyboard.FindAction("Block", throwIfNotFound: true);
        m_PlayerInputControlsKeyboard_MapShortcut = m_PlayerInputControlsKeyboard.FindAction("MapShortcut", throwIfNotFound: true);
        // KeyboardAndMouse
        m_KeyboardAndMouse = asset.FindActionMap("KeyboardAndMouse", throwIfNotFound: true);
        m_KeyboardAndMouse_Newaction = m_KeyboardAndMouse.FindAction("New action", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // PlayerInputControls
    private readonly InputActionMap m_PlayerInputControls;
    private IPlayerInputControlsActions m_PlayerInputControlsActionsCallbackInterface;
    private readonly InputAction m_PlayerInputControls_XYMovement;
    private readonly InputAction m_PlayerInputControls_Direction;
    private readonly InputAction m_PlayerInputControls_Rotation;
    private readonly InputAction m_PlayerInputControls_Jump;
    private readonly InputAction m_PlayerInputControls_HeavyAttack;
    private readonly InputAction m_PlayerInputControls_LightAttack;
    private readonly InputAction m_PlayerInputControls_Interaction;
    private readonly InputAction m_PlayerInputControls_PauseGame;
    private readonly InputAction m_PlayerInputControls_LightAttackAlt;
    private readonly InputAction m_PlayerInputControls_finallightattack;
    private readonly InputAction m_PlayerInputControls_MapZoom;
    private readonly InputAction m_PlayerInputControls_MapMove;
    private readonly InputAction m_PlayerInputControls_MapShortcut;
    public struct PlayerInputControlsActions
    {
        private @Controls m_Wrapper;
        public PlayerInputControlsActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @XYMovement => m_Wrapper.m_PlayerInputControls_XYMovement;
        public InputAction @Direction => m_Wrapper.m_PlayerInputControls_Direction;
        public InputAction @Rotation => m_Wrapper.m_PlayerInputControls_Rotation;
        public InputAction @Jump => m_Wrapper.m_PlayerInputControls_Jump;
        public InputAction @HeavyAttack => m_Wrapper.m_PlayerInputControls_HeavyAttack;
        public InputAction @LightAttack => m_Wrapper.m_PlayerInputControls_LightAttack;
        public InputAction @Interaction => m_Wrapper.m_PlayerInputControls_Interaction;
        public InputAction @PauseGame => m_Wrapper.m_PlayerInputControls_PauseGame;
        public InputAction @LightAttackAlt => m_Wrapper.m_PlayerInputControls_LightAttackAlt;
        public InputAction @finallightattack => m_Wrapper.m_PlayerInputControls_finallightattack;
        public InputAction @MapZoom => m_Wrapper.m_PlayerInputControls_MapZoom;
        public InputAction @MapMove => m_Wrapper.m_PlayerInputControls_MapMove;
        public InputAction @MapShortcut => m_Wrapper.m_PlayerInputControls_MapShortcut;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInputControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputControlsActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputControlsActions instance)
        {
            if (m_Wrapper.m_PlayerInputControlsActionsCallbackInterface != null)
            {
                @XYMovement.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnXYMovement;
                @XYMovement.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnXYMovement;
                @XYMovement.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnXYMovement;
                @Direction.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnDirection;
                @Direction.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnDirection;
                @Direction.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnDirection;
                @Rotation.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnRotation;
                @Jump.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnJump;
                @HeavyAttack.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnHeavyAttack;
                @LightAttack.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnLightAttack;
                @LightAttack.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnLightAttack;
                @LightAttack.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnLightAttack;
                @Interaction.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnInteraction;
                @PauseGame.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnPauseGame;
                @LightAttackAlt.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnLightAttackAlt;
                @LightAttackAlt.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnLightAttackAlt;
                @LightAttackAlt.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnLightAttackAlt;
                @finallightattack.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnFinallightattack;
                @finallightattack.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnFinallightattack;
                @finallightattack.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnFinallightattack;
                @MapZoom.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnMapZoom;
                @MapZoom.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnMapZoom;
                @MapZoom.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnMapZoom;
                @MapMove.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnMapMove;
                @MapMove.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnMapMove;
                @MapMove.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnMapMove;
                @MapShortcut.started -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnMapShortcut;
                @MapShortcut.performed -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnMapShortcut;
                @MapShortcut.canceled -= m_Wrapper.m_PlayerInputControlsActionsCallbackInterface.OnMapShortcut;
            }
            m_Wrapper.m_PlayerInputControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @XYMovement.started += instance.OnXYMovement;
                @XYMovement.performed += instance.OnXYMovement;
                @XYMovement.canceled += instance.OnXYMovement;
                @Direction.started += instance.OnDirection;
                @Direction.performed += instance.OnDirection;
                @Direction.canceled += instance.OnDirection;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @LightAttack.started += instance.OnLightAttack;
                @LightAttack.performed += instance.OnLightAttack;
                @LightAttack.canceled += instance.OnLightAttack;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
                @LightAttackAlt.started += instance.OnLightAttackAlt;
                @LightAttackAlt.performed += instance.OnLightAttackAlt;
                @LightAttackAlt.canceled += instance.OnLightAttackAlt;
                @finallightattack.started += instance.OnFinallightattack;
                @finallightattack.performed += instance.OnFinallightattack;
                @finallightattack.canceled += instance.OnFinallightattack;
                @MapZoom.started += instance.OnMapZoom;
                @MapZoom.performed += instance.OnMapZoom;
                @MapZoom.canceled += instance.OnMapZoom;
                @MapMove.started += instance.OnMapMove;
                @MapMove.performed += instance.OnMapMove;
                @MapMove.canceled += instance.OnMapMove;
                @MapShortcut.started += instance.OnMapShortcut;
                @MapShortcut.performed += instance.OnMapShortcut;
                @MapShortcut.canceled += instance.OnMapShortcut;
            }
        }
    }
    public PlayerInputControlsActions @PlayerInputControls => new PlayerInputControlsActions(this);

    // PlayerInputControlsKeyboard
    private readonly InputActionMap m_PlayerInputControlsKeyboard;
    private IPlayerInputControlsKeyboardActions m_PlayerInputControlsKeyboardActionsCallbackInterface;
    private readonly InputAction m_PlayerInputControlsKeyboard_XYMovement;
    private readonly InputAction m_PlayerInputControlsKeyboard_Direction;
    private readonly InputAction m_PlayerInputControlsKeyboard_Rotation;
    private readonly InputAction m_PlayerInputControlsKeyboard_Jump;
    private readonly InputAction m_PlayerInputControlsKeyboard_HeavyAttack;
    private readonly InputAction m_PlayerInputControlsKeyboard_LightAttack;
    private readonly InputAction m_PlayerInputControlsKeyboard_Interaction;
    private readonly InputAction m_PlayerInputControlsKeyboard_PauseGame;
    private readonly InputAction m_PlayerInputControlsKeyboard_LightAttackAlt;
    private readonly InputAction m_PlayerInputControlsKeyboard_finallightattack;
    private readonly InputAction m_PlayerInputControlsKeyboard_MapZoom;
    private readonly InputAction m_PlayerInputControlsKeyboard_MapMove;
    private readonly InputAction m_PlayerInputControlsKeyboard_MapMouseZoom;
    private readonly InputAction m_PlayerInputControlsKeyboard_Block;
    private readonly InputAction m_PlayerInputControlsKeyboard_MapShortcut;
    public struct PlayerInputControlsKeyboardActions
    {
        private @Controls m_Wrapper;
        public PlayerInputControlsKeyboardActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @XYMovement => m_Wrapper.m_PlayerInputControlsKeyboard_XYMovement;
        public InputAction @Direction => m_Wrapper.m_PlayerInputControlsKeyboard_Direction;
        public InputAction @Rotation => m_Wrapper.m_PlayerInputControlsKeyboard_Rotation;
        public InputAction @Jump => m_Wrapper.m_PlayerInputControlsKeyboard_Jump;
        public InputAction @HeavyAttack => m_Wrapper.m_PlayerInputControlsKeyboard_HeavyAttack;
        public InputAction @LightAttack => m_Wrapper.m_PlayerInputControlsKeyboard_LightAttack;
        public InputAction @Interaction => m_Wrapper.m_PlayerInputControlsKeyboard_Interaction;
        public InputAction @PauseGame => m_Wrapper.m_PlayerInputControlsKeyboard_PauseGame;
        public InputAction @LightAttackAlt => m_Wrapper.m_PlayerInputControlsKeyboard_LightAttackAlt;
        public InputAction @finallightattack => m_Wrapper.m_PlayerInputControlsKeyboard_finallightattack;
        public InputAction @MapZoom => m_Wrapper.m_PlayerInputControlsKeyboard_MapZoom;
        public InputAction @MapMove => m_Wrapper.m_PlayerInputControlsKeyboard_MapMove;
        public InputAction @MapMouseZoom => m_Wrapper.m_PlayerInputControlsKeyboard_MapMouseZoom;
        public InputAction @Block => m_Wrapper.m_PlayerInputControlsKeyboard_Block;
        public InputAction @MapShortcut => m_Wrapper.m_PlayerInputControlsKeyboard_MapShortcut;
        public InputActionMap Get() { return m_Wrapper.m_PlayerInputControlsKeyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerInputControlsKeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerInputControlsKeyboardActions instance)
        {
            if (m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface != null)
            {
                @XYMovement.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnXYMovement;
                @XYMovement.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnXYMovement;
                @XYMovement.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnXYMovement;
                @Direction.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnDirection;
                @Direction.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnDirection;
                @Direction.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnDirection;
                @Rotation.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnRotation;
                @Rotation.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnRotation;
                @Rotation.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnRotation;
                @Jump.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnJump;
                @HeavyAttack.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnHeavyAttack;
                @HeavyAttack.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnHeavyAttack;
                @LightAttack.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnLightAttack;
                @LightAttack.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnLightAttack;
                @LightAttack.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnLightAttack;
                @Interaction.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnInteraction;
                @Interaction.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnInteraction;
                @Interaction.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnInteraction;
                @PauseGame.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnPauseGame;
                @PauseGame.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnPauseGame;
                @PauseGame.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnPauseGame;
                @LightAttackAlt.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnLightAttackAlt;
                @LightAttackAlt.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnLightAttackAlt;
                @LightAttackAlt.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnLightAttackAlt;
                @finallightattack.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnFinallightattack;
                @finallightattack.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnFinallightattack;
                @finallightattack.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnFinallightattack;
                @MapZoom.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapZoom;
                @MapZoom.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapZoom;
                @MapZoom.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapZoom;
                @MapMove.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapMove;
                @MapMove.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapMove;
                @MapMove.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapMove;
                @MapMouseZoom.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapMouseZoom;
                @MapMouseZoom.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapMouseZoom;
                @MapMouseZoom.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapMouseZoom;
                @Block.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnBlock;
                @MapShortcut.started -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapShortcut;
                @MapShortcut.performed -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapShortcut;
                @MapShortcut.canceled -= m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface.OnMapShortcut;
            }
            m_Wrapper.m_PlayerInputControlsKeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @XYMovement.started += instance.OnXYMovement;
                @XYMovement.performed += instance.OnXYMovement;
                @XYMovement.canceled += instance.OnXYMovement;
                @Direction.started += instance.OnDirection;
                @Direction.performed += instance.OnDirection;
                @Direction.canceled += instance.OnDirection;
                @Rotation.started += instance.OnRotation;
                @Rotation.performed += instance.OnRotation;
                @Rotation.canceled += instance.OnRotation;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @HeavyAttack.started += instance.OnHeavyAttack;
                @HeavyAttack.performed += instance.OnHeavyAttack;
                @HeavyAttack.canceled += instance.OnHeavyAttack;
                @LightAttack.started += instance.OnLightAttack;
                @LightAttack.performed += instance.OnLightAttack;
                @LightAttack.canceled += instance.OnLightAttack;
                @Interaction.started += instance.OnInteraction;
                @Interaction.performed += instance.OnInteraction;
                @Interaction.canceled += instance.OnInteraction;
                @PauseGame.started += instance.OnPauseGame;
                @PauseGame.performed += instance.OnPauseGame;
                @PauseGame.canceled += instance.OnPauseGame;
                @LightAttackAlt.started += instance.OnLightAttackAlt;
                @LightAttackAlt.performed += instance.OnLightAttackAlt;
                @LightAttackAlt.canceled += instance.OnLightAttackAlt;
                @finallightattack.started += instance.OnFinallightattack;
                @finallightattack.performed += instance.OnFinallightattack;
                @finallightattack.canceled += instance.OnFinallightattack;
                @MapZoom.started += instance.OnMapZoom;
                @MapZoom.performed += instance.OnMapZoom;
                @MapZoom.canceled += instance.OnMapZoom;
                @MapMove.started += instance.OnMapMove;
                @MapMove.performed += instance.OnMapMove;
                @MapMove.canceled += instance.OnMapMove;
                @MapMouseZoom.started += instance.OnMapMouseZoom;
                @MapMouseZoom.performed += instance.OnMapMouseZoom;
                @MapMouseZoom.canceled += instance.OnMapMouseZoom;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @MapShortcut.started += instance.OnMapShortcut;
                @MapShortcut.performed += instance.OnMapShortcut;
                @MapShortcut.canceled += instance.OnMapShortcut;
            }
        }
    }
    public PlayerInputControlsKeyboardActions @PlayerInputControlsKeyboard => new PlayerInputControlsKeyboardActions(this);

    // KeyboardAndMouse
    private readonly InputActionMap m_KeyboardAndMouse;
    private IKeyboardAndMouseActions m_KeyboardAndMouseActionsCallbackInterface;
    private readonly InputAction m_KeyboardAndMouse_Newaction;
    public struct KeyboardAndMouseActions
    {
        private @Controls m_Wrapper;
        public KeyboardAndMouseActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_KeyboardAndMouse_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardAndMouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardAndMouseActions set) { return set.Get(); }
        public void SetCallbacks(IKeyboardAndMouseActions instance)
        {
            if (m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface != null)
            {
                @Newaction.started -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnNewaction;
                @Newaction.performed -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnNewaction;
                @Newaction.canceled -= m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_KeyboardAndMouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Newaction.started += instance.OnNewaction;
                @Newaction.performed += instance.OnNewaction;
                @Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public KeyboardAndMouseActions @KeyboardAndMouse => new KeyboardAndMouseActions(this);
    public interface IPlayerInputControlsActions
    {
        void OnXYMovement(InputAction.CallbackContext context);
        void OnDirection(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnLightAttack(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
        void OnLightAttackAlt(InputAction.CallbackContext context);
        void OnFinallightattack(InputAction.CallbackContext context);
        void OnMapZoom(InputAction.CallbackContext context);
        void OnMapMove(InputAction.CallbackContext context);
        void OnMapShortcut(InputAction.CallbackContext context);
    }
    public interface IPlayerInputControlsKeyboardActions
    {
        void OnXYMovement(InputAction.CallbackContext context);
        void OnDirection(InputAction.CallbackContext context);
        void OnRotation(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnHeavyAttack(InputAction.CallbackContext context);
        void OnLightAttack(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
        void OnLightAttackAlt(InputAction.CallbackContext context);
        void OnFinallightattack(InputAction.CallbackContext context);
        void OnMapZoom(InputAction.CallbackContext context);
        void OnMapMove(InputAction.CallbackContext context);
        void OnMapMouseZoom(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnMapShortcut(InputAction.CallbackContext context);
    }
    public interface IKeyboardAndMouseActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
}
