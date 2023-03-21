using Microsoft.Extensions.Configuration;

// specify configuration sources and order of them
IConfigurationBuilder builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddUserSecrets(typeof(Program).Assembly, optional: true);

IConfigurationRoot config = builder.Build();

Console.WriteLine("I think the password is: " + config["Password"]);
