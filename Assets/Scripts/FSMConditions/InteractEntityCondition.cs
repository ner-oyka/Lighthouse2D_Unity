using NodeCanvas.Framework;
using ParadoxNotion.Design;
using EventBusSystem;


namespace NodeCanvas.Tasks.Conditions{

	[Category("Player")]
	public class InteractEntityCondition : ConditionTask, IEntityHandler, IMouseInputHandler
	{
        private bool isInteracted = false;

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

        public void OnTakeToInventory(Entity entity)
        {

        }

        public void OnWeaponHit(in Entity entity, float value)
        {

        }

        public void OnKillEntity(Entity entity)
        {

        }

        public void OnShowDescription(string description)
        {

        }

        public void OnHideDescription()
        {

        }

        public void OnShowActionMenu(in Entity entity)
        {
            isInteracted = true;
            YieldReturn(true);
        }

        public void OnMouseLeftDown()
        {
            if (isInteracted)
            {
                isInteracted = false;
                YieldReturn(true);
            }
        }

        public void OnMouseLeftUp()
        {

        }

        public void OnMouseRightDown()
        {

        }

        public void OnMouseRightUp()
        {

        }

        public void OnMouseWheelUp()
        {

        }

        public void OnMouseWheelDown()
        {

        }
    }
}