using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace haisan.domain
{
    class Product
    {
        private int id;
        private string name;
        private string code;
        private Provider provider;
        private Category category;
        private string address;
        private string unit;
        private decimal purchase_price;
        private decimal sale_price;
        private decimal wholesale_price;
        private string description;
        private Image image;

        public Product()
        {
            id = 0;
            image = null;
            category = new Category();
            provider = new Provider();
        }

        public Product(int id)
        {
            category = new Category();
            provider = new Provider();
            this.id = id;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        private string spec;

        public string Spec
        {
            get { return spec; }
            set { spec = value; }
        }
        public Provider Provider
        {
            get { return provider; }
            set { provider = value; }
        }
        public Category Category
        {
            get { return category; }
            set { category = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public decimal Purchase_price
        {
            get { return purchase_price; }
            set { purchase_price = value; }
        }
       

        public decimal Sale_price
        {
            get { return sale_price; }
            set { sale_price = value; }
        }
        

        public decimal Wholesale_price
        {
            get { return wholesale_price; }
            set { wholesale_price = value; }
        }
       
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }



    }
}
