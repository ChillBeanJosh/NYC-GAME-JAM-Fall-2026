using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    
    [Header("Refrences:")]
    [SerializeField] private Gameplay _inputActions;
	[SerializeField] private PlayerMovement _movementLogic;
	
	[SerializeField] private float moveAcceleration;
	
	
	
	private void Start()
	{
		//Initial
		_inputActions = new Gameplay();
		_inputActions.Enable();
		
		
	}
   
    
    
}
