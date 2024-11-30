var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.VisionAI>("visionai");
var ollama = builder.AddOllama("ollama").WithDataVolume().WithOpenWebUI();

var llama = ollama.AddModel("llama3.2-vision");

builder.AddProject<Projects.VisionAI>("api").WithReference(llama);

builder.Build().Run();
