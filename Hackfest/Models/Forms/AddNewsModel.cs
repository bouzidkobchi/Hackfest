﻿namespace Hackfest.Models.Forms
{
    public class AddNewsModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public IFormFile[] Pictures { get; set; } = [];
    }
}