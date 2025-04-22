using System.ComponentModel;
using Structurizr;
using Component = Structurizr.Component;

namespace TeeLab.C4Model.Components;

public class ApiComponent
{
    private C4 Project { get; set; }
    private ContextDiagram Context { get; set; }
    private ContainerDiagram Container { get; set; }
    
    public Component OrderProcessing { get; set; }
    public Component DesignStudio { get; set; }
    public Component PaymentGateway { get; set; }
    public Component OrderFulfillment { get; set; }
    public Component ProductCatalog { get; set; }
    public Component UserManagement { get; set; }
    
    public Component Shared { get; set; }



    public ApiComponent(ContextDiagram context, ContainerDiagram container, C4 project)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Container = container ?? throw new ArgumentNullException(nameof(container));
        Project = project ?? throw new ArgumentNullException(nameof(project));
        
        OrderProcessing = Container.Api.AddComponent("Order Processing", "Handles order processing and management.");
        OrderProcessing.AddTags(nameof(OrderProcessing));
        DesignStudio = Container.Api.AddComponent("Design Studio", "Handles design studio and management.");
        DesignStudio.AddTags(nameof(DesignStudio));
        PaymentGateway = Container.Api.AddComponent("Payment Gateway", "Handles payment processing and management.");   
        PaymentGateway.AddTags(nameof(PaymentGateway));
        OrderFulfillment = Container.Api.AddComponent("Order Fulfillment", "Handles order fulfillment and management.");
        OrderFulfillment.AddTags(nameof(OrderFulfillment));
        ProductCatalog = Container.Api.AddComponent("Product Catalog", "Handles product catalog and management.");
        ProductCatalog.AddTags(nameof(ProductCatalog));
        UserManagement = Container.Api.AddComponent("User Management", "Handles user management and authentication.");
        UserManagement.AddTags(nameof(UserManagement));
        
        Shared = Container.Api.AddComponent("Shared", "Handles shared components and management.");
        Shared.AddTags(nameof(Shared));
        
    }

    public void Generate()
    {
        OrderFulfillment.Uses(OrderProcessing, "Use");
        OrderProcessing.Uses(UserManagement, "Use");
        OrderProcessing.Uses(ProductCatalog, "Use");
        OrderProcessing.Uses(PaymentGateway, "Use");
        DesignStudio.Uses(UserManagement, "Use");

        OrderProcessing.Uses(Shared, "Use");
        DesignStudio.Uses(Shared, "Use");
        OrderFulfillment.Uses(Shared, "Use");
        ProductCatalog.Uses(Shared, "Use");
        UserManagement.Uses(Shared, "Use");
        PaymentGateway.Uses(Shared, "Use");

        PaymentGateway.Uses(Context.Stripe, "Use");
        DesignStudio.Uses(Context.Cloudinary, "Use");
        ProductCatalog.Uses(Context.Cloudinary, "Use");

        ApplyStyles();
        Publish();
    }

    public void ApplyStyles()
    {
        var styles = Project.ViewSet.Configuration.Styles;
        styles.Add(new ElementStyle(Tags.Component) {Background = "#FFC142", Shape = Shape.Component});
    }

    public void Publish()
    {
        var view = Project.ViewSet.CreateComponentView(Container.Api, "TeeLab API Component View", "");
        view.AddAllComponents();
        view.Add(Context.Cloudinary);
        view.Add(Context.Stripe);
    }
}   