using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.domain
{
    class XialiaoOrderItem
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
        private OrderItem orderItem;

        internal OrderItem OrderItem
        {
            get { return orderItem; }
            set { orderItem = value; }
        }
        private int originalPackage;

        public int OriginalPackage
        {
            get { return originalPackage; }
            set { originalPackage = value; }
        }
        private int remainPackage;

        public int RemainPackage
        {
            get { return remainPackage; }
            set { remainPackage = value; }
        }
        private int usePackage;

        public int UsePackage
        {
            get { return usePackage; }
            set { usePackage = value; }
        }

        private decimal workingNumber1;

        public decimal WorkingNumber1
        {
            get { return workingNumber1; }
            set { workingNumber1 = value; }
        }
        private decimal workingNumber2;

        public decimal WorkingNumber2
        {
            get { return workingNumber2; }
            set { workingNumber2 = value; }
        }
        private decimal workingNumber3;

        public decimal WorkingNumber3
        {
            get { return workingNumber3; }
            set { workingNumber3 = value; }
        }

        public XialiaoOrderItem()
        {
            this.id = 0;
        }

        public XialiaoOrderItem(int id)
        {
            this.id = id;
        }

    }
}
