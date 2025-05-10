using Structurizr;
using Component = Structurizr.Component;

namespace TeeLab.C4Model.Components;

public class ApiComponent
{
    private C4 Project { get; }
    private ContextDiagram Context { get; }
    private ContainerDiagram Container { get; }

    public Component DesignStudio { get; set; }
    public Component BlueprintManagement { get; set; }
    public Component ProductCatalog { get; set; }
    public Component OrderProcessing { get; set; }
    public Component OrderFulfillment { get; set; }
    public Component PaymentGateway { get; set; }
    public Component UserManagement { get; set; }
    public Component AccessAndSecurity { get; set; }
    public Component Shared { get; set; }

    public ApiComponent(ContextDiagram context, ContainerDiagram container, C4 project)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Container = container ?? throw new ArgumentNullException(nameof(container));
        Project = project ?? throw new ArgumentNullException(nameof(project));

        DesignStudio = Container.Api.AddComponent(
            "Design Studio",
            "Allows Garment Designers to create visual products (Blueprints), add layers (text/images), configure them, and preview them.",
            "Spring Boot - Java 24");

        BlueprintManagement = Container.Api.AddComponent(
            "Blueprint Management",
            "Handles reusable design blueprints: save, load, publish, duplicate, rename, and delete.",
            "Spring Boot - Java 24");

        ProductCatalog = Container.Api.AddComponent(
            "Product Catalog",
            "Displays finished products for users to explore, filter, and select what to buy.",
            "Spring Boot - Java 24");

        OrderProcessing = Container.Api.AddComponent(
            "Order Processing",
            "Manages checkout and order lifecycle including validation and preparation.",
            "Spring Boot - Java 24");

        OrderFulfillment = Container.Api.AddComponent(
            "Order Fulfillment",
            "Handles production, packaging, and delivery after order processing.",
            "Spring Boot - Java 24");

        PaymentGateway = Container.Api.AddComponent(
            "Payment Gateway",
            "Processes payments, validates transactions, and generates receipts.",
            "Spring Boot - Java 24");

        UserManagement = Container.Api.AddComponent(
            "User Management",
            "Manages user accounts, roles, and profile data.",
            "Spring Boot - Java 24");

        AccessAndSecurity = Container.Api.AddComponent(
            "Access & Security",
            "Manages authentication, tokens, and route protection.",
            "Spring Boot - Java 24");

        Shared = Container.Api.AddComponent(
            "Shared",
            "Cross-cutting logic shared across modules (e.g., validation, logging).",
            "Spring Boot - Java 24");
    }

    public void Generate()
    {
        // Internal RESTful routes
        DesignStudio.Uses(BlueprintManagement, "Calls /api/blueprints");
        DesignStudio.Uses(UserManagement, "Calls /api/users/current");
        DesignStudio.Uses(Shared, "Calls /api/shared/design");

        BlueprintManagement.Uses(Shared, "Calls /api/shared/blueprints");

        ProductCatalog.Uses(Shared, "Calls /api/shared/products");
        ProductCatalog.Uses(Context.Cloudinary, "Calls Cloudinary API");

        OrderProcessing.Uses(ProductCatalog, "Calls /api/products");
        OrderProcessing.Uses(UserManagement, "Calls /api/users/{id}");
        OrderProcessing.Uses(PaymentGateway, "Calls /api/payment/checkout");
        OrderProcessing.Uses(Shared, "Calls /api/shared/orders");

        OrderFulfillment.Uses(OrderProcessing, "Calls /api/orders/{id}/status");
        OrderFulfillment.Uses(Shared, "Calls /api/shared/fulfillment");

        PaymentGateway.Uses(Context.Stripe, "Calls Stripe API");
        PaymentGateway.Uses(Shared, "Calls /api/shared/payments");

        UserManagement.Uses(Shared, "Calls /api/shared/users");

        AccessAndSecurity.Uses(UserManagement, "Calls /api/users/validate");

        // Supabase usage for persistence
        BlueprintManagement.Uses(Context.Supabase, "Stores blueprint metadata", "SQL via Supabase");
        ProductCatalog.Uses(Context.Supabase, "Reads catalog", "SQL via Supabase");
        OrderProcessing.Uses(Context.Supabase, "Stores order state", "SQL via Supabase");
        OrderFulfillment.Uses(Context.Supabase, "Reads logistics data", "SQL via Supabase");
        UserManagement.Uses(Context.Supabase, "Stores user profiles", "SQL via Supabase");
        AccessAndSecurity.Uses(Context.Supabase, "Uses Supabase Auth", "JWT / OAuth2");
        PaymentGateway.Uses(Context.Supabase, "Stores transaction data", "SQL via Supabase");

        ApplyStyles();
        Publish();
    }

    public void ApplyStyles()
    {
        var styles = Project.ViewSet.Configuration.Styles;
        styles.Add(new ElementStyle(Tags.Component)
        {
            Background = "#F5A623",
            Shape = Shape.Component,
            Color = "#000000"
        });
    }

    public void Publish()
    {
        var view = Project.ViewSet.CreateComponentView(Container.Api, "TeeLab API Component View", "Component diagram for TeeLab API layer.");
        view.AddAllComponents();
        view.Add(Context.Cloudinary);
        view.Add(Context.Supabase);
        view.Add(Context.Stripe);
    }
}
