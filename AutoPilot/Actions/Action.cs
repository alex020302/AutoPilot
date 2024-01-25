namespace AutoPilot
{
    public class Action
    {
        public string Comment { get; set; }
        public string ActionType;

        public virtual void Execute()
        {

        }

        //Wird automatisch von der Listbox verwendet
        public override string ToString()
        {
            return "";
        }
    }
}
