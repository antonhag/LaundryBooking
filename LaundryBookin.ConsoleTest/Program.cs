using LaundryBooking.Core.Enums;
using LaundryBooking.Core.Models;
using LaundryBooking.Core.Repositories;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var connectionString = config["MongoDb:ConnectionString"]!;

var housingCooperativeRepo = new MongoHousingCooperativeRepository(connectionString);
var bookingRepo = new MongoBookingRepository(connectionString);
var issueRepo = new MongoIssueRepository(connectionString);

// Create a test booking
var testBooking = new Booking
{
    HousingCooperativeId = "test-cooperative-id",
    ApartmentNumber = "101",
    Date = DateOnly.FromDateTime(DateTime.Today),
    TimeSlot = TimeSlot.Morning
};

await bookingRepo.CreateBookingAsync(testBooking);
Console.WriteLine($"Created booking (Id: {testBooking.Id})");

// Create a test issue report
var testIssue = new IssueReport
{
    HousingCooperativeId = "test-cooperative-id",
    ApartmentNumber = "101",
    Description = "Washing machine is broken",
    Status = IssueStatus.Open
};

await issueRepo.CreateIssueAsync(testIssue);
Console.WriteLine($"Created issue report (Id: {testIssue.Id})");

// Fetch and print bookings
var bookings = await bookingRepo.GetBookingsByDateAsync(DateOnly.FromDateTime(DateTime.Today));
Console.WriteLine($"\nBookings today: {bookings.Count}");
foreach (var b in bookings)
{
    Console.WriteLine($"- Apartment {b.ApartmentNumber}, {b.TimeSlot} (Id: {b.Id})");
}

// Fetch and print issues
var issues = await issueRepo.GetAllIssuesAsync();
Console.WriteLine($"\nIssue reports: {issues.Count}");
foreach (var i in issues)
{
    Console.WriteLine($"- {i.Description}, Status: {i.Status} (Id: {i.Id})");
}
