using Structurizr;

namespace TeeLab.C4Model.Components;

public class OrderProcessing
{
    private C4 Project { get; set; }
    private ContextDiagram Context { get; set; }
    private ContainerDiagram Container { get; set; }
    
    private Component Infrastructure { get; set; }
    private Component Application { get; set; }
    private Component Domain { get; set; }
    private Component Interfaces { get; set; }

    public OrderProcessing(ContextDiagram context, ContainerDiagram container, C4 project)
    {
        Project = project ?? throw new ArgumentNullException(nameof(project));
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Container = container ?? throw new ArgumentNullException(nameof(container));
        
        Infrastructure = Container.OrderProcessing.AddComponent("Infrastructure", "Handles infrastructure and management.");
        Application = Container.OrderProcessing.AddComponent("Application", "Handles application and management.");
        Domain = Container.OrderProcessing.AddComponent("Domain", "Handles domain and management.");
        Interfaces = Container.OrderProcessing.AddComponent("Interfaces", "Handles interfaces and management.");
    }

    public void Generate()
    {
        Interfaces.Uses(Application, "Use");
        Application.Uses(Domain, "Use");
        Application.Uses(Infrastructure, "Use");
        
        ApplyStyles();
        Publish();
    }

    public void ApplyStyles()
    {
        
    }
    
    public void Publish()
    {
        ComponentView view = Project.ViewSet.CreateComponentView(Container.OrderProcessing, "Order Processing Component", "");
        view.Title = "Order Processing Component View";
        view.Add(Infrastructure);
        view.Add(Application);
        view.Add(Domain);
        view.Add(Interfaces);
    }
    
}