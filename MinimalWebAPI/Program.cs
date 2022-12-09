using Microsoft.EntityFrameworkCore;
using MinimalWebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

//var todoItems = app.MapGroup("/todoitems"); Need .NET 7.0

app.MapGet("/todoitems", GetAllTodosAsync);

app.MapGet("/todoitems/complete", GetCompleteTodos);

app.MapGet("/todoitems/{id}", GetTodoAsync);

app.MapPost("/todoitems", CreateTodoAsync);

app.MapPut("/todoitems/{id}", UpdateTodoAsync);

app.MapDelete("/todoitems/{id}", DeleteTodoAsync);


app.Run();

static async Task<IResult> GetAllTodosAsync(TodoDb db) => 
    Results.Ok(await db.Todos
    .Select(todoItem => new TodoDTO(todoItem))
    .ToArrayAsync());

static async Task<IResult> GetCompleteTodos(TodoDb db) =>
    Results.Ok(await db.Todos
    .Where(todo => todo.IsComplete)
    .Select(todoItem => new TodoDTO(todoItem))
    .ToArrayAsync());

static async Task<IResult> GetTodoAsync(int id, TodoDb db) =>
    await db.Todos.FindAsync(id)
    is Todo todo
    ? Results.Ok(new TodoDTO(todo))
    : Results.NotFound();

static async Task<IResult> CreateTodoAsync(TodoDTO todoDTO, TodoDb db)
{
    var todo = new Todo
    {
        Id = todoDTO.Id,
        Name = todoDTO.Name,
        IsComplete = todoDTO.IsComplete
    };
    await db.Todos.AddAsync(todo);

    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{todo.Id}", new TodoDTO(todo));
}

static async Task<IResult> UpdateTodoAsync(int id, TodoDTO inputTodoDTO, TodoDb db)
{
    if (id != inputTodoDTO.Id) return Results.BadRequest();

    Todo? todo = await db.Todos.FindAsync(id);
    if (todo == null) return Results.NotFound();

    todo.Name = inputTodoDTO.Name;
    todo.IsComplete = inputTodoDTO.IsComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
}

static async Task<IResult> DeleteTodoAsync(int id, TodoDb db)
{
    if (await db.Todos.FindAsync(id) is Todo todo)
    {
        db.Todos.Remove(todo);
        await db.SaveChangesAsync();
        return Results.Ok(new TodoDTO(todo));
    }

    return Results.NotFound();
}
