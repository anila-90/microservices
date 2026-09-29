using System;
using Microsoft.EntityFrameworkCore;


namespace platformservice.models
{
    public class platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }

    }
}