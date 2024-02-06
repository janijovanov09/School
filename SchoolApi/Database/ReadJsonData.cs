using System.Text.Json;
using SchoolApi.Entities;

namespace SchoolApi.Database;

public class ReadJsonData
{
    private readonly IHostEnvironment _hostEnvironment;

    public ReadJsonData(IHostEnvironment hostEnvironment) 
    {
        _hostEnvironment = hostEnvironment;
    }

    public IList<Course> ReadJson()
    {
        var rootPath = _hostEnvironment.ContentRootPath; //get the root path

        var fullPath = Path.Combine(rootPath, "Data/courses.json"); //combine the root path with that of our json file inside mydata directory

        var jsonData = System.IO.File.ReadAllText(fullPath); //read all the content inside the file

        if (string.IsNullOrWhiteSpace(jsonData)) return null; //if no data is present then return null or error if you wish

        var courses = JsonSerializer.Deserialize<List<Course>>(jsonData); //deserialize object as a list of users in accordance with your json file

        return courses;
    }

}
