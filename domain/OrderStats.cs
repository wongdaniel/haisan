using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace haisan.domain
{
    class OrderStats
    {
        private int id;

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
        private TypeOfProcess typeOfProcess;

        public TypeOfProcess TypeOfProcess
        {
            get { return typeOfProcess; }
            set { typeOfProcess = value; }
        }
        private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }
        private string unit;

        public string Unit
        {
            get { return unit; }
            set { unit = value; }
        }
        private decimal totalNumber;

        public decimal TotalNumber
        {
            get { return totalNumber; }
            set { totalNumber = value; }
        }
        private decimal unitPrice;

        public decimal UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }
        private decimal amountOfMoney;

        public decimal AmountOfMoney
        {
            get { return amountOfMoney; }
            set { amountOfMoney = value; }
        }

        private byte[] dwg;

        public byte[] Dwg
        {
            get { return dwg; }
            set { dwg = value; }
        }

        private string thicknessStats;

        public string ThicknessStats
        {
            get { return thicknessStats; }
            set { thicknessStats = value; }
        }

        public OrderStats()
        {
        }

        public OrderStats(int id)
        {
            this.id = id;
        }

    }
}
