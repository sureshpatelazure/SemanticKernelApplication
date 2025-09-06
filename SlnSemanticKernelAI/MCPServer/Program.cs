using PizzaOrderPlugin.Plugin;
using SemanticKernelCore.KernelCore;
using SemanticKernelCore.McpServer;
using SemanticKernelCore.Plugin;

//////// Kernel and Plugin Initialization ////////
IKernelService kernelService = new KernelService();
kernelService.CreatekernelBuilder();

IPLuginObject pLuginObject = new PLuginObject();
pLuginObject.KernelService = kernelService;

List<Object> Plugins = new List<object>();
Plugins.Add(new PizzaPlugin());
pLuginObject.AddPluginObject(Plugins);

kernelService.BuildKernel();

//////// ASP.NET Core Web Application ////////  
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools(kernelService.Kernel.Plugins);

var app = builder.Build();

app.MapMcp();
app.Run();
