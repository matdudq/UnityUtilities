using System;
using System.Collections.Generic;

namespace Utilities.StateMachine
{
	/// <summary>
	/// Class which stores states, changing states and sharing access to them. 
	/// </summary>
	public class StateMachine
	{
		#region Properties

		/// <summary>
		/// Actual state of state machine
		/// </summary>
		public State CurrentState { get; private set; }

		/// <summary>
		/// Dictionary which defines all of object states.
		/// Correct generics for dictionary:
		/// <typeof(state), state>
		/// </summary>
		protected Dictionary<Type, State> States { get; }

		#endregion Properties

		#region Public functions

		/// <summary>
		/// Constructor to define states Dictionary based on pre-created dictionary
		/// </summary>
		/// <param name="states">all states defined for object</param>
		/// <param name = "initialState" >type of state that should be set up as default.</param>
		public StateMachine(Dictionary<Type, State> states, Type initialState = null)
		{
			this.States = states;

			if (initialState != null)
			{
				ChangeState(initialState);
			}
		}

		/// <summary>
		/// Constructor to define states Dictionary based on pre-created collection
		/// </summary>
		/// <param name="states">all states defined for object</param>
		/// <param name = "initialState" >type of state that should be set up as default.</param>
		public StateMachine(ICollection<State> states, Type initialState = null)
		{
			this.States = new Dictionary<Type, State>();

			foreach (State state in states)
			{
				States.Add(state.GetType(), state);
			}

			if (initialState != null)
			{
				ChangeState(initialState);
			}
		}

		/// <summary>
		/// Function which changes actual object state
		/// </summary>
		/// <param name="state">Type of state we want to change.</param>
		public virtual void ChangeState(Type state)
		{
			if (States.ContainsKey(state))
			{
				ChangeState(States[state]);
			}
			else
			{
				GameConsole.LogError(this, "State:" + state.ToString() + " doesn't exist!");
			}
		}

		public virtual State GetState(Type state)
		{
			if (States.ContainsKey(state))
			{
				return States[state];
			}
			else
			{
				GameConsole.LogError(this, "State:" + state.ToString() + " doesn't exist!");
				return null;
			}
		}

		#endregion Public functions

		#region Private functions

		private void ChangeState(State newState)
		{
			if (CurrentState == newState)
			{
				return;
			}

			CurrentState?.Exit();

			CurrentState = newState;

			CurrentState?.Enter();
		}

		#endregion Private functions
	}
}