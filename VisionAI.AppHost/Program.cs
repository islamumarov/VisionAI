var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.VisionAI>("visionai");

builder.Build().Run();
