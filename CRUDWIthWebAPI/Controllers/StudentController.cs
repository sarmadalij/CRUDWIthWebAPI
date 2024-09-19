using CRUDWIthWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace CRUDWIthWebAPI.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7130/api/StudentAPI/";

        private HttpClient client = new HttpClient(); //it is important

        //get data from the api 
        [HttpGet]
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();

            //getting response from the target url
            HttpResponseMessage httpResponseMessage = client.GetAsync(url).Result;

            //to check the response
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                //json is stored in the string
                string result = httpResponseMessage.Content.ReadAsStringAsync().Result;

                //convert the json into the List Object
                var data = JsonConvert.DeserializeObject <List<Student>> (result);

                if(data != null)
                {
                    //store data in student object
                    students = data;
                }
            }
            return View(students);
        }


        //to just create View 

        [HttpGet]
        public IActionResult Create () 
        { 
            return View();
        }

        //to insert data in the database using api
        [HttpPost]
        public IActionResult Create(Student student)
        {
            //serialize data from student object to json 
            string data  = JsonConvert.SerializeObject (student);

            //json converted into formatted text
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //hitting url 
            HttpResponseMessage httpResponse = client.PostAsync(url, content).Result;

            if (httpResponse.IsSuccessStatusCode) 
            {
                TempData["insert_message"] = "New Student " +student.StudentName + "'s Data Added Successfully";
                return RedirectToAction("Index");
            }

            return View();
        }

        //get data for edit
        [HttpGet]
        public IActionResult Edit(int id) 
        {
            Student student = new Student();

            //hitting url with id
            HttpResponseMessage httpResponse = client.GetAsync(url + id).Result;

            if (httpResponse.IsSuccessStatusCode) 
            {
				//json is stored in the string
				string result = httpResponse.Content.ReadAsStringAsync().Result;

				//convert the json into the Student Object
				var data = JsonConvert.DeserializeObject<Student>(result);

				if (data != null)
				{
					//store data in student object
					student = data;
				}
			}

            return View(student);
        }

        //now to putting edited data to database using web api
        [HttpPost]
        public IActionResult Edit(Student student) 
        {
			//serialize data from student object to json 
			string data = JsonConvert.SerializeObject(student);

			//json converted into formatted text
			StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

			//hitting url 
			HttpResponseMessage httpResponse = client.PutAsync(url + student.Id, content).Result;

			if (httpResponse.IsSuccessStatusCode)
			{
				TempData["update_message"] = "Student " + student.StudentName + "'s Data Updated Successfully";
				return RedirectToAction("Index");
			}

			return View(student);
		}

        //getting data for specific students details from database using api
        [HttpGet]
        public IActionResult Details(int id)
        {
			Student student = new Student();

			//hitting url with id
			HttpResponseMessage httpResponse = client.GetAsync(url + id).Result;

			if (httpResponse.IsSuccessStatusCode)
			{
				//json is stored in the string
				string result = httpResponse.Content.ReadAsStringAsync().Result;

				//convert the json into the Student Object
				var data = JsonConvert.DeserializeObject<Student>(result);

				if (data != null)
				{
					//store data in student object
					student = data;
				}
			}

			return View(student);

        }

		//getting data for specific students to delete from database using api
		[HttpGet]
        public IActionResult Delete(int id)
        {
			Student student = new Student();

			//hitting url with id
			HttpResponseMessage httpResponse = client.GetAsync(url + id).Result;

			if (httpResponse.IsSuccessStatusCode)
			{
				//json is stored in the string
				string result = httpResponse.Content.ReadAsStringAsync().Result;

				//convert the json into the Student Object
				var data = JsonConvert.DeserializeObject<Student>(result);

				if (data != null)
				{
					//store data in student object
					student = data;
				}
			}

			return View(student);
        }

		//now delete the data 
		[HttpPost, ActionName("Delete")]
		public IActionResult DeleteConfirmed(int id)
		{
			
			//hitting url with id
			HttpResponseMessage httpResponse = client.DeleteAsync(url + id).Result;

			if (httpResponse.IsSuccessStatusCode)
			{
				TempData["delete_message"] = "Student " + id + "'s Data deleted Successfully";
				return RedirectToAction("Index");
			}

			return View();
		}

	}
}
