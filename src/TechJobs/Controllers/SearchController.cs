using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results
        public IActionResult Results(string searchType, string searchTerm)
        {
            List<Dictionary<string, string>> jobs;
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            ViewBag.textInfo = textInfo;
            ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search Results";
            ViewBag.searchType = searchType;
            ViewBag.searchTerm = searchTerm;

            if (searchType.Equals("all"))
            {
                jobs = JobData.FindByValue(searchTerm);

                if(searchTerm.Equals(""))
                {
                    ViewBag.heading = "All Jobs";
                }
                else
                {
                    ViewBag.heading = "Jobs with " + searchTerm;
                }
            }
            else
            {
                jobs = JobData.FindByColumnAndValue(searchType, searchTerm);

                if (searchTerm.Equals(""))
                {
                    ViewBag.heading = "All Jobs";
                }
                else
                {
                    ViewBag.heading= "Jobs with " +
                        ListController.columnChoices.GetValueOrDefault(searchType) +
                        " : " + searchTerm;
                }
            }

            ViewBag.jobs = jobs;
            return View("Index");
        }
    }
}
