using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

public class PublishingHouse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
}

public class Book
{
    public int PublishingHouseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public PublishingHouse PublishingHouse { get; set; } = new();
}

public class BookOut
{
    [JsonIgnore]
    public int PublishingHouseId { get; set; }

    [JsonPropertyName("Name")]
    public string Title { get; set; } = string.Empty;

    public PublishingHouse PublishingHouse { get; set; } = new();
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string json = File.ReadAllText("books.json");
        List<Book> books = JsonSerializer.Deserialize<List<Book>>(json) ?? new();

        foreach (var b in books)
            Console.WriteLine($"{b.Title} — {b.PublishingHouse.Name}");

        var output = books.Select(b => new BookOut
        {
            PublishingHouseId = b.PublishingHouseId,
            Title = b.Title,
            PublishingHouse = b.PublishingHouse
        }).ToList();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        Console.WriteLine(JsonSerializer.Serialize(output, options));
    }
}