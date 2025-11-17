using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace USBHandbrake
{
    public partial class VerticalBar : UserControl
    {
        public event EventHandler<ushort> MinLimitChanging;
        public event EventHandler<ushort> MaxLimitChanging;
        public event EventHandler<VBHitTest> OnUserEdit;

        private ushort _value = 0;
        private ushort _minimum = ushort.MinValue;
        private ushort _maximum = ushort.MaxValue;
        private Color _colorMin = Color.AliceBlue;
        private Color _colorMax = Color.Coral;
        private bool _bEnableEdit = false;

        [Category("User Properties"), Browsable(true)]
        public bool EnableUserEdit
        {
            get { return _bEnableEdit; }
            set { _bEnableEdit = value; }
        }

        [Category("User Properties"), Browsable(true)]
        public ushort Value
        {
            get { return _value; }
            set
            {
                if (value != _value)
                {
                    _value = value;
                    Invalidate();
                }
            }
        }

        [Category("User Properties"), Browsable(true)]
        public ushort MinLimit
        {
            get { return _minimum; }
            set
            {
                if (value != _minimum)
                {
                    _minimum = value;
                    Invalidate();
                }
            }
        }

        [Category("User Properties"), Browsable(true)]
        public ushort MaxLimit
        {
            get { return _maximum; }
            set
            {
                if (value != _maximum)
                {
                    _maximum = value;
                    Invalidate();
                }
            }
        }

        [Category("User Properties"), Browsable(true)]
        public Color MinLimitColor
        {
            get { return _colorMin; }
            set
            {
                if (value != _colorMin)
                {
                    _colorMin = value;
                    Invalidate();
                }
            }
        }

        [Category("User Properties"), Browsable(true)]
        public Color MaxLimitColor
        {
            get { return _colorMax; }
            set
            {
                if (value != _colorMax)
                {
                    _colorMax = value;
                    Invalidate();
                }
            }
        }

        public VerticalBar()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int hValue = (int)Math2.Mapd(_value, ushort.MinValue, ushort.MaxValue, ClientRectangle.Top, ClientRectangle.Bottom);
            int hMinValue = (int)Math2.Mapd(_minimum, ushort.MinValue, ushort.MaxValue, ClientRectangle.Top, ClientRectangle.Bottom);
            int hMaxValue = (int)Math2.Mapd(_maximum, ushort.MinValue, ushort.MaxValue, ClientRectangle.Top, ClientRectangle.Bottom);

            using (SolidBrush br1 = new SolidBrush(ForeColor))
            using (SolidBrush brMin = new SolidBrush(_colorMin))
            using (SolidBrush brMax = new SolidBrush(_colorMax))
            {
                e.Graphics.FillRectangle(br1, new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Top + ClientRectangle.Bottom  - hValue), new Size(ClientRectangle.Width, hValue)));
                e.Graphics.FillRectangle(brMin, new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Top + +ClientRectangle.Bottom - hMinValue), new Size(ClientRectangle.Width, hMinValue)));
                e.Graphics.FillRectangle(brMax, new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Top), new Size(ClientRectangle.Width, ClientRectangle.Bottom - hMaxValue)));
                e.Graphics.DrawRectangle(Pens.White, new Rectangle(new Point(ClientRectangle.X, ClientRectangle.Top + ClientRectangle.Bottom - hValue), new Size(ClientRectangle.Width, 1)));
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (_bEnableEdit)
                Cursor = Cursors.Hand;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (_bEnableEdit)
                Cursor = Cursors.Default;
        }

        public enum VBHitTest { None, MinLimit, MaxLimit};

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (_bMouseDown == true && _downHT != VBHitTest.None)
            {
                double v = Math2.Mapd(ClientRectangle.Bottom - e.Location.Y, ClientRectangle.Top, ClientRectangle.Bottom, ushort.MinValue, ushort.MaxValue);
                v = Math2.Clamp(v, ushort.MinValue, ushort.MaxValue);

                switch (_downHT)
                {
                    case VBHitTest.MinLimit:
                        MinLimit = (ushort)v;
                        Cursor = Cursors.HSplit;
                        MinLimitChanging?.Invoke(this, MinLimit);
                        return;
                    case VBHitTest.MaxLimit:
                        MaxLimit = (ushort)v;
                        Cursor = Cursors.HSplit;
                        MaxLimitChanging?.Invoke(this, MaxLimit);
                        return;
                }
            }

            if (_bEnableEdit && HitTest(e.Location) != VBHitTest.None )
            {
                Cursor = Cursors.HSplit;
            }
            else
                Cursor = Cursors.Hand;
        }

        bool _bMouseDown = false;
        VBHitTest _downHT = VBHitTest.None;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (_bEnableEdit && e.Button == MouseButtons.Left )
            {
                _bMouseDown = true;
                _downHT = HitTest(e.Location);
                OnUserEdit?.Invoke(this, _downHT);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                _bMouseDown = false;
                _downHT = VBHitTest.None;
                OnUserEdit?.Invoke(this, _downHT);
            }
        }

        private VBHitTest HitTest(Point location)
        {
            int hMinValue = (int)Math2.Mapd(_minimum, ushort.MinValue, ushort.MaxValue, ClientRectangle.Top, ClientRectangle.Bottom);
            int hMaxValue = (int)Math2.Mapd(_maximum, ushort.MinValue, ushort.MaxValue, ClientRectangle.Top, ClientRectangle.Bottom);

            if (Math.Abs(ClientRectangle.Bottom - location.Y -  hMinValue) < 2)
                return VBHitTest.MinLimit;
            
            if (Math.Abs(ClientRectangle.Bottom - location.Y - hMaxValue) < 2)
                return VBHitTest.MaxLimit;

            return VBHitTest.None;
        }
    }
}
