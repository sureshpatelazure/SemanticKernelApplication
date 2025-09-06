using PizzaOrderPlugin.Plugin;
using SemanticKernelCore.KernelCore;
using SemanticKernelCore.McpServerCore;
using SemanticKernelCore.Plugin;

//////// Kernel and Plugin Initialization ////////
IKernelService kernelService = new KernelService();
kernelService.CreatekernelBuilder();

IMcpServerService mcpServerService = new McpServerService();
mcpServerService.KernelService = kernelService; 

List<Object> Plugins = new List<object>();
Plugins.Add(new PizzaPlugin());
mcpServerService.AddTool(Plugins);

kernelService.BuildKernel();

//////// ASP.NET Core Web Application ////////  
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMcpServer()
    .WithHttpTransport()
    .WithTools(kernelService.Kernel.Plugins);

var app = builder.Build();

app.MapMcp();
app.Run();
