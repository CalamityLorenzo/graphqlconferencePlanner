using ConferencePlanner.GraphQL;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Dataloader;
using ConferencePlanner.GraphQL.Speakers;
using ConferencePlanner.GraphQL.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(opts => opts.UseSqlite("DataSource=conferences.db"));

builder.Services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<SpeakerMutations>()
                .AddType<SpeakerType>()
                .AddQueryFieldToMutationPayloads()
                .AddDataLoader<SpeakerByIdDataLoader>()
                .AddDataLoader<SessionByIdDataLoader>();

var app = builder.Build();

if (builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseRouting();
app.UseEndpoints(endpoint =>
{
    endpoint.MapGraphQL();
});

//app.MapGet("/", () => "Hello World!");


app.Run();
