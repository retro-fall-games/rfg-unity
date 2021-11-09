using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace RFG
{
  [AddComponentMenu("RFG/Platformer/Character/Ability/Attack")]
  public class AttackAbility : MonoBehaviour, IAbility
  {
    [HideInInspector]
    private Character _character;
    private PlayerInventory _playerInventory;
    private InputActionReference _primaryAttackInput;
    private InputActionReference _secondaryAttackInput;
    private bool _pointerOverUi = false;

    private void Awake()
    {
      _character = GetComponent<Character>();
      _playerInventory = GetComponent<PlayerInventory>();
      _primaryAttackInput = _character.InputPack.PrimaryAttackInput;
      _secondaryAttackInput = _character.InputPack.SecondaryAttackInput;
    }

    private void Update()
    {
      _pointerOverUi = EventSystem.current.IsPointerOverGameObject();
    }

    public void OnPrimaryAttackStarted(InputAction.CallbackContext ctx)
    {
      if (!_pointerOverUi)
      {
        _character.MovementState.ChangeState(typeof(PrimaryAttackStartedState));
        WeaponItem leftHand = _playerInventory.Inventory.LeftHand as WeaponItem;
        if (leftHand != null)
        {
          leftHand.Started();
        }
      }
    }

    public void OnPrimaryAttackCanceled(InputAction.CallbackContext ctx)
    {
      _character.MovementState.ChangeState(typeof(PrimaryAttackCanceledState));
      WeaponItem leftHand = _playerInventory.Inventory.LeftHand as WeaponItem;
      if (leftHand != null)
      {
        leftHand.Cancel();
      }
    }

    public void OnPrimaryAttackPerformed(InputAction.CallbackContext ctx)
    {
      _character.MovementState.ChangeState(typeof(PrimaryAttackPerformedState));
      WeaponItem leftHand = _playerInventory.Inventory.LeftHand as WeaponItem;
      if (leftHand != null)
      {
        leftHand.Perform();
      }
    }

    public void OnSecondaryAttackStarted(InputAction.CallbackContext ctx)
    {
      if (!_pointerOverUi)
      {
        _character.MovementState.ChangeState(typeof(SecondaryAttackStartedState));
        WeaponItem rightHand = _playerInventory.Inventory.RightHand as WeaponItem;
        if (rightHand != null)
        {
          rightHand.Started();
        }
      }
    }

    public void OnSecondaryAttackCanceled(InputAction.CallbackContext ctx)
    {
      _character.MovementState.ChangeState(typeof(SecondaryAttackCanceledState));
      WeaponItem rightHand = _playerInventory.Inventory.RightHand as WeaponItem;
      if (rightHand != null)
      {
        rightHand.Cancel();
      }
    }

    public void OnSecondaryAttackPerformed(InputAction.CallbackContext ctx)
    {
      _character.MovementState.ChangeState(typeof(SecondaryAttackPerformedState));
      WeaponItem rightHand = _playerInventory.Inventory.RightHand as WeaponItem;
      if (rightHand != null)
      {
        rightHand.Perform();
      }
    }

    private void OnEnable()
    {

      // Make sure to setup new events
      OnDisable();

      if (_primaryAttackInput != null)
      {
        _primaryAttackInput.action.started += OnPrimaryAttackStarted;
        _primaryAttackInput.action.canceled += OnPrimaryAttackCanceled;
        _primaryAttackInput.action.performed += OnPrimaryAttackPerformed;
      }

      if (_secondaryAttackInput != null)
      {
        _secondaryAttackInput.action.started += OnSecondaryAttackStarted;
        _secondaryAttackInput.action.canceled += OnSecondaryAttackCanceled;
        _secondaryAttackInput.action.performed += OnSecondaryAttackPerformed;
      }
    }

    private void OnDisable()
    {
      if (_primaryAttackInput != null)
      {
        _primaryAttackInput.action.started -= OnPrimaryAttackStarted;
        _primaryAttackInput.action.canceled -= OnPrimaryAttackCanceled;
        _primaryAttackInput.action.performed -= OnPrimaryAttackPerformed;
      }

      if (_secondaryAttackInput != null)
      {
        _secondaryAttackInput.action.started -= OnSecondaryAttackStarted;
        _secondaryAttackInput.action.canceled -= OnSecondaryAttackCanceled;
        _secondaryAttackInput.action.performed -= OnSecondaryAttackPerformed;
      }
    }

  }
}