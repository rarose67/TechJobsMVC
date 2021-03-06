﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TechJobs.Models;

namespace TechJobs.Controllers
{
    public class SearchController : TechJobsController
    {
        public IActionResult Index()
        {
            //ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search";
            return View();
        }

        // TODO #1 - Create a Results action method to process 
        // search request and display results
        public IActionResult Results(string searchType, string searchTerm)
        {
            List<Dictionary<string, string>> jobs;
            
            //ViewBag.columns = ListController.columnChoices;
            ViewBag.title = "Search Results";
            ViewBag.searchType = searchType;
            ViewBag.searchTerm = searchTerm;
            
            if(String.IsNullOrEmpty(searchTerm))
            {
                jobs = JobData.FindAll();
                ViewBag.heading = "All Jobs";
            }
            else
            {
                if (searchType.Equals("all"))
                {
                    jobs = JobData.FindByValue(searchTerm);
                    ViewBag.heading = "Jobs with " + searchTerm;
                }
                else
                {
                    jobs = JobData.FindByColumnAndValue(searchType, searchTerm);
                    ViewBag.heading = "Jobs with " +
                        TechJobsController.columnChoices.GetValueOrDefault(searchType) +
                        " : " + searchTerm;
                }
            }

            ViewBag.jobs = jobs;
            return View("Index");
        }
    }
}
