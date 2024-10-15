﻿namespace PropertyRentals.Models
{
    public class ApartmentPhotoLinkViewModel
    {
        public Apartment Apartment { get; set; }
        public string PhotoLink { get; set; }

        public Property Property { get; set; }
        public string Address { get; set; }
    }
}
