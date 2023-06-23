var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureApplicationServices()
    .Build();

host.Run();