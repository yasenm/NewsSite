using NewsSite.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.ComponentModel.DataAnnotations;

namespace NewsSite.Web.Areas.Administration.InputModels
{
    public class CategoryEditInputModel : IHaveCustomMappings
    {
        [Required]
        public string Name { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<NewsSite.Data.Models.Category, CategoryEditInputModel>().ReverseMap();
        }
    }
}