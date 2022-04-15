using NodeCanvas.Framework;
using ParadoxNotion.Design;
using EventBusSystem;


namespace NodeCanvas.Tasks.Conditions
{

	[Category("Player")]
	public class InventoryCondition : ConditionTask, IInventoryHandler
	{

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable(){
			EventBus.Subscribe(this);
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable(){
			EventBus.Unsubscribe(this);
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck(){
			return false;
		}

        public void OnOpenInventory()
        {
			YieldReturn(true);
        }

        public void OnCloseInventory()
        {
			YieldReturn(true);
		}
    }
}