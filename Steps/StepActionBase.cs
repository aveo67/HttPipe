using System;

namespace HttPipe
{
	internal abstract class StepActionBase<TFunc>
		where TFunc : Delegate
	{
		protected readonly TFunc _action;

		public StepActionBase(TFunc action)
		{
			_action = action ?? throw new ArgumentNullException("Action must not be null");
		}
	}
}
