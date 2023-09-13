// Початкові дані
List<Person> users = new List<Person>
{
     new() { Id = Guid.NewGuid().ToString(), Name = "Tom", Age = 37},
     new() { Id = Guid.NewGuid().ToString(), Name = "Bob", Age = 41},
     new() { Id = Guid.NewGuid().ToString(), Name = "Sam", Age = 24}
};
 
var builder = WebApplication.CreateBuilder();
var app = builder.Build();
 
app.UseDefaultFiles();
app.UseStaticFiles();
 
app.MapGet("/api/users", ()=>Results.Ok( users));
 
app.MapGet("/api/users/{id}", (string id) =>
{
     // отримуємо користувача за id
     Person? user = users.FirstOrDefault(u => u.Id == id);
     // якщо не знайдено, надсилаємо статусний код та повідомлення про помилку
     if (user == null) return Results.NotFound(new { message = "Користувач не знайдений"});
 
     // якщо користувач знайдено, відправляємо його
     return Results.Json(user);
});
 
app.MapDelete("/api/users/{id}", (string id) =>
{
     // отримуємо користувача за id
     Person? user = users.FirstOrDefault(u => u.Id == id);
 
     // якщо не знайдено, надсилаємо статусний код та повідомлення про помилку
     if (user == null) return Results.NotFound(new { message = "Користувач не знайдений"});
 
     // якщо користувач знайдено, видаляємо його
     users.Remove(user);
     return Results.Json(user);
});
 
app.MapPost("/api/users", (Person user)=>{
 
     // встановлюємо id для нового користувача
     user.Id = Guid.NewGuid().ToString();
     // додаємо користувача до списку
     users.Add(user);
     return user;
});
 
app.MapPut("/api/users", (Person userData) => {
 
     // отримуємо користувача за id
     var user = users.FirstOrDefault(u => u.Id == userData.Id);
     // якщо не знайдено, надсилаємо статусний код та повідомлення про помилку
     if (user == null) return Results.NotFound(new { message = "Користувач не знайдений"});
     // якщо користувач знайдено, змінюємо його дані та відправляємо назад клієнту
     
     user.Age = userData.Age;
     user.Name = userData.Name;
     return Results.Json(user);
});
 
app.Run();
 
public class Person
{
     public string Id {get; set; } = "";
     public string Name { get; set; } = "";
     public int Age {get; set; }
}