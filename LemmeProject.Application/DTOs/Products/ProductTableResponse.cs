﻿using LemmeProject.Application.DTOs.Images;

namespace LemmeProject.Application.DTOs.Products
{
    public class ProductTableResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }
        public string HowToUse { get; set; }
        public string Ingredients { get; set; }

        //Relations
        //public IList<ImageAddRequest> Images { get; set; }
    }
}
