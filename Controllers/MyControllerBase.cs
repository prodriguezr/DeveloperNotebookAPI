using DeveloperNotebookAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperNotebookAPI.Controllers
{
    public class MyControllerBase : ControllerBase 
    {
        protected readonly MyDbContext myDbContext;

        public MyControllerBase(MyDbContext context) => myDbContext = context;
    }
}