using NewsSite.Web.Infrastructure.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using NewsSite.Data.Models;

namespace NewsSite.Web.Areas.Administration.InputModels.Articles
{
    public class ArticleEditModel : IHaveCustomMappings
    {
        public ArticleEditModel()
        {
        }

        [Display(Name = "Заглавие")]
        [Required(ErrorMessage = "Заглавието е задължително!")]
        public string Title { get; set; }

        [AllowHtml]
        [Display(Name = "Съдържание")]
        [Required(ErrorMessage = "Съдържанието е задължително!")]
        [UIHint("TinyMce")]
        public string Content { get; set; }

        [Display(Name = "Важна новина")]
        public bool IsImportant { get; set; }

        [Display(Name = "Акцентирай")]
        public bool IsHighlighted { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Изберете категория!")]
        public int CategoryId { get; set; }

        [Display(Name = "Начална снимка")]
        public HttpPostedFileBase CoverPhoto { get; set; }

        public ICollection<HttpPostedFileBase> ArticlePhotos { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Article, ArticleEditModel>().ReverseMap();
        }
    }
}