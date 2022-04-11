using ConferencePlanner.GraphQL;
using ConferencePlanner.GraphQL.Data;
using ConferencePlanner.GraphQL.Dataloader;
using ConferencePlanner.GraphQL.Sessions;
using ConferencePlanner.GraphQL.Speakers;
using ConferencePlanner.GraphQL.Tracks;
using ConferencePlanner.GraphQL.Types;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(opts => opts.UseSqlite("DataSource=conferences.db"));

builder.Services.AddGraphQLServer()
                .AddQueryFieldToMutationPayloads()
                .AddGlobalObjectIdentification()
                .AddQueryType(d => d.Name("Query"))
                .AddTypeExtension<SpeakersQueries>()
                .AddTypeExtension<SessionQueries>()
                .AddTypeExtension<TrackQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                .AddTypeExtension<SpeakerMutations>()
                .AddTypeExtension<SessionMutation>()
                .AddTypeExtension<TracksMutation>()
                .AddType<AttendeeType>()
                .AddType<SessionType>()
                .AddType<SpeakerType>()
                .AddType<TrackType>()
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
