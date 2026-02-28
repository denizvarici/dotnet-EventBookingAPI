// See https://aka.ms/new-console-template for more information
Console.WriteLine("BCRYPT password hash results");

Console.WriteLine("Seed Admin Password Hashed:");
string hashedAdminPassword = BCrypt.Net.BCrypt.HashPassword("Admin123.");
Console.WriteLine(hashedAdminPassword);

Console.WriteLine("Seed User Password Hashed:");
string hashedUserPassword = BCrypt.Net.BCrypt.HashPassword("User123.");
Console.WriteLine(hashedUserPassword);
