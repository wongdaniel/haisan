using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace haisan.domain
{
    class ProcessingImage
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }
        private bool up;

        public bool Up
        {
            get { return up; }
            set { up = value; }
        }
        private bool down;

        public bool Down
        {
            get { return down; }
            set { down = value; }
        }
        private bool left;

        public bool Left
        {
            get { return left; }
            set { left = value; }
        }
        private bool right;

        public bool Right
        {
            get { return right; }
            set { right = value; }
        }

        public ProcessingImage(int id, Image image)
        {
            this.id = id;
            this.image = image;
        }

        public ProcessingImage(int id, Image image, bool up, bool down, bool left, bool right)
        {
            this.id = id;
            this.image = image;
            this.up = up;
            this.down = down;
            this.left = left;
            this.right = right;
        }

        public ProcessingImage()
        {
        }

    }
}
