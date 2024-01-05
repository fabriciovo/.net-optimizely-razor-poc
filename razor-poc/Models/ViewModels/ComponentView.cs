namespace razor_poc.Models.ViewModels
{
    public class ComponentView
    {
        public ComponentView(string componentName,string componentId, object data)
        {
            Data = data;
            Component = componentName;
            ContainerId = componentId;
        }
        public string Component { get; }
        public string ContainerId { get; }
        public object Data { get; set; }
    }
}
