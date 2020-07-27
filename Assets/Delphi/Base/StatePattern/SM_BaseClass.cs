namespace SM_Stuff
{
	public class SM_BaseClass<T>
	{
		public SM_BaseState<T> currentState { get; private set; }
		public T Agent;

		public SM_BaseClass(T agent)
		{
			Agent = agent;
			currentState = null;
		}

		public void ChangeState(SM_BaseState<T> newState)
		{
			if (currentState != null)
				currentState.ExitState(Agent);
			currentState = newState;
			currentState.EnterState(Agent);
		}

		public void Update()
		{
			if (currentState != null)
				currentState.UpdateState(Agent);
		}
	}
}