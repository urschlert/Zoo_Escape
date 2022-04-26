namespace ZooBreakout.Data
{

    /*
        This class is a service for the drag and drop
        functionality for the second round of gameplay
        and infinite mode.
    */

    public class DragAndDropService
    {
        public object Data { get; set; }
        public string Zone { get; set; }

        // stores the object and zone of where the drag began
        public void StartDrag(object data, string zone)
        {
            this.Data = data;
            this.Zone = zone;
        }

        // determines if a zone is compatable
        public bool Accepts(string zone)
        {
            return this.Zone == zone;
        }
    }
}