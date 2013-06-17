using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    class XialiaoOrderStats
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private XialiaoOrder xialiaoOrder;

        internal XialiaoOrder XialiaoOrder
        {
            get { return xialiaoOrder; }
            set { xialiaoOrder = value; }
        }
        private OrderStats orderStats;

        internal OrderStats OrderStats
        {
            get { return orderStats; }
            set { orderStats = value; }
        }
        private decimal totalNumber;

        public decimal TotalNumber
        {
            get { return totalNumber; }
            set { totalNumber = value; }
        }
        private decimal amountOfMoney;

        public decimal AmountOfMoney
        {
            get { return amountOfMoney; }
            set { amountOfMoney = value; }
        }

        public XialiaoOrderStats()
        {
            this.id = 0;
        }

        public XialiaoOrderStats(int id)
        {
            this.id = id;
        }
    }
}
