using Arshid.Aspire.ApiDocs.Extensions;

var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", true);

var sqlserver = builder.AddSqlServer("sql",sqlPassword, 53032)
    .WithDataVolume();

var authDb = sqlserver.AddDatabase("authDb");
var orderDb = sqlserver.AddDatabase("orderDb");
var customerDb = sqlserver.AddDatabase("customerDb");

var keyCloak = builder.AddKeycloak("keyCloak", 8089)
    .WithDataVolume()
    .WithExternalHttpEndpoints();

var authApi = builder.AddProject<Projects.Yarp_Microservices_Auth_Api>("auth-api")
    .WithScalar()
    .WithSwagger()
    .WithReference(authDb)
    .WaitFor(sqlserver)
    .WaitFor(authDb);

var orderApi = builder.AddProject<Projects.Yarp_Microservices_Orders_Api>("order-api")
    .WithScalar()
    .WithSwagger()
    .WithReference(orderDb)
    .WaitFor(sqlserver)
    .WaitFor(orderDb);

var customerApi = builder.AddProject<Projects.Yarp_Microservices_Customers_Api>("customer-api")
    .WithScalar()
    .WithSwagger()
    .WithReference(customerDb)
    .WaitFor(sqlserver)
    .WaitFor(customerDb);

 var orchestrator = builder.AddProject<Projects.Yarp_Microservices_Orchestrators>("orchestrator-api")
    .WithScalar()
    .WithSwagger()
    .WaitFor(sqlserver)
    .WaitFor(customerDb)
    .WaitFor(orderDb);

builder.AddProject<Projects.Yarp_Microservice_Gateway>("api-gateway")
    .WithExternalHttpEndpoints()
    .WithReference(authApi);

builder.Build().Run();
