using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions{

	[Category("Player")]
	public class EnablePlayerInteraction : ActionTask{

		public PlayerInteractionHandler playerInteraction;
		public bool enable;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute(){
			playerInteraction.enabled = enable;
			EndAction(true);
		}
	}
}