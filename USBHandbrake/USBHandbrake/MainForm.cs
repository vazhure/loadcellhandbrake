using System;
using System.Windows.Forms;
using Microsoft.Win32;
using HidLibrary;
using System.Linq;
using System.ComponentModel;
using System.Drawing;

namespace USBHandbrake
{
    public partial class MainForm : Form
    {
        int MinValue = 0;
        int MidValue = 0;
        int MaxValue = 0;
        const ushort cDefaultMin = 1500;
        const ushort cDefaultMid = 20000;
        const ushort cDefaultMax = 40000;

        const int cMaxOutput = ushort.MaxValue;
        const int cInterval = 33; // ms => 30 Hz
        const double cMaxLoad = 20; // kg

        readonly int VendorID = 0x2398;
        readonly int ProductID = 0x8098;

        private bool bAttached = false;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                if (GetCalibration(VendorID, ProductID, out int min, out int mid, out int max))
                {
                    MinValue = min;
                    MidValue = mid;
                    MaxValue = max;
                }
            }
            catch
            {
                MinValue = 0;
                MidValue = short.MaxValue;
                MaxValue = ushort.MaxValue;
            }

            UpdateCalibrationInfo();

            btnApplyChanges.Visible = btnDiscardChanges.Visible = false;

            verticalBar1.MinLimit = (ushort)MinValue;
            verticalBar1.MaxLimit = (ushort)MaxValue;
            verticalBar1.MinLimitChanging += VerticalBar1_LimitChanging;
            verticalBar1.MaxLimitChanging += VerticalBar1_LimitChanging;
            verticalBar1.OnUserEdit += VerticalBar1_OnUserEdit;
        }

        VerticalBar.VBHitTest _userEdit = VerticalBar.VBHitTest.None;

        private void VerticalBar1_OnUserEdit(object sender, VerticalBar.VBHitTest e)
        {
            _userEdit = e;
        }

        private void VerticalBar1_LimitChanging(object sender, ushort e)
        {
            btnApplyChanges.Visible = btnDiscardChanges.Visible = MinValue != verticalBar1.MinLimit || MaxValue != verticalBar1.MaxLimit;
        }

        bool GetCalibration(int vendorID, int productID, out int min, out int mid, out int max)
        {
            string s2 = $"System\\CurrentControlSet\\Control\\MediaProperties\\PrivateProperties\\DirectInput\\VID_{vendorID:x}&PID_{productID:x}\\Calibration\\0\\Type\\Axes\\5";
            if (Registry.CurrentUser.OpenSubKey(s2) is RegistryKey key)
            {
                byte[] Axis = key.GetValue("Calibration") as byte[];
                min = 256 * Axis[1] + Axis[0];
                mid = 256 * Axis[5] + Axis[4];
                max = 256 * Axis[9] + Axis[8];

                return true;
            }
            else
            {
                min = 0;
                mid = short.MaxValue;
                max = ushort.MaxValue;
            }

            return false;
        }

        bool SetCalibration(int vendorID, int productID, int min, int mid, int max)
        {
            string s2 = $"System\\CurrentControlSet\\Control\\MediaProperties\\PrivateProperties\\DirectInput\\VID_{vendorID:x}&PID_{productID:x}\\Calibration\\0\\Type\\Axes\\5";

            if (Registry.CurrentUser.CreateSubKey(s2) is RegistryKey key)
            {
                key.SetValue("Calibration", new byte[] { (byte)(min % 256), (byte)(min / 256), 0, 0, (byte)(mid % 256), (byte)(mid / 256), 0, 0, (byte)(max % 256), (byte)(max / 256), 0, 0 });
                /*
                * ByteArray[0] = (byte)(min % 256);
                * ByteArray[1] = (byte)(min / 256);
                * ByteArray[4] = (byte)(middle % 256);
                * ByteArray[5] = (byte)(middle / 256);
                * ByteArray[8] = (byte)(max % 256);
                * ByteArray[9] = (byte)(max / 256);
                */
                return true;
            }

            return false;
        }

        HidDevice handbrake;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Connect();
        }

        private void Connect()
        {
            do
            {
                if (HidDevices.Enumerate().Where(p => p.Attributes.VendorHexId == $"0x{VendorID:x}" && p.Attributes.ProductHexId == $"0x{ProductID:x}").FirstOrDefault() is HidDevice device)
                {
                    if (handbrake == null)
                    {
                        handbrake = device;
                        handbrake.OpenDevice();
                        handbrake.MonitorDeviceEvents = true;
                        handbrake.Inserted += Handbrake_Inserted;
                        handbrake.Removed += Handbrake_Removed;
                        handbrake.ReadReport(OnReport, cInterval);
                        bAttached = true;
                    }
                }
                else
                {
                    if (MessageBox.Show("Ручной тормоз не подключен! Повторить?") == DialogResult.Cancel)
                    {
                        BeginInvoke((Action)delegate
                        {
                            Close();
                        });
                        break;
                    }
                }
            } while (handbrake == null);
        }

        private void OnReport(HidReport report)
        {
            if (!bAttached)
                return;

            if (report.Data.Length == 3)
            {
                BeginInvoke((Action)delegate
                {
                    // Флаг нажатой кнопки
                    byte btn = report.Data[0];
                    // Значение оси
                    uint value = BitConverter.ToUInt16(report.Data, 1);
                    verticalBar1.Value = (ushort)value;
                    _currentMax = (ushort)Math.Max(value, _currentMax);

                    if (_currentStep == 2)
                        verticalBar1.MaxLimit = _currentMax;

                    double valueNew = Math2.Mapd(value, MinValue, MaxValue, 0, cMaxOutput);
                    valueNew = Math2.Clamp((int)valueNew, 0, cMaxOutput);
                    verticalBar2.Value = (ushort)valueNew;

                    if (_userEdit == VerticalBar.VBHitTest.None)
                    {
                        lblKg.Text = $"{ Math2.Mapd(value, ushort.MinValue, ushort.MaxValue, 0, cMaxLoad):N1} кг";
                        lblRawInput.Text = $"{value:N0}";
                    }
                    else
                    {
                        if (_userEdit == VerticalBar.VBHitTest.MaxLimit)
                        {
                            lblKg.Text = $"{ Math2.Mapd(verticalBar1.MaxLimit, ushort.MinValue, ushort.MaxValue, 0, cMaxLoad):N1} кг";
                            lblRawInput.Text = $"{verticalBar1.MaxLimit:N0}";
                        }
                        else
                        {
                            lblKg.Text = $"{ Math2.Mapd(verticalBar1.MinLimit, ushort.MinValue, ushort.MaxValue, 0, cMaxLoad):N1} кг";
                            lblRawInput.Text = $"{verticalBar1.MinLimit:N0}";
                        }
                    }

                    lblPercent.Text = $"{ Math2.Mapd(valueNew, ushort.MinValue, ushort.MaxValue, 0, 100):N1} %";
                    lblRawOutput.Text = $"{valueNew:N0}";

                    pbVirtualButton.BackColor = btn > 0 ? Color.Green : BackColor;
                });
            }

            handbrake.ReadReport(OnReport, cInterval);
        }

        private void Handbrake_Removed()
        {
            bAttached = false;
            BeginInvoke((Action)delegate
            {
                btnCalibrate.Enabled = false;
                if (_currentStep > 0)
                {
                    if (MessageBox.Show(this, "Устройство отключено! Отменить калибровку?", "Калибровка", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        EndCalibration(true);
                    }
                }
            });
        }

        private void Handbrake_Inserted()
        {
            bAttached = true;
            BeginInvoke((Action)delegate
            {
                btnCalibrate.Enabled = true;
            });
            handbrake.ReadReport(OnReport, cInterval);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_currentStep > 0)
            {
                if (MessageBox.Show("Калибровка не завершена! Завершить программу?") != DialogResult.OK)
                    e.Cancel = true;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            handbrake?.CloseDevice();
        }

        /// <summary>
        /// Текущее максимальное значение калибровки
        /// </summary>
        ushort _currentMax = 0;
        /// <summary>
        /// Шаг мастера калибровки
        /// </summary>
        int _currentStep = 0;

        private void BtnCalibrate_Click(object sender, EventArgs e)
        {
            _min = MinValue;
            _max = MaxValue;
            _mid = MidValue;

            MinValue = ushort.MinValue;
            MidValue = short.MaxValue;
            MaxValue = ushort.MaxValue;

            verticalBar1.MinLimit = (ushort)MinValue;
            verticalBar1.MaxLimit = (ushort)MaxValue;

            _currentStep = 1;
            _currentMax = 0;
            btnCancel.Visible = btnNext.Visible = true;
            btnCalibrate.Enabled = false;
            lblTips.Text = "Верните рукоятку ручного тормоза в вертикальное положение и нажмите кнопку далее";
        }

        int _min = 0;
        int _max = 0;
        int _mid = 0;

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (!bAttached && _currentStep > 0)
            {
                if (MessageBox.Show(this, "Устройство отключено! Отменить калибровку?", "Калибровка", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    EndCalibration(true);
                    return;
                }
            }

            switch (_currentStep)
            {
                case 1:
                    MinValue = _currentMax;
                    _currentMax = 0;
                    lblTips.Text = "Выжмите ручной тормоз до комфортного усилия и нажмите кнопку далее";
                    _currentStep++;
                    break;
                case 2:
                    MaxValue = _currentMax;
                    MidValue = (MaxValue + MinValue) / 2;
                    _currentMax = 0;
                    lblTips.Text = "Нажмите кнопку далее для завершения";
                    _currentStep++;
                    break;
                case 3:
                    {
                        try
                        {
                            SetCalibration(VendorID, ProductID, MinValue, (MaxValue + MinValue) / 2, MaxValue);
                            EndCalibration();
                            MessageBox.Show("Калибровка успешно завершена!");
                        }
                        catch
                        {
                            MessageBox.Show("Возникла ошибка сохранения результатов калибровки!");
                            MinValue = _min;
                            MaxValue = _max;
                            MidValue = _mid;
                            EndCalibration();
                        }
                    }
                    break;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_currentStep > 0)
            {
                EndCalibration(true);
            }
        }

        private void EndCalibration(bool cancel = false)
        {
            if (cancel)
            {
                MinValue = _min;
                MaxValue = _max;
                MidValue = _mid;
            }

            lblTips.Text = "";
            _currentStep = 0;
            btnCancel.Visible = btnNext.Visible = false;
            btnCalibrate.Enabled = bAttached;
            verticalBar1.MinLimit = (ushort)MinValue;
            verticalBar1.MaxLimit = (ushort)MaxValue;
            UpdateCalibrationInfo();
        }

        private void BtnDefault_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Установить рекомендуемые значения?") == DialogResult.OK)
            {
                MinValue = cDefaultMin;
                MidValue = cDefaultMid;
                MaxValue = cDefaultMax;

                SetCalibration(VendorID, ProductID, MinValue, MidValue, MaxValue);

                verticalBar1.MinLimit = (ushort)MinValue;
                verticalBar1.MaxLimit = (ushort)MaxValue;
                
                UpdateCalibrationInfo();
                
                btnDiscardChanges.Visible = btnApplyChanges.Visible = false;
            }
        }

        private void BtnApplyChanges_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Применить и сохранить калибровку?") == DialogResult.OK)
            {
                MinValue = verticalBar1.MinLimit;
                MaxValue = verticalBar1.MaxLimit;
                SetCalibration(VendorID, ProductID, MinValue, MidValue, MaxValue);
                btnApplyChanges.Visible = btnDiscardChanges.Visible = false;
                UpdateCalibrationInfo();
            }
        }

        private void BtnDiscardChanges_Click(object sender, EventArgs e)
        {
            verticalBar1.MinLimit = (ushort)MinValue;
            verticalBar1.MaxLimit = (ushort)MaxValue;
            btnDiscardChanges.Visible = btnApplyChanges.Visible = false;
            UpdateCalibrationInfo();
        }

        private void UpdateCalibrationInfo()
        {
            lblMin.Text = $"Нижний порог: {MinValue:N0} ({ Math2.Mapd(MinValue, ushort.MinValue, ushort.MaxValue, 0, cMaxLoad):N1} кг)";
            lblMid.Text = $"Середина: {MidValue:N0} ({ Math2.Mapd(MidValue, ushort.MinValue, ushort.MaxValue, 0, cMaxLoad):N1} кг)";
            lblMax.Text = $"Верхний порог: {MaxValue:N0} ({ Math2.Mapd(MaxValue, ushort.MinValue, ushort.MaxValue, 0, cMaxLoad):N1} кг)";
        }
    }
}