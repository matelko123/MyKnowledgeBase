using MyKnowledgeBase.Optimization.Pagination.PaginatedList;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var Users = Enumerable.Range(1, 100)  // Generating 100 users
            .Select(i => new User { Id = Guid.NewGuid(), Age = i });

app.MapGet("/users", (
    [AsParameters] GetAllUsersRequest request) =>
{
    // If you using EF just do: usersQuerable = _dbContext.Users;
    // IMPORTANT!! DO NOT USE .ToList() !! - this will immediately retrieve the entire data set from the database, which can be inefficient if the table contains a large number of records.
    IQueryable<User> usersQuerable = Users.AsQueryable();

    // Filter users by Age if is set in parameters
    if (request.MinimumAge.HasValue)
    {
        usersQuerable = usersQuerable.Where(x => x.Age > request.MinimumAge!);
    }

    // If you're using EF, it's best to use ToPaginatedListAsync and pass the CancellationToken to method
    var paginatedUser = usersQuerable.ToPaginatedList(request);
    return Results.Ok(paginatedUser);
});

app.Run();

public class User
{
    public Guid Id { get; set; }
    public int Age { get; set; }
}

public class GetAllUsersRequest : PaginatedListRequest
{
    public int? MinimumAge { get; init; } = null;
}