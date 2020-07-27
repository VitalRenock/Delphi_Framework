namespace SM_Stuff
{
	public abstract class SM_BaseState<T>
	{
		public abstract void EnterState(T agent);
		public abstract void UpdateState(T agent);
		public abstract void ExitState(T agent);
	}
}