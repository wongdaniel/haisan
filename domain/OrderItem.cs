using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace haisan.domain
{
    class OrderItem
    {
        private  int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private Order order;

        internal Order Order
        {
            get { return order; }
            set { order = value; }
        }
        private CategoryOfStone categoryOfStone;

        internal CategoryOfStone CategoryOfStone
        {
            get { return categoryOfStone; }
            set { categoryOfStone = value; }
        }

        private ProductName productName;

        internal ProductName ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        private string length;

        public string Length
        {
            get { return length; }
            set { length = value; }
        }
        private string width;

        public string Width
        {
            get { return width; }
            set { width = value; }
        }
        private string thickness;

        public string Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }
        private int package;

        public int Package
        {
            get { return package; }
            set { package = value; }
        }
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private decimal number;

        public decimal Number
        {
            get { return number; }
            set { number = value; }
        }
        private decimal unitPrice;

        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }
        private decimal cost;

        public decimal Cost
        {
            get { return cost; }
            set { cost = value; }
        }
        private ProcessingImage workingDiagram1;

        public ProcessingImage WorkingDiagram1
        {
            get { return workingDiagram1; }
            set { workingDiagram1 = value; }
        }
        private TypeOfProcess workingName1;

        public TypeOfProcess WorkingName1
        {
            get { return workingName1; }
            set { workingName1 = value; }
        }
        private decimal workingNumber1;

        public decimal WorkingNumber1
        {
            get { return workingNumber1; }
            set { workingNumber1 = value; }
        }

        private ProcessingImage workingDiagram2;

        public ProcessingImage WorkingDiagram2
        {
            get { return workingDiagram2; }
            set { workingDiagram2 = value; }
        }
        private TypeOfProcess workingName2;

        public TypeOfProcess WorkingName2
        {
            get { return workingName2; }
            set { workingName2 = value; }
        }
        private decimal workingNumber2;

        public decimal WorkingNumber2
        {
            get { return workingNumber2; }
            set { workingNumber2 = value; }
        }

        private ProcessingImage workingDiagram3;

        public ProcessingImage WorkingDiagram3
        {
            get { return workingDiagram3; }
            set { workingDiagram3 = value; }
        }
        private TypeOfProcess workingName3;

        public TypeOfProcess WorkingName3
        {
            get { return workingName3; }
            set { workingName3 = value; }
        }
        private decimal workingNumber3;

        public decimal WorkingNumber3
        {
            get { return workingNumber3; }
            set { workingNumber3 = value; }
        }


        public OrderItem()
        {
        }

        public OrderItem(int id)
        {
            this.id = id;
        }
    }
}
