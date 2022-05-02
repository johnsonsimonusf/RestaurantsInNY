using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Linq_practice_studnet_6.DataAccess;
using Linq_practice_studnet_6.Models;
using RestaurantsInNY.Models;
using Newtonsoft.Json;

namespace Linq_practice_studnet_6
{
    public class HomeController : Controller
    {
        SchoolDbContext dbContext;
        public HomeController(SchoolDbContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            //populateData();
           // ViewBag.title = "hi";
            return View();
        }

        public async Task<ViewResult> ApiAsync()

        {

            HttpClient httpClient;

            string BASE_URL = "https://data.cityofnewyork.us/resource/qgc5-ecnb.json";

            httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Accept.Clear();

            httpClient.DefaultRequestHeaders.Accept.Add(

                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string NATIONAL_PARK_API_PATH = BASE_URL;

            string rdbData = "";

            List<APIModel> rdbList = null;

            //httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);

            httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);

            try

            {

                //HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH)

                //                                        .GetAwaiter().GetResult();

                HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH)

                                                        .GetAwaiter().GetResult();

                ;
                if (response.IsSuccessStatusCode)

                {

                    rdbData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                }

                if (!rdbData.Equals(""))

                {

                    // JsonConvert is part of the NewtonSoft.Json Nuget package

                    rdbList = JsonConvert.DeserializeObject<APIModel[]>(rdbData).ToList();
                    Console.WriteLine(rdbList[0].calories);

                }



                Restaurant r = new Restaurant();
                Menu m = new Menu();
                Nutrition n = new Nutrition();

                Random rand = new Random();
                foreach (var rdb in rdbList)
                {
                    r.Restaurant_ID = Int32.Parse(rdb.restaurant_id);
                    r.Restaurant_Name = rdb.restaurant;
                    r.Restaurant_Location = "New York";
                    dbContext.Restaurant.Add(r);
                    //dbContext.SaveChangesAsync();

                    m.Item_Name = rdb.item_name;
                    m.Item_Category = rdb.food_category;
                    m.Menu_Item_ID = Int32.Parse(rdb.menu_item_id);
                    dbContext.Menu.Add(m);
                    //dbContext.SaveChangesAsync();

                    n.Nutrition_ID = rand.Next();
                    if (rdb.sugar == null) rdb.sugar = "0";
                    n.Sugar = Int32.Parse(rdb.sugar);
                    if (rdb.total_fat == null) rdb.total_fat = "0";
                    n.Total_Fat = Int32.Parse(rdb.total_fat);
                    if (rdb.calories == null) rdb.calories = "0";
                    n.Calorie = Int32.Parse(rdb.calories);
                    if (rdb.protein == null) rdb.protein = "0";
                    n.Protein = Int32.Parse(rdb.protein);
                    dbContext.Nutrition.Add(n);
                    //dbContext.ExecuteStoreCommand("SET IDENTITY_INSERT Menu ON");
                    await dbContext.SaveChangesAsync();
                    //dbContext.SaveChanges();
                }
            }

            catch (Exception e)

            {

                Console.WriteLine(e.Message);

            }

            return View(rdbList);

        }

        public async Task<IActionResult> Queries()
        {            
            return View();
        }


     /*   void populateData()
        {
            Random rnd = new Random();
                
            string[] Colleges = {
                                 "Muma College of Business, MCOB",
                                 "College of Engineering, CoE",
                                 "College of Arts and Sciences, CAS",
                                 "College of Nursing, CON",
                                 "Morsani College of Medicine,MCOM",
                                 "College of Public Health,COPH"
            };
            string[] Courses = {
                "ISM 6225, Distributed Information Systems",
                "ISM 6218, Advanced Database Management Systems",
                "ISM 6137, Statistical Data Mining",
                "ISM 6419, Data Visualization",
                "ISM 6930, Blockchain Fundamentals",
                "ISM 6562, Big Data for Business",
                "ISM 6328, Information Security and IT Risk Management",
                "QMB 6304, Analytical Methods For Business",
                "ISM 6136, Data Mining",
                "NUR 3125, Pathophysiology for Nursing Practice",
                "NUR 3145, Pharmacology in Nursing Practice",
                "NUR 4165, Nursing Inquiry",
                "NUR 3078, Information Technology Skills for Nurses",
                "NUR 4169, Evidence-Based Practice for Baccalaureate Prepared Nurse",
                "NUR 4634, Population Health",
                "NSP 3147, Web-Based Education for Staff Development"

            };
            
            string[] Students = {
                "Monica", "Sara","Adam","Jude","Callie","Ross","Stark",
                "Chandler","Phoebe","Carrie","Tristan","sally","Robert",
                "Sid","Warner","Joey","Andy","Conner","Ruby","Kate"
            };
            string[] Grades = { "A", "A-", "B+", "B", "B-" };
            int[] scores = { 95, 91, 87, 82, 75,66,55,80,63,90,45,60};

            College[] colleges = new College[Colleges.Length];
            Course[] courses = new Course[Courses.Length];
            Student[] students = new Student[Students.Length];

            for (int i = 0; i < Colleges.Length; i++)
            {
                College college = new College
                {
                    Name = Colleges[i].Split(",")[0],
                    Abbreviation = Colleges[i].Split(",")[1]
                };

                dbContext.Colleges.Add(college);
                colleges[i] = college;
            }

            for (int i = 0; i < Courses.Length; i++)
            {
                Course course = new Course
                {

                    Number = Courses[i].Split(",")[0],
                    Name = Courses[i].Split(",")[1],
                    College = colleges[rnd.Next(colleges.Length)]
                };

                dbContext.Courses.Add(course);
                courses[i] = course;
            }

            for (int i = 0; i < Students.Length; i++)
            {
                Student student = new Student
                { 
                    Name = Students[i] 
                };
                
                dbContext.Students.Add(student);
                students[i] = student;
            }

            foreach (Student student in students)
            {
                foreach (Course course in courses)
                {
                    Enrollment enrollment = new Enrollment
                    {
                        Course = course,
                        Student = student,
                        Grade = Grades[rnd.Next(Grades.Length)],
                        score=scores[rnd.Next(scores.Length)]
                    };

                    dbContext.Enrollments.Add(enrollment);
                }
            }

            //dbContext.SaveChanges();
        }*/
    }
}
