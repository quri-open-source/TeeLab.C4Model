using Structurizr;
using Structurizr.Api;
using TeeLab.C4Model.Components;

namespace TeeLab.C4Model;

public class C4
{
    private string ApiKey { set; get; }
    private string ApiSecret { set; get; }
    private long WorkspaceId { set; get; }
    private string WorkspaceName { set; get; }
    private string WorkspaceDescription { set; get; }

    public StructurizrClient Project { get; set; }
    public Workspace Workspace { get; set; }
    public Model Model { get; set; }
    public ViewSet ViewSet { get; set; }
    
    public C4()
    {
        ApiKey = "f9f9ec3d-ce88-4c21-ba44-1dc7877243d4";
        ApiSecret = "57e7eac0-da22-4b4d-b090-3cd86a3b2677";
        WorkspaceId = 101506;
        WorkspaceName = "TeeLab - Open Source";
        WorkspaceDescription = "TeeLav - C4 Model";

        Project = new StructurizrClient(ApiKey, ApiSecret);
        Workspace = new Workspace(WorkspaceName, WorkspaceDescription);
        Model = Workspace.Model;
        ViewSet = Workspace.Views;
    }

    public void Generate()
    {
        var context = new ContextDiagram(this);
        context.Generate();
        var container = new ContainerDiagram(context, this);
        container.Generate();
        
        var api = new ApiComponent(context, container, this);
        api.Generate();
        var orderProcessing = new OrderProcessing(context, container, this);
        orderProcessing.Generate();
        var designStudio = new DesignStudio(context, container, this);
        designStudio.Generate();
        var paymentGateway = new PaymentGateway(context, container, this);
        paymentGateway.Generate();
        var orderFulfillment = new OrderFulfillment(context, container, this);
        orderFulfillment.Generate();
        var productCatalog = new ProductCatalog(context, container, this);
        productCatalog.Generate();
        var userManagement = new UserManagement(context, container, this);
        userManagement.Generate();
        
        Project.PutWorkspace(WorkspaceId, Workspace);
    }
    
}