using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MapMenuManager : MonoBehaviour
{

    [SerializeField] GameObject MapMenu;
    [SerializeField] GameObject MapCloseButton;
    [SerializeField] GameObject PhoneResumeButton;
    [SerializeField] Camera MapRenderCamera;


    [SerializeField] float MaxZoom, MinZoom;
    float ZoomInVariable;
    private void Start()
    {
        InputManager.inputManager.PInput.PlayerInputControls.PauseGame.performed += ctx
        =>
        {
            if(MapMenu.activeInHierarchy == true)
            {
                CloseMapMenu();
                return;
            }
        };

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.PauseGame.performed += ctx
        =>
        {
            if (MapMenu.activeInHierarchy == true)
            {
                CloseMapMenu();
                return;
            }
        };

        InputManager.inputManager.PInput.PlayerInputControls.MapShortcut.performed += ctx
        =>
        {
            if(MapMenu.activeInHierarchy== true)
            {
                CloseMapMenu();
                return;
            }
            else
            {
                OpenMapMenu();
            }

        };

        InputManager.inputManager.PInput.PlayerInputControlsKeyboard.MapShortcut.performed += ctx
        =>
        {
            if (MapMenu.activeInHierarchy == true)
            {
                CloseMapMenu();
                return;
            }
            else
            {
                OpenMapMenu();
            }
        };

    }

    float ZoomValue;
    private void Update()
    {
        ZoomInVariable = (InputManager.inputManager.PInput.PlayerInputControlsKeyboard.MapMouseZoom.ReadValue<Vector2>().y * 2f) + InputManager.inputManager.PInput.PlayerInputControls.MapZoom.ReadValue<float>();
        Vector2 MoveVar = InputManager.inputManager.PInput.PlayerInputControlsKeyboard.MapMove.ReadValue<Vector2>() + InputManager.inputManager.PInput.PlayerInputControls.MapMove.ReadValue<Vector2>();
        if (MapMenu.activeInHierarchy)
        { 
            if (MapRenderCamera.orthographicSize  >= MinZoom - 1 && MapRenderCamera.orthographicSize <= MaxZoom +1)
            { 
                ZoomValue += 100 * Time.fixedDeltaTime * - ZoomInVariable;
            }
            else if(MapRenderCamera.orthographicSize <= MinZoom)
            {
                ZoomValue = MinZoom + 5;
            }
            else if (MapRenderCamera.orthographicSize >= MaxZoom)
            {
                ZoomValue = MaxZoom - 5;
            }


            MapRenderCamera.orthographicSize = Mathf.Lerp(MapRenderCamera.orthographicSize, ZoomValue, Time.fixedDeltaTime * 5f);

            float MoveMult = MapRenderCamera.orthographicSize/MaxZoom;
            MapRenderCamera.transform.position += (Vector3)MoveVar.normalized * Time.fixedDeltaTime * 100 * MoveMult;
        }
    }
    public void OpenMapMenu()
    {
        MapMenu.SetActive(true);
        MapRenderCamera.transform.localPosition = new Vector3(0, 0, MapRenderCamera.transform.localPosition.z);
        EventSystem.current.SetSelectedGameObject(MapCloseButton);
    }

    public void CloseMapMenu()
    {
        MapMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(PhoneResumeButton);
    }

}
