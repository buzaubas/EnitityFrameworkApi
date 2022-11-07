using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Globalization;
using System.Text.Json;

namespace EnitityFrameworkApi
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://randomuser.me/api/")
            };

            var response = await httpClient.GetStringAsync("");
            var obj = JsonSerializer.Deserialize<Rootobject>(response);
            await using var dbContext = new PersonDbContext();
            await using var tran = await dbContext.Database.BeginTransactionAsync();
            try
            {
                var nullabaleResult = obj.results[0];

                var personAddress = AddressCreate(nullabaleResult);

                dbContext.peopleAddresses.Add(personAddress);
                await dbContext.SaveChangesAsync();

                var person = PersonCreate(nullabaleResult);
                person.personAddressId = personAddress.id;

                dbContext.people.Add(person);
                await dbContext.SaveChangesAsync();

                await tran.CommitAsync();
                Console.WriteLine("ALL PROCESSES WORK FINE");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
            
        }

        static Person PersonCreate(Result rs)
        {
            var person = new Person();
            person.firstName = rs.name.first;
            person.lastName = rs.name.last;
            person.gender = rs.gender;
            person.address = rs.location.street + " " + rs.location.street.name;
            person.email = rs.email;    
            person.login = rs.login.username;
            person.password = rs.login.password;
            person.phone = rs.phone;
            person.pictureUrlLarge = rs.picture.large;
            return person;
        }

        static PersonAddress AddressCreate(Result rs)
        {
            var personAddress = new PersonAddress();
            personAddress.city = rs.location.city;
            personAddress.state = rs.location.state;
            personAddress.country = rs.location.country;
            personAddress.post = rs.location.postcode;
            personAddress.latitude = double.Parse(rs.location.coordinates.latitude, CultureInfo.InvariantCulture);
            personAddress.longitude = double.Parse(rs.location.coordinates.longitude, CultureInfo.InvariantCulture);
            return personAddress;
        }
    }


    public class Rootobject
    {
        public Result[] results { get; set; }
        public Info info { get; set; }
    }

    public class Info
    {
        public string seed { get; set; }
        public int results { get; set; }
        public int page { get; set; }
        public string version { get; set; }
    }

    public class Result
    {
        public string gender { get; set; }
        public Name name { get; set; }
        public Location location { get; set; }
        public string email { get; set; }
        public Login login { get; set; }
        public Dob dob { get; set; }
        public Registered registered { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }
        public Id id { get; set; }
        public Picture picture { get; set; }
        public string nat { get; set; }
    }

    public class Name
    {
        public string title { get; set; }
        public string first { get; set; }
        public string last { get; set; }
    }

    public class Location
    {
        public Street street { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public int postcode { get; set; }
        public Coordinates coordinates { get; set; }
        public Timezone timezone { get; set; }
    }

    public class Street
    {
        public int number { get; set; }
        public string name { get; set; }
    }

    public class Coordinates
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
    }

    public class Timezone
    {
        public string offset { get; set; }
        public string description { get; set; }
    }

    public class Login
    {
        public string uuid { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public string md5 { get; set; }
        public string sha1 { get; set; }
        public string sha256 { get; set; }
    }

    public class Dob
    {
        public DateTime date { get; set; }
        public int age { get; set; }
    }

    public class Registered
    {
        public DateTime date { get; set; }
        public int age { get; set; }
    }

    public class Id
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class Picture
    {
        public string large { get; set; }
        public string medium { get; set; }
        public string thumbnail { get; set; }
    }

}