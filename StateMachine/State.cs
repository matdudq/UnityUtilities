using System;

namespace Utilities
{
	/// <summary>
	/// Abstract state class for state machine use	
	/// </summary>
	public abstract class State
	{
		#region Events

		/// <summary>
		/// Invoked on state enter
		/// </summary>
		public event Action OnStateEnter ;
		
		/// <summary>
		/// Invoked on state update
		/// </summary>
		public event Action OnStateUpdate ;
		
		/// <summary>
		/// Invoked on state exit
		/// </summary>
		public event Action OnStateExit ;

		#endregion Events
		
		#region Public Functions

		/// <summary>
		/// Called when object entering state
		/// </summary>
		public virtual void Enter()
		{
			OnStateEnter?.Invoke();
		}
		
		/// <summary>
		/// Called each frame when updating logic
		/// </summary>
		public virtual void LogicUpdate()
		{
			OnStateUpdate?.Invoke();
		}
		
		/// <summary>
		/// Called when object changing state
		/// </summary>
		public virtual void Exit()
		{
			OnStateExit?.Invoke();
		}

		#endregion Public Functions
	}
}