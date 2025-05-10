using Structurizr;

namespace TeeLab.C4Model;

public class ContainerDiagram
{
    private C4 Project { get; set; }
    private ContextDiagram Context { get; set; }
    
    public Container LandingPage { get; set; }
    public Container WebApp { get; set; }
    public Container Api { get; set; }
    
    public Container OrderProcessing { get; set; }
    public Container DesignStudio { get; set; }
    public Container PaymentGateway { get; set; }
    public Container OrderFulfillment { get; set; }
    public Container ProductCatalog { get; set; }
    public Container UserManagement { get; set; }
    
    public Container SinglePageApplication { get; set; }

    
    public ContainerDiagram(ContextDiagram context, C4 project)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Project = project ?? throw new ArgumentNullException(nameof(project));
        
        LandingPage = Context.TeeLab.AddContainer("Landing Page", "The landing page of TeeLab.", "HTML, CSS, JS");
        LandingPage.AddTags(nameof(LandingPage));
        WebApp = Context.TeeLab.AddContainer("Web App", "The web app of TeeLab.", "Angular 19");
        WebApp.AddTags(nameof(WebApp));
        Api = Context.TeeLab.AddContainer("API", "The API of TeeLab.", "Spring Boot - Java 24");
        Api.AddTags(nameof(Api));
        
        OrderProcessing = Context.TeeLab.AddContainer("Order Processing", "Handles order processing and management.", "Spring Boot - Java 24");
        OrderProcessing.AddTags(nameof(OrderProcessing));
        DesignStudio = Context.TeeLab.AddContainer("Design Studio", "Handles design studio and management.", "Spring Boot - Java 24");
        DesignStudio.AddTags(nameof(DesignStudio));
        PaymentGateway = Context.TeeLab.AddContainer("Payment Gateway", "Handles payment processing and management.", "Spring Boot - Java 24");
        PaymentGateway.AddTags(nameof(PaymentGateway));
        OrderFulfillment = Context.TeeLab.AddContainer("Order Fulfillment", "Handles order fulfillment and management.", "Spring Boot - Java 24");
        OrderFulfillment.AddTags(nameof(OrderFulfillment));
        ProductCatalog = Context.TeeLab.AddContainer("Product Catalog", "Handles product catalog and management.", "Spring Boot - Java 24");
        ProductCatalog.AddTags(nameof(ProductCatalog));
        UserManagement = Context.TeeLab.AddContainer("User Management", "Handles user management and authentication.", "Spring Boot - Java 24");
        UserManagement.AddTags(nameof(UserManagement));
        
        SinglePageApplication = Context.TeeLab.AddContainer("Single Page Application", "Provides core functionality via the browser.", "Angular 19, NGX-Translate, Angular Material");
        SinglePageApplication.AddTags(nameof(SinglePageApplication));

    }

    public void Generate()
    {
        Context.User.Uses(LandingPage, "Use TeeLab to buy their clothes.");
        Context.Designer.Uses(WebApp, "Use TeeLab to design their clothes.");
        Context.Manufacturer.Uses(WebApp, "Use TeeLab to manufacture their clothes.");
        
        Api.Uses(Context.Cloudinary, "Use Cloudinary to create a cloud service to manage images.");
        Api.Uses(Context.Stripe, "Use Stripe to create a cloud service to manage payments.");
        Api.Uses(Context.Supabase, "Use Supabase to create a cloud service to manage data.");
        
        LandingPage.Uses(WebApp, "Redirect to the web app.");
        WebApp.Uses(SinglePageApplication , "Use the SPA to manage the data.");
        SinglePageApplication.Uses(Api, "Use the API to manage the data.");
        
        
        ApplyStyles();
        Publish();
    }

    private void ApplyStyles()
    {
        var styles = Project.ViewSet.Configuration.Styles;
        
        styles.Add(new ElementStyle(nameof(LandingPage)) {Background = "#006A1C", Shape = Shape.RoundedBox, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(WebApp)) {Background = "#0000E4", Shape = Shape.RoundedBox, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(Api)) {Background = "#FF0D17", Shape = Shape.RoundedBox, Color = "#FFFFFF"});
        styles.Add(new ElementStyle(nameof(SinglePageApplication)) { Background = "#438dd5", Shape = Shape.WebBrowser, Color = "#FFFFFF" });

    }
    
    private void Publish()
    {
        ContainerView view = Project.ViewSet.CreateContainerView(Context.TeeLab, "TeeLab Container View", "");
        view.AddAllSoftwareSystems();
        view.AddAllPeople();
        view.Add(LandingPage);
        view.Add(WebApp);
        view.Add(Api);
        view.Add(SinglePageApplication);
    }
}