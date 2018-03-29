//----------------------------------------------------------------------------------------------------------------------+
// WiiBalanceWalker - Released by Richard Perry from GreyCube.com - Under the Microsoft Public License.
//
// Project platform set as x86 for the joystick option work as VJoy.DLL only available as native 32-bit.
//
// Uses the WiimoteLib DLL:           http://wiimotelib.codeplex.com/
// Uses the 32Feet.NET bluetooth DLL: http://32feet.codeplex.com/
// Used the VJoy joystick DLL:        http://headsoft.com.au/index.php?category=vjoy
//----------------------------------------------------------------------------------------------------------------------+

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;
using VJoyLibrary;
using WiimoteLib;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;

namespace WiiBalanceWalker
{
    public partial class FormMain : Form
    {   
        //0.05 second interval 20Hz
        System.Timers.Timer infoUpdateTimer = new System.Timers.Timer() { Interval = 50,     Enabled = false };
        //240 second interval
        System.Timers.Timer joyResetTimer   = new System.Timers.Timer() { Interval = 240000, Enabled = false };

        ActionList actionList = new ActionList();
        Wiimote wiiDevice     = new Wiimote();
        DateTime jumpTime     = DateTime.UtcNow;
        VJoy joyDevice        = null;

        bool setCenterOffset = false;

        

        // Used to zero out the WiiBoard

        float blOffset = 0f;
        float brOffset = 0f;
        float tlOffset = 0f;
        float trOffset = 0f;
        float weightOffset = 0f;

        float naCorners     = 0f;
        float oaTopLeft     = 0f;
        float oaTopRight    = 0f;
        float oaBottomLeft  = 0f;
        float oaBottomRight = 0f;
        

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Setup a timer which controls the rate at which updates are processed.

            infoUpdateTimer.Elapsed += new ElapsedEventHandler(infoUpdateTimer_Elapsed);

            // Setup a timer which prevents a VJoy popup message.

            joyResetTimer.Elapsed += new ElapsedEventHandler(joyResetTimer_Elapsed);

            Globals.TimerOn = false;

            // Load trigger settings.

            //numericUpDown_TLR.Value  = Properties.Settings.Default.TriggerLeftRight;
            //numericUpDown_TFB.Value  = Properties.Settings.Default.TriggerForwardBackward;
            //numericUpDown_TMLR.Value = Properties.Settings.Default.TriggerModifierLeftRight;
            //numericUpDown_TMFB.Value = Properties.Settings.Default.TriggerModifierForwardBackward;

            // Link up form controls with action settings.

            //actionList.Left          = new ActionItem("Left",          comboBox_AL,  numericUpDown_AL);
            //actionList.Right         = new ActionItem("Right",         comboBox_AR,  numericUpDown_AR);
            //actionList.Forward       = new ActionItem("Forward",       comboBox_AF,  numericUpDown_AF);
            //actionList.Backward      = new ActionItem("Backward",      comboBox_AB,  numericUpDown_AB);
            //actionList.Modifier      = new ActionItem("Modifier",      comboBox_AM,  numericUpDown_AM);
            //actionList.Jump          = new ActionItem("Jump",          comboBox_AJ,  numericUpDown_AJ);
            //actionList.DiagonalLeft  = new ActionItem("DiagonalLeft",  comboBox_ADL, numericUpDown_ADL);
            //actionList.DiagonalRight = new ActionItem("DiagonalRight", comboBox_ADR, numericUpDown_ADR);

            // Load joystick preference.

            Array.Clear(Globals.COGxArray, 0, 600);
            Array.Clear(Globals.COGyArray, 0, 600);

            Globals.TraceOn = false;

            //checkBox_EnableJoystick.Checked = Properties.Settings.Default.EnableJoystick;
        }


        private void button_SetCenterOffset_Click(object sender, EventArgs e)
        {
            setCenterOffset = true;
        }

        private void button_ResetDefaults_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            this.Close();
        }

        private void button_BluetoothAddDevice_Click(object sender, EventArgs e)
        {
            var form = new FormBluetooth();
            form.ShowDialog(this);
        }

        private void button_Connect_Click(object sender, EventArgs e)
        {
            try
            {
                // Find all connected Wii devices.

                var deviceCollection = new WiimoteCollection();
                deviceCollection.FindAllWiimotes();

                for (int i = 0; i < deviceCollection.Count; i++)
                {
                    wiiDevice = deviceCollection[i];

                    // Device type can only be found after connection, so prompt for multiple devices.

                    if (deviceCollection.Count > 1)
                    {
                        var devicePathId = new Regex("e_pid&.*?&(.*?)&").Match(wiiDevice.HIDDevicePath).Groups[1].Value.ToUpper();

                        var response = MessageBox.Show("Connect to HID " + devicePathId + " device " + (i + 1) + " of " + deviceCollection.Count + " ?", "Multiple Wii Devices Found", MessageBoxButtons.YesNoCancel);
                        if (response == DialogResult.Cancel) return;
                        if (response == DialogResult.No) continue;
                    }

                    // Setup update handlers.

                    wiiDevice.WiimoteChanged          += wiiDevice_WiimoteChanged;
                    wiiDevice.WiimoteExtensionChanged += wiiDevice_WiimoteExtensionChanged;

                    // Connect and send a request to verify it worked.

                    wiiDevice.Connect();
                    wiiDevice.SetReportType(InputReport.IRAccel, false); // FALSE = DEVICE ONLY SENDS UPDATES WHEN VALUES CHANGE!
                    wiiDevice.SetLEDs(true, false, false, false);

                    // Enable processing of updates.

                    infoUpdateTimer.Enabled = true;

                    // Prevent connect being pressed more than once.

                    button_Connect.Enabled = false;
                    button_BluetoothAddDevice.Enabled = false;
                    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Function to zero out WiiBoard
        private void button1_Click(object sender, EventArgs e)
        {
            const float four = 4;

            weightOffset = wiiDevice.WiimoteState.BalanceBoardState.WeightKg;

            tlOffset = (wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.TopLeft) / four;
            trOffset = (wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.TopRight) / four;
            blOffset = (wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft) / four;
            brOffset = (wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.BottomRight) / four;

        }

        private void wiiDevice_WiimoteChanged(object sender, WiimoteChangedEventArgs e)
        {
            // Called every time there is a sensor update, values available using e.WiimoteState.
            // Use this for tracking and filtering rapid accelerometer and gyroscope sensor data.
            // The balance board values are basic, so can be accessed directly only when needed.
        }

        private void wiiDevice_WiimoteExtensionChanged(object sender, WiimoteExtensionChangedEventArgs e)
        {
            // This is not needed for balance boards.
        }

        void infoUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Pass event onto the form GUI thread.

            this.BeginInvoke(new Action(() => InfoUpdate()));
        }

        private void InfoUpdate()
        {
            const double boardX = 433;
            const double boardY = 228;
            const float four = 4;

            if (wiiDevice.WiimoteState.ExtensionType != ExtensionType.BalanceBoard)
            {
                label_Status.Text = "DEVICE IS NOT A BALANCE BOARD...";
                return;
            }

            // Get the current raw sensor KG values.

            var rwWeight = wiiDevice.WiimoteState.BalanceBoardState.WeightKg - weightOffset;

            var rwTopLeft = ((wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.TopLeft) / four) - tlOffset;
            var rwTopRight = ((wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.TopRight) / four) - trOffset;
            var rwBottomLeft = ((wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.BottomLeft) / four) - blOffset;
            var rwBottomRight = ((wiiDevice.WiimoteState.BalanceBoardState.SensorValuesKg.BottomRight) / four) - brOffset;



            /*Globals.COPx = ((boardX / 2.0) * ((rwTopRight + rwBottomRight) - (rwTopLeft + rwBottomLeft))
                / (rwTopRight + rwBottomRight + rwTopLeft + rwBottomLeft))/10.0;
            Globals.COPy = ((boardY / 2.0) * ((rwTopRight + rwTopLeft) - (rwBottomLeft + rwBottomRight))
                / (rwTopRight + rwBottomRight + rwTopLeft + rwBottomLeft))/10.0;*/

            Globals.COGx = wiiDevice.WiimoteState.BalanceBoardState.CenterOfGravity.X;
            Globals.COGy = (-1.0) * wiiDevice.WiimoteState.BalanceBoardState.CenterOfGravity.Y;

            //start at the end COGxArray[599] (COGxArray is all initialized to 0)
            //shift to left by one
            Array.Copy(Globals.COGxArray, 1, Globals.COGxArray, 0, 599);
            Globals.COGxArray[599] = Globals.COGx;

            Array.Copy(Globals.COGyArray, 1, Globals.COGyArray, 0, 599);
            Globals.COGyArray[599] = Globals.COGy;



            Array.Copy(Globals.TLArray, 1, Globals.TLArray, 0, 599);
            Globals.TLArray[599] = rwTopLeft;

            Array.Copy(Globals.BLArray, 1, Globals.BLArray, 0, 599);
            Globals.BLArray[599] = rwBottomLeft;

            Array.Copy(Globals.TRArray, 1, Globals.TRArray, 0, 599);
            Globals.TRArray[599] = rwTopRight;

            Array.Copy(Globals.BRArray, 1, Globals.BRArray, 0, 599);
            Globals.BRArray[599] = rwBottomRight;



            if (Globals.TimerOn)
            {
                Globals.Period--;
                if (Globals.Period == 0)
                {
                    print();
                    Globals.TimerOn = false;
                    button3.Text = "Start";
                }
            }

            //only trace the last 100 (599~500);

            //copScatter1.Update();
            if (Globals.TraceOn) {
                copScatter1.ValuesA.Add(new ObservablePoint(Globals.COGx, Globals.COGy));
                if (copScatter1.ValuesA.Count > 50) copScatter1.ValuesA.RemoveAt(0);
            }
            copScatter1.ValuesB[0].X = Globals.COGx;
            copScatter1.ValuesB[0].Y = Globals.COGy;
            copScatter1.ValuesB[0].Weight = 1;

            // The alternative .SensorValuesRaw is not adjusted with 17KG and 34KG calibration data, but does that make for better or worse control?
            //
            //var rwTopLeft     = wiiDevice.WiimoteState.BalanceBoardState.SensorValuesRaw.TopLeft     - wiiDevice.WiimoteState.BalanceBoardState.CalibrationInfo.Kg0.TopLeft;
            //var rwTopRight    = wiiDevice.WiimoteState.BalanceBoardState.SensorValuesRaw.TopRight    - wiiDevice.WiimoteState.BalanceBoardState.CalibrationInfo.Kg0.TopRight;
            //var rwBottomLeft  = wiiDevice.WiimoteState.BalanceBoardState.SensorValuesRaw.BottomLeft  - wiiDevice.WiimoteState.BalanceBoardState.CalibrationInfo.Kg0.BottomLeft;
            //var rwBottomRight = wiiDevice.WiimoteState.BalanceBoardState.SensorValuesRaw.BottomRight - wiiDevice.WiimoteState.BalanceBoardState.CalibrationInfo.Kg0.BottomRight;

            // Show the raw sensor values.

            label_rwWT.Text = rwWeight.ToString("0.000");
            label_rwTL.Text = rwTopLeft.ToString("0.000");
            label_rwTR.Text = rwTopRight.ToString("0.000");
            label_rwBL.Text = rwBottomLeft.ToString("0.000");
            label_rwBR.Text = rwBottomRight.ToString("0.000");

            label1.Text = Globals.COGx.ToString("0.000");
            label2.Text = Globals.COGy.ToString("0.000");

            //label3.Text = Globals.COPx.ToString("0.000");
            //label4.Text = Globals.COPy.ToString("0.000");

            // Prevent negative values by tracking lowest possible value and making it a zero based offset.

            if (rwTopLeft     < naCorners) naCorners = rwTopLeft;
            if (rwTopRight    < naCorners) naCorners = rwTopRight;
            if (rwBottomLeft  < naCorners) naCorners = rwBottomLeft;
            if (rwBottomRight < naCorners) naCorners = rwBottomRight;

            // Negative total weight is reset to zero as jumping or lifting the board causes negative spikes, which would break 'in use' checks.

            var owWeight      = rwWeight < 0f ? 0f : rwWeight;

            var owTopLeft     = rwTopLeft     -= naCorners;
            var owTopRight    = rwTopRight    -= naCorners;
            var owBottomLeft  = rwBottomLeft  -= naCorners;
            var owBottomRight = rwBottomRight -= naCorners;

            // Get offset that would make current values the center of mass.

            if (setCenterOffset)
            {
                setCenterOffset = false;

                var rwHighest = Math.Max(Math.Max(rwTopLeft, rwTopRight), Math.Max(rwBottomLeft, rwBottomRight));

                oaTopLeft     = rwHighest - rwTopLeft;
                oaTopRight    = rwHighest - rwTopRight;
                oaBottomLeft  = rwHighest - rwBottomLeft;
                oaBottomRight = rwHighest - rwBottomRight;
            }

            /*// Keep values only when board is being used, otherwise offsets and small value jitters can trigger unwanted actions.

            if (owWeight > 0f)
            {
                owTopLeft     += oaTopLeft;
                owTopRight    += oaTopRight;
                owBottomLeft  += oaBottomLeft;
                owBottomRight += oaBottomRight;
            }
            else
            {
                owTopLeft     = 0;
                owTopRight    = 0;
                owBottomLeft  = 0;
                owBottomRight = 0;
            }

            label_owWT.Text = owWeight.ToString("0.0");
            label_owTL.Text = owTopLeft.ToString("0.0")     + "\r\n" + oaTopLeft.ToString("0.0");
            label_owTR.Text = owTopRight.ToString("0.0")    + "\r\n" + oaTopRight.ToString("0.0");
            label_owBL.Text = owBottomLeft.ToString("0.0")  + "\r\n" + oaBottomLeft.ToString("0.0");
            label_owBR.Text = owBottomRight.ToString("0.0") + "\r\n" + oaBottomRight.ToString("0.0");

            // Calculate each weight ratio.

            var owrPercentage  = 100 / (owTopLeft + owTopRight + owBottomLeft + owBottomRight);
            var owrTopLeft     = owrPercentage * owTopLeft;
            var owrTopRight    = owrPercentage * owTopRight;
            var owrBottomLeft  = owrPercentage * owBottomLeft;
            var owrBottomRight = owrPercentage * owBottomRight;

            label_owrTL.Text = owrTopLeft.ToString("0.0");
            label_owrTR.Text = owrTopRight.ToString("0.0");
            label_owrBL.Text = owrBottomLeft.ToString("0.0");
            label_owrBR.Text = owrBottomRight.ToString("0.0");

            // Calculate balance ratio.

            var brX = owrBottomRight + owrTopRight;
            var brY = owrBottomRight + owrBottomLeft;

            label_brX.Text = brX.ToString("0.0");
            label_brY.Text = brY.ToString("0.0");*/
            
        }

        private void checkBox_EnableJoystick_CheckedChanged(object sender, EventArgs e)
        {
            // Start joystick emulator.

            try
            {
                joyDevice = new VJoy();
                joyDevice.Initialize();
                joyDevice.Reset();
                joyDevice.Update(0);
            }
            catch (Exception ex)
            {
                // VJoy.DLL missing from .EXE folder or project built as 'Any CPU' and DLL is 32-bit only.

                infoUpdateTimer.Enabled = false;
                MessageBox.Show(ex.Message, "VJoy Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            joyResetTimer.Enabled = true;

            // Show reminder ( if not being changed by load settings ) and save settings.

            var isChecked = ((CheckBox)sender).Checked;
            if (isChecked)
            {
                if (Properties.Settings.Default.EnableJoystick == false)
                {
                    MessageBox.Show("Actions still apply! Set 'Do Nothing' for any movement conflicts.\r\n\r\nRequires Headsoft VJoy driver to be installed.", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            Properties.Settings.Default.EnableJoystick = isChecked;
            Properties.Settings.Default.Save();
        }

        void joyResetTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            joyDevice.Initialize();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop updates.

            infoUpdateTimer.Enabled = false;
            wiiDevice.Disconnect();

            // Prevent 'stuck' down keys from closing while doing an action.

            actionList.Left.Stop();
            actionList.Right.Stop();
            actionList.Forward.Stop();
            actionList.Backward.Stop();
            actionList.Modifier.Stop();
            actionList.Jump.Stop();
            actionList.DiagonalLeft.Stop();
            actionList.DiagonalRight.Stop();
        }

        private void groupBox_OffsetWeight_Enter(object sender, EventArgs e)
        {

        }

        //pause
        private void button2_Click(object sender, EventArgs e)
        {
            if (infoUpdateTimer.Enabled == true)
            {
                button2.Text = "Resume";
                infoUpdateTimer.Enabled = false;
            }
            else
            {
                button2.Text = "Pause";
                infoUpdateTimer.Enabled = true;
            }
            
        }

        //start button
        private void button3_Click(object sender, EventArgs e)
        {
            if(Globals.TimerOn == true)
            {
                print();
                Globals.TimerOn = false;
                button3.Text = "Start";
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {

                    //MAX 30 SECONDS
                    int parsedInt = 0;
                    if (int.TryParse(textBox1.Text, out parsedInt))
                    {
                        Globals.TimerOn = true;
                        button3.Text = "Stop";
                        //infoUpdateTimer.Enabled = true;
                        Globals.Time = parsedInt;
                        Globals.Period = parsedInt * 20; //20Hz
                        //maybe change to 20
                    }



                }
            }
           
            /*if (infoUpdateTimer.Enabled == true)
            {
                //stopped
                button3.Text = "Start";
                infoUpdateTimer.Enabled = false;
                print();
                Array.Clear(Globals.COGxArray, 0, 600);
                Array.Clear(Globals.COGyArray, 0, 600);

            }
            else
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    
                    //MAX 30 SECONDS
                    int parsedInt = 0;
                    if (int.TryParse(textBox1.Text, out parsedInt))
                    {
                        button3.Text = "Stop";
                        infoUpdateTimer.Enabled = true;
                        Globals.Time = parsedInt;
                        Globals.Period = parsedInt * 50;
                    }
                    

                    
                }
            }*/
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //need to (check Time * 50) - Period to see how much of the last whatever values we do need to print out
            print();
        }

        private void print()
        {
            string strFilePath = @"C:\testfile.csv";
            string strSeperator = ",";
            StringBuilder sbOutput = new StringBuilder();

            /*int[][] inaOutput = new int[][]{
                new int[]{1000, 2000, 3000, 4000, 5000},
                new int[]{6000, 7000, 8000, 9000, 10000},
                new int[]{11000, 12000, 13000, 14000, 15000}
            };*/
            String[] headers = { "Time (seconds)", "COGx (cm)", "COGy (cm)", "TL (kgf)", "BL (kgf)", "TR (kgf)", "BR (kgf)" };
            sbOutput.AppendLine(string.Join(strSeperator, headers));
            double[] OutputRow = { 0, 0, 0, 0, 0, 0, 0 };

            //int ilength = inaOutput.GetLength(0);
            int j = 0;
            for (int i = 600 - (Globals.Time * 20) + Globals.Period; i < 600; i++){
                OutputRow[0] = 0.05 * j;
                OutputRow[1] = Globals.COGxArray[i];
                OutputRow[2] = Globals.COGyArray[i];
                OutputRow[3] = Globals.TLArray[i];
                OutputRow[4] = Globals.BLArray[i];
                OutputRow[5] = Globals.TRArray[i];
                OutputRow[6] = Globals.BRArray[i];

                sbOutput.AppendLine(string.Join(strSeperator, OutputRow));
                j++;
            }



            // Create and write the csv file
            File.WriteAllText(strFilePath, sbOutput.ToString());

            // To append more lines to the csv file
            File.AppendAllText(strFilePath, sbOutput.ToString());

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Globals.TraceOn)
            {
                Globals.TraceOn = false;
                while(copScatter1.ValuesA.Count > 0)
                {
                    copScatter1.ValuesA.RemoveAt(0);
                }

            } else
            {
                Globals.TraceOn = true;
            }
        }
    }
}
