/*
   Copyright 2016-2020 LIPtoH <liptoh.codebase@gmail.com>

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace TS_SE_Tool
{
    public partial class FormMain
    {
        //User Trucks tab
        private void CreateTruckPanelControls()
        {
            CreateTruckPanelMainButtons();
            CreateTruckPanelPartsControls();
        }

        private void CreateTruckPanelMainButtons()
        {
            int pHeight = RepairImg.Height, pOffset = 5, tOffset = comboBoxUserTruckCompanyTrucks.Location.Y;
            int topbutoffset = comboBoxUserTruckCompanyTrucks.Location.X + comboBoxUserTruckCompanyTrucks.Width + pOffset;

            Button buttonInfo = new Button();
            tableLayoutPanelUserTruckControls.Controls.Add(buttonInfo, 3, 0);
            buttonInfo.FlatStyle = FlatStyle.Flat;
            buttonInfo.Size = new Size(CustomizeImg.Width, CustomizeImg.Height);
            buttonInfo.Name = "buttonTruckInfo";
            buttonInfo.BackgroundImage = CustomizeImg;
            buttonInfo.BackgroundImageLayout = ImageLayout.Zoom;
            buttonInfo.Text = "";
            buttonInfo.FlatAppearance.BorderSize = 0;
            buttonInfo.Enabled = false;
            buttonInfo.Dock = DockStyle.Fill;

            Button buttonR = new Button();
            tableLayoutPanelUserTruckControls.Controls.Add(buttonR, 1, 0);
            buttonR.FlatStyle = FlatStyle.Flat;
            buttonR.Size = new Size(RepairImg.Height, RepairImg.Height);
            buttonR.Name = "buttonTruckRepair";
            buttonR.BackgroundImage = RepairImg;
            buttonR.BackgroundImageLayout = ImageLayout.Zoom;
            buttonR.Text = "";
            buttonR.FlatAppearance.BorderSize = 0;
            buttonR.Click += new EventHandler(buttonTruckRepair_Click);
            buttonR.EnabledChanged += new EventHandler(buttonElRepair_EnabledChanged);
            buttonR.Dock = DockStyle.Fill;

            Button buttonF = new Button();
            tableLayoutPanelUserTruckControls.Controls.Add(buttonF, 2, 0);
            buttonF.FlatStyle = FlatStyle.Flat;
            buttonF.Size = new Size(RepairImg.Height, RepairImg.Height);
            buttonF.Name = "buttonTruckReFuel";
            buttonF.BackgroundImage = RefuelImg;
            buttonF.BackgroundImageLayout = ImageLayout.Zoom;
            buttonF.Text = "";
            buttonF.FlatAppearance.BorderSize = 0;
            buttonF.Click += new EventHandler(buttonTruckReFuel_Click);
            buttonF.EnabledChanged += new EventHandler(buttonRefuel_EnabledChanged);
            buttonF.Dock = DockStyle.Fill;
        }

        private void CreateTruckPanelPartsControls()
        {
            int pSkillsNameHeight = 32, pSkillsNameWidth = 32;

            string[] toolskillimgtooltip = new string[] { "Engine", "Transmission", "Chassis", "Cabin", "Wheels" };
            Label partLabel, partnameLabel;
            Panel pbPanel;

            for (int i = 0; i < 5; i++)
            {
                //Create table layout
                TableLayoutPanel tbllPanel = new TableLayoutPanel();
                tableLayoutPanelTruckDetails.Controls.Add(tbllPanel, 0, i);
                tbllPanel.Dock = DockStyle.Fill;
                tbllPanel.Margin = new Padding(0);
                //
                tbllPanel.Name = "tableLayoutPanelTruckDetails" + toolskillimgtooltip[i];

                tbllPanel.ColumnCount = 3;
                tbllPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
                tbllPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                tbllPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
                tbllPanel.RowCount = 2;
                tbllPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 18F));
                tbllPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                //

                FlowLayoutPanel flowPanel = new FlowLayoutPanel();
                flowPanel.FlowDirection = FlowDirection.LeftToRight;
                flowPanel.Margin = new Padding(0);
                tbllPanel.SetColumnSpan(flowPanel, 2);
                tbllPanel.Controls.Add(flowPanel, 0, 0);

                //Part type
                partLabel = new Label();
                partLabel.Name = "labelTruckPartName" + toolskillimgtooltip[i];
                partLabel.Text = toolskillimgtooltip[i];
                partLabel.AutoSize = true;
                partLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                partLabel.MinimumSize = new Size(36, partLabel.Height);

                flowPanel.Controls.Add(partLabel);

                //Part name
                partnameLabel = new Label();
                partnameLabel.Name = "labelTruckPartDataName" + i;
                partnameLabel.Text = "";
                partnameLabel.AutoSize = true;
                partnameLabel.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;

                flowPanel.Controls.Add(partnameLabel);

                //Part type image
                Panel imgpanel = new Panel();
                imgpanel.BorderStyle = BorderStyle.None;
                imgpanel.Size = new Size(pSkillsNameWidth, pSkillsNameHeight);
                imgpanel.Margin = new Padding(1);
                imgpanel.Name = "TruckPartImg" + i.ToString();

                Bitmap bgimg = new Bitmap(TruckPartsImg[i], pSkillsNameHeight, pSkillsNameWidth);
                imgpanel.BackgroundImage = bgimg;
                tbllPanel.Controls.Add(imgpanel, 0, 1);

                //Progress bar panel 
                pbPanel = new Panel();
                pbPanel.BorderStyle = BorderStyle.FixedSingle;
                pbPanel.Name = "progressbarTruckPart" + i.ToString();
                pbPanel.Dock = DockStyle.Fill;
                tbllPanel.Controls.Add(pbPanel, 1, 1);

                //Repair button
                Button button = new Button();
                button.FlatStyle = FlatStyle.Flat;
                button.Dock = DockStyle.Fill;
                button.Margin = new Padding(1);

                button.Name = "buttonTruckElRepair" + i.ToString();
                button.BackgroundImage = RepairImg;
                button.BackgroundImageLayout = ImageLayout.Zoom;
                button.Text = "";
                button.FlatAppearance.BorderSize = 0;
                button.Click += new EventHandler(buttonElRepair_Click);
                button.EnabledChanged += new EventHandler(buttonElRepair_EnabledChanged);

                tbllPanel.Controls.Add(button, 2, 1);
            }

            //Fuel panel
            Panel Ppanelf = new Panel();
            Ppanelf.BorderStyle = BorderStyle.FixedSingle;
            Ppanelf.Dock = DockStyle.Fill;
            Ppanelf.Name = "progressbarTruckFuel";

            //label - Fuel
            Label labelF = new Label();
            labelF.Name = "labelTruckDetailsFuel";
            labelF.Text = "Fuel";
            labelF.AutoSize = true;
            labelF.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            labelF.TextAlign = ContentAlignment.MiddleCenter;

            tableLayoutPanelTruckFuel.Controls.Add(Ppanelf, 0, 1);
            tableLayoutPanelTruckFuel.Controls.Add(labelF, 0, 0);

            //License plate
            Label labelPlate = new Label();
            labelPlate.Name = "labelUserTruckLicensePlate";
            labelPlate.Text = "License plate";
            labelPlate.Margin = new Padding(0);
            labelPlate.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            labelPlate.TextAlign = ContentAlignment.MiddleCenter;

            Label lcPlate = new Label();
            lcPlate.Name = "labelLicensePlate";
            lcPlate.Text = "A 000 AA";
            lcPlate.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
            lcPlate.Dock = DockStyle.Fill;
            lcPlate.TextAlign = ContentAlignment.MiddleLeft;

            tableLayoutPanelTruckLP.Controls.Add(labelPlate, 0, 0);
            tableLayoutPanelTruckLP.Controls.Add(lcPlate, 1, 0);

            //
            Panel LPpanel = new Panel();
            LPpanel.Dock = DockStyle.Fill;
            LPpanel.Margin = new Padding(0);
            LPpanel.Name = "TruckLicensePlateIMG";
            LPpanel.BackgroundImageLayout = ImageLayout.Center;

            tableLayoutPanelTruckLP.Controls.Add(LPpanel, 2, 0);
        }

        private void FillUserCompanyTrucksList()
        {
            DataTable combDT = new DataTable();
            DataColumn dc = new DataColumn("UserTruckNameless", typeof(string));
            combDT.Columns.Add(dc);

            //dc = new DataColumn("UserTruckName", typeof(string));
            //combDT.Columns.Add(dc);
            //

            dc = new DataColumn("TruckType", typeof(string));
            combDT.Columns.Add(dc);

            dc = new DataColumn("TruckName", typeof(string));
            combDT.Columns.Add(dc);

            dc = new DataColumn("GarageName", typeof(string));
            combDT.Columns.Add(dc);

            dc = new DataColumn("DriverName", typeof(string));
            combDT.Columns.Add(dc);

            DataColumn dcDisplay = new DataColumn("DisplayMember");
            dcDisplay.Expression = string.Format("IIF(UserTruckNameless <> '', '[' + {0} +'] ' + IIF(GarageName <> '', {1} +' || ','') + {2} + IIF(DriverName <> 'null', ' || In use - ' + {3},'')," +
                                                        "'-- NONE --')",
                                                "TruckType", "GarageName", "TruckName", "DriverName");
            combDT.Columns.Add(dcDisplay);
            //

            foreach (KeyValuePair<string, UserCompanyTruckData> UserTruck in UserTruckDictionary)
            {
                string truckname = "";

                try
                {
                    string templine = UserTruck.Value.Parts.Find(x => x.PartType == "truckbrandname").PartData.Find(xline => xline.StartsWith(" data_path:"));
                    truckname = templine.Split(new char[] { '"' })[1].Split(new char[] { '/' })[4];
                }
                catch { }

                TruckBrandsLngDict.TryGetValue(truckname, out string trucknamevalue);
                //
                string tmpTruckType = "", tmpTruckName = "", tmpGarageName = "", tmpDriverName = "";

                if (UserTruckDictionary[UserTruck.Key].Users)
                {
                    tmpTruckType = "U";

                    tmpGarageName = GaragesList.Find(x => x.Vehicles.Contains(UserTruck.Key)).GarageNameTranslated;
                }
                else
                    tmpTruckType = "Q";
                //
                if (trucknamevalue != null && trucknamevalue != "")
                {
                    tmpTruckName = trucknamevalue;
                }
                else
                {
                    tmpTruckName = truckname;
                }

                Garages tmpGrg = GaragesList.Where(tX => tX.Vehicles.Contains(UserTruck.Key))?.SingleOrDefault() ?? null;

                if (tmpGrg != null)
                {
                    tmpDriverName = tmpGrg.Drivers[tmpGrg.Vehicles.IndexOf(UserTruck.Key)];
                }
                else
                {
                    tmpDriverName = UserDriverDictionary.Where(tX => tX.Value.AssignedTruck == UserTruck.Key)?.SingleOrDefault().Key ?? "null";
                }
                
                if (tmpDriverName != null && tmpDriverName != "null")
                    if (PlayerDataData.UserDriver == tmpDriverName)
                    {
                        tmpDriverName = "> " + Utilities.TextUtilities.FromHexToString(Globals.SelectedProfile);
                    }
                    else
                    {
                        DriverNames.TryGetValue(tmpDriverName, out string _resultvalue);

                        if (_resultvalue != null && _resultvalue != "")
                        {
                            tmpDriverName = _resultvalue.TrimStart(new char[] { '+' });
                        }
                    }
                
                combDT.Rows.Add(UserTruck.Key, tmpTruckType, tmpTruckName, tmpGarageName, tmpDriverName);
            }

            bool noTrucks = false;
            
            if (combDT.Rows.Count == 0)
            {
                combDT.Rows.Add("null");//, "-- NONE --"); //none
                noTrucks = true;
            }
            
            comboBoxUserTruckCompanyTrucks.ValueMember = "UserTruckNameless";
            comboBoxUserTruckCompanyTrucks.DisplayMember = "DisplayMember";
            comboBoxUserTruckCompanyTrucks.DataSource = combDT;

            if (!noTrucks)
            {
                //combDT.DefaultView.Sort = "UserTruckName ASC";
                comboBoxUserTruckCompanyTrucks.Enabled = true;
                comboBoxUserTruckCompanyTrucks.SelectedValue = PlayerDataData.UserCompanyAssignedTruck;
            }
            else
            {
                comboBoxUserTruckCompanyTrucks.Enabled = false;
                comboBoxUserTruckCompanyTrucks.SelectedValue = "null";
            }
        }
        //
        private void UpdateTruckPanelDetails()
        {
            for (byte i = 0; i < 5; i++)
                UpdateTruckPanelProgressBar(i);

            CheckTruckRepair();

            UpdateTruckPanelFuel();
            UpdateTruckPanelLicensePlate();
        }

        private void UpdateTruckPanelProgressBar(byte _number)
        {
            UserTruckDictionary.TryGetValue(comboBoxUserTruckCompanyTrucks.SelectedValue.ToString(), out UserCompanyTruckData SelectedUserCompanyTruck);

            if (SelectedUserCompanyTruck == null)
                return;

            string pnlname = "progressbarTruckPart" + _number.ToString(), labelPartName = "labelTruckPartDataName" + _number.ToString();

            //Progres bar
            Panel pbPanel = groupBoxUserTruckTruckDetails.Controls.Find(pnlname, true).FirstOrDefault() as Panel;
            
            //Part name
            Label pnLabel = groupBoxUserTruckTruckDetails.Controls.Find(labelPartName, true).FirstOrDefault() as Label;
            
            //Repair button
            Button repairButton = groupBoxUserTruckTruckDetails.Controls.Find("buttonTruckElRepair" + _number, true).FirstOrDefault() as Button;

            if (pbPanel != null)
            {
                List<UserCompanyTruckDataPart> TruckDataPart = null;

                try
                {
                    switch (_number)
                    {
                        case 0:
                            TruckDataPart = SelectedUserCompanyTruck.Parts.FindAll(xp => xp.PartType == "engine");
                            break;
                        case 1:
                            TruckDataPart = SelectedUserCompanyTruck.Parts.FindAll(xp => xp.PartType == "transmission");
                            break;
                        case 2:
                            TruckDataPart = SelectedUserCompanyTruck.Parts.FindAll(xp => xp.PartType == "chassis");
                            break;
                        case 3:
                            TruckDataPart = SelectedUserCompanyTruck.Parts.FindAll(xp => xp.PartType == "cabin");
                            break;
                        case 4:
                            TruckDataPart = SelectedUserCompanyTruck.Parts.FindAll(xp => xp.PartType == "tire");
                            break;
                    }
                }
                catch
                {
                    repairButton.Enabled = false;
                    return;
                }

                decimal _wear = 0;
                byte partCount = 0;

                if (TruckDataPart != null && TruckDataPart.Count > 0)
                {
                    if (pnLabel != null)
                    {
                        pnLabel.Text = TruckDataPart[0].PartData.Find(xl => xl.StartsWith(" data_path:")).Split(new char[] { '"' })[1].Split(new char[] { '/' }).Last().Split(new char[] { '.' })[0];
                    }

                    foreach (UserCompanyTruckDataPart tmpPartData in TruckDataPart)
                    {
                        try
                        {
                            string tmpWear = tmpPartData.PartData.Find(xl => xl.StartsWith(" wear:")).Split(new char[] { ' ' })[2];
                            decimal _tmpWear = 0;

                            if (tmpWear != "0" && tmpWear != "1")
                            {
                                _tmpWear = Utilities.NumericUtilities.HexFloatToDecimalFloat(tmpWear);
                            }
                            else if (tmpWear == "1")
                            {
                                _tmpWear = 1;
                            }

                            _wear += _tmpWear;
                            partCount++;
                        }
                        catch
                        { }
                    }
                }
                else
                {
                    pnLabel.Text = "!! Part not found !!";
                }

                _wear = _wear / partCount;

                if (_wear == 0)                
                    repairButton.Enabled = false;                
                else
                    repairButton.Enabled = true;

                //
                SolidBrush ppen = new SolidBrush(GetProgressbarColor(_wear));

                int x = 0, y = 0, pnlwidth = (int)(pbPanel.Width * (1 - _wear));

                Bitmap progress = new Bitmap(pbPanel.Width, pbPanel.Height);

                Graphics g = Graphics.FromImage(progress);
                g.FillRectangle(ppen, x, y, pnlwidth, pbPanel.Height);

                int fontSize = 12;
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                GraphicsPath p = new GraphicsPath();
                p.AddString(
                    ((int)((1 - _wear) * 100)).ToString() + " %",   // text to draw
                    FontFamily.GenericSansSerif,                    // or any other font family
                    (int)FontStyle.Bold,                            // font style (bold, italic, etc.)
                    g.DpiY * fontSize / 72,                         // em size
                    new Rectangle(0, 0, pbPanel.Width, pbPanel.Height),     // location where to draw text
                    sf);                                            // set options here (e.g. center alignment)
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillPath(Brushes.Black, p);
                g.DrawPath(Pens.Black, p);

                pbPanel.BackgroundImage = progress;
            }
        }

        private void CheckTruckRepair()
        {
            bool repairEnabled = false;
            Button repairTruck = tableLayoutPanelUserTruckControls.Controls.Find("buttonTruckRepair", true).FirstOrDefault() as Button;

            for (byte i = 0; i < 5; i++)
            {
                try
                {
                    Button tmp = groupBoxUserTruckTruckDetails.Controls.Find("buttonTruckElRepair" + i, true).FirstOrDefault() as Button;
                    if (tmp.Enabled)
                    {
                        repairEnabled = true;
                        break;
                    }
                }
                catch
                {
                    continue;
                }
            }

            repairTruck.Enabled = repairEnabled;
        }

        private void UpdateTruckPanelFuel()
        {
            UserTruckDictionary.TryGetValue(comboBoxUserTruckCompanyTrucks.SelectedValue.ToString(), out UserCompanyTruckData SelectedUserCompanyTruck);

            string pnlnamefuel = "progressbarTruckFuel";
            Panel pnlfuel = groupBoxUserTruckTruckDetails.Controls.Find(pnlnamefuel, true).FirstOrDefault() as Panel;

            Button refuelTruck = tableLayoutPanelUserTruckControls.Controls.Find("buttonTruckReFuel", true).FirstOrDefault() as Button;

            if (pnlfuel != null)
            {
                string fuel = SelectedUserCompanyTruck.Parts.Find(xp => xp.PartType == "truckdata").PartData.Find(xl => xl.StartsWith(" fuel_relative:")).Split(new char[] { ' ' })[2];//SelectedUserCompanyTruck.Fuel;
                decimal _fuel = 0;

                if (fuel != "0" && fuel != "1")
                {
                    refuelTruck.Enabled = true;
                    _fuel = Utilities.NumericUtilities.HexFloatToDecimalFloat(fuel);
                }
                else if (fuel == "1")
                {
                    refuelTruck.Enabled = false;
                    _fuel = 1;
                }
                else
                    refuelTruck.Enabled = true;

                SolidBrush ppen = new SolidBrush(GetProgressbarColor(1 - _fuel));
                int pnlheight = (int)(pnlfuel.Height * (_fuel)), x = 0, y = pnlfuel.Height - pnlheight;

                Bitmap progress = new Bitmap(pnlfuel.Width, pnlfuel.Height);

                Graphics g = Graphics.FromImage(progress);
                g.FillRectangle(ppen, x, y, pnlfuel.Width, pnlheight);

                int fontSize = 10;
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Center;
                sf.Alignment = StringAlignment.Center;

                GraphicsPath p = new GraphicsPath();
                p.AddString(
                    ((int)(_fuel * 100)).ToString() + " %",             // text to draw
                    FontFamily.GenericSansSerif,                        // or any other font family
                    (int)FontStyle.Regular,                             // font style (bold, italic, etc.)
                    g.DpiY * fontSize / 72,                             // em size
                    new Rectangle(0, 0, pnlfuel.Width, pnlfuel.Height), // location where to draw text
                    sf);                                                // set options here (e.g. center alignment)
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.FillPath(Brushes.Black, p);
                g.DrawPath(Pens.Black, p);

                pnlfuel.BackgroundImage = progress;
            }
        }
        
        private void UpdateTruckPanelLicensePlate()
        {
            UserTruckDictionary.TryGetValue(comboBoxUserTruckCompanyTrucks.SelectedValue.ToString(), out UserCompanyTruckData SelectedUserCompanyTruck);
            string LicensePlate = SelectedUserCompanyTruck.Parts.Find(xp => xp.PartType == "truckdata").PartData.Find(xl => xl.StartsWith(" license_plate:")).Split(new char[] { '"' })[1];

            SCS.SCSLicensePlate thisLP = new SCS.SCSLicensePlate(LicensePlate, SCS.SCSLicensePlate.LPtype.Truck);

            //Find label control
            Label lpText = groupBoxUserTruckTruckDetails.Controls.Find("labelLicensePlate", true).FirstOrDefault() as Label;
            if (lpText != null)
            {
                lpText.Text = thisLP.LicensePlateTXT + " | ";

                string value = null;
                CountriesLngDict.TryGetValue(thisLP.SourceLPCountry, out value);

                if (value != null && value != "")
                {
                    lpText.Text += value;
                }
                else
                {
                    string CapName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(thisLP.SourceLPCountry);
                    lpText.Text += CapName;
                }
            }

            //
            Panel lpPanel = groupBoxUserTruckTruckDetails.Controls.Find("TruckLicensePlateIMG", true).FirstOrDefault() as Panel;
            if (lpPanel != null)
            {
                lpPanel.BackgroundImage = Utilities.TS_Graphics.ResizeImage(thisLP.LicensePlateIMG, LicensePlateWidth[GameType], 32); //ETS - 128x32 or ATS - 128x64 | 64x32
            }
        }

        //Events
        private void comboBoxCompanyTrucks_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmbbx = sender as ComboBox;

            if (cmbbx.SelectedValue != null && cmbbx.SelectedValue.ToString() != "null") //cmbbx.SelectedIndex != -1 && 
            {
                ToggleTruckPartsCondition(true);

                buttonUserTruckSelectCurrent.Enabled = true;
                tableLayoutPanelUserTruckControls.Enabled = true;

                groupBoxUserTruckTruckDetails.Enabled = true;
                groupBoxUserTruckShareTruckSettings.Enabled = true;

                UpdateTruckPanelDetails();
            }
            else
            {
                ToggleTruckPartsCondition(false);

                buttonUserTruckSelectCurrent.Enabled = false;
                tableLayoutPanelUserTruckControls.Enabled = false;

                groupBoxUserTruckTruckDetails.Enabled = false;
                groupBoxUserTruckShareTruckSettings.Enabled = false;
            }
        }

        private void groupBoxUserTruckTruckDetails_EnabledChanged(object sender, EventArgs e)
        {
            ToggleVisualTruckDetails(groupBoxUserTruckTruckDetails.Enabled);
        }

        private void tableLayoutPanelUserTruckControls_EnabledChanged(object sender, EventArgs e)
        {
            ToggleVisualTruckControls(tableLayoutPanelUserTruckControls.Enabled);
        }

        private void ToggleVisualTruckDetails(bool _state)
        {
            for (int i = 0; i < 5; i++)
            {
                Control[] tmp = tabControlMain.TabPages["tabPageTruck"].Controls.Find("buttonTruckElRepair" + i.ToString(), true);
                if (_state)
                    tmp[0].BackgroundImage = RepairImg;
                else
                    tmp[0].BackgroundImage = ConvertBitmapToGrayscale(RepairImg);
            }
        }

        private void ToggleVisualTruckControls(bool _state)
        {
            Control TMP;

            string[] buttons = { "buttonTruckReFuel", "buttonTruckRepair", "buttonTruckInfo" };
            Image[] images = { RefuelImg, RepairImg, CustomizeImg };

            for (int i = 0; i < buttons.Count(); i++)
            {
                try
                {
                    TMP = tabControlMain.TabPages["tabPageTruck"].Controls.Find(buttons[i], true)[0];
                }
                catch
                {
                    break;
                }
                
                if (_state && TMP.Enabled)
                    TMP.BackgroundImage = images[i];
                else
                    TMP.BackgroundImage = ConvertBitmapToGrayscale(images[i]);
            }
        }

        private void ToggleTruckPartsCondition(bool _state)
        {
            if (!_state)
            {
                string lblname, pnlname;

                for (int i = 0; i < 5; i++)
                {
                    lblname = "labelTruckPartDataName" + i.ToString();
                    Label pnLabel = groupBoxUserTruckTruckDetails.Controls.Find(lblname, true).FirstOrDefault() as Label;

                    if (pnLabel != null)
                        pnLabel.Text = "";

                    pnlname = "progressbarTruckPart" + i.ToString();
                    Panel pbPanel = groupBoxUserTruckTruckDetails.Controls.Find(pnlname, true).FirstOrDefault() as Panel;

                    if (pbPanel != null)                    
                        pbPanel.BackgroundImage = null;                    
                }

                string pnlFname =  "progressbarTruckFuel";
                Panel pnlF = groupBoxUserTruckTruckDetails.Controls.Find(pnlFname, true).FirstOrDefault() as Panel;

                if (pnlF != null)                
                    pnlF.BackgroundImage = null;

                string lblLCname = "labelLicensePlate";
                Label lblLC = groupBoxUserTruckTruckDetails.Controls.Find(lblLCname, true).FirstOrDefault() as Label;

                if (lblLC != null)
                    lblLC.Text = "A 000 AA";

                pnlname = "TruckLicensePlateIMG";
                Panel LPPanel = groupBoxUserTruckTruckDetails.Controls.Find(pnlname, true).FirstOrDefault() as Panel;

                if (LPPanel != null)
                    LPPanel.BackgroundImage = null;
            }
        }
        //Buttons
        public void buttonTruckReFuel_Click(object sender, EventArgs e)
        {
            int i = 0;
            foreach (string temp in UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(x => x.PartType == "truckdata").PartData)
            {
                if (temp.StartsWith(" fuel_relative:"))
                {
                    UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(x => x.PartType == "truckdata").PartData[i] = " fuel_relative: 1";
                    break;
                }
                i++;
            }

            UpdateTruckPanelFuel();
            //UpdateTruckPanelProgressBars();
        }

        public void buttonTruckRepair_Click(object sender, EventArgs e)
        {
            string[] PartList = { "engine", "transmission", "chassis", "cabin", "tire" };

            foreach (string tempPart in PartList)
            {
                foreach (UserCompanyTruckDataPart temp in UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.FindAll(x => x.PartType == tempPart))
                {
                    string partNameless = temp.PartNameless;

                    int i = 0;

                    foreach (string temp2 in temp.PartData)
                    {
                        if (temp2.StartsWith(" wear:"))
                        {
                            UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(x => x.PartNameless == partNameless).PartData[i] = " wear: 0";
                            break;
                        }
                        i++;
                    }
                }
            }

            for (byte i = 0; i < 5; i++)
                UpdateTruckPanelProgressBar(i);

            CheckTruckRepair();
        }
        //
        public void buttonElRepair_Click(object sender, EventArgs e)
        {
            Button curbtn = sender as Button;
            byte bi = Convert.ToByte(curbtn.Name.Substring(19));

            string[] PartList = { "engine", "transmission", "chassis", "cabin", "tire" };

            foreach (UserCompanyTruckDataPart tempPart in UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.FindAll(x => x.PartType == PartList[bi]))
            {
                string partNameless = tempPart.PartNameless;

                int i = 0;

                foreach (string tempPartData in tempPart.PartData)
                {
                    if (tempPartData.StartsWith(" wear:"))
                    {
                        UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(x => x.PartNameless == partNameless).PartData[i] = " wear: 0";
                        break;
                    }
                    i++;
                }
            }

            UpdateTruckPanelProgressBar(bi);

            CheckTruckRepair();
        }
        //
        public void buttonElRepair_EnabledChanged(object sender, EventArgs e)
        {
            Button tmp = sender as Button;

            if (tmp.Enabled)
                tmp.BackgroundImage = RepairImg;
            else
                tmp.BackgroundImage = ConvertBitmapToGrayscale(RepairImg);
        }

        public void buttonRefuel_EnabledChanged(object sender, EventArgs e)
        {
            Button tmp = sender as Button;

            if (tmp.Enabled)
                tmp.BackgroundImage = RefuelImg;
            else
                tmp.BackgroundImage = ConvertBitmapToGrayscale(RefuelImg);
        }
        //
        private void buttonUserTruckSelectCurrent_Click(object sender, EventArgs e)
        {
            comboBoxUserTruckCompanyTrucks.SelectedValue = PlayerDataData.UserCompanyAssignedTruck;
        }

        private void buttonUserTruckSwitchCurrent_Click(object sender, EventArgs e)
        {
            PlayerDataData.UserCompanyAssignedTruck = comboBoxUserTruckCompanyTrucks.SelectedValue.ToString();
        }
        //
        //Share buttons
        private void buttonTruckPaintCopy_Click(object sender, EventArgs e)
        {
            string tempPaint = "TruckPaint\r\n";

            List<string> paintstr = UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(xp => xp.PartType == "paintjob").PartData;

            foreach (string temp in paintstr)
            {
                tempPaint += temp + "\r\n";
            }

            string tmpString = BitConverter.ToString(Utilities.ZipDataUtilitiescs.zipText(tempPaint)).Replace("-", "");
            Clipboard.SetText(tmpString);
            MessageBox.Show("Paint data has been copied.");
        }

        private void buttonTruckPaintPaste_Click(object sender, EventArgs e)
        {
            try
            {
                string inputData = Utilities.ZipDataUtilitiescs.unzipText(Clipboard.GetText());
                string[] Lines = inputData.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                if (Lines[0] == "TruckPaint")
                {
                    List<string> paintstr = new List<string>();
                    for (int i = 1; i < Lines.Length; i++)
                    {
                        paintstr.Add(Lines[i]);
                    }

                    UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(xp => xp.PartType == "paintjob").PartData = paintstr;

                    MessageBox.Show("Paint data  has been inserted.");
                }
                else
                    MessageBox.Show("Wrong data. Expected Paint data but\r\n" + Lines[0] + "\r\nwas found.");
            }
            catch
            {
                MessageBox.Show("Something gone wrong with decoding.");
            }
        }

        private void buttonTruckCapricornsPaste_Click(object sender, EventArgs e)
        {
            try
            {
                string truckname = "";
                try
                {
                    string templine = UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(x => x.PartType == "truckbrandname").PartData.Find(xline => xline.StartsWith(" data_path:"));
                    truckname = templine.Split(new char[] { '"' })[1].Split(new char[] { '/' })[4];
                }
                catch { }
                TruckBrandsLngDict.TryGetValue(truckname, out string trucknamevalue);

                string TruckName = "";

                if (trucknamevalue != null && trucknamevalue != "")
                {
                    TruckName += trucknamevalue;
                }
                else
                {
                    TruckName += truckname;
                }

                string decode = "";
                if (TruckName.Equals("MAN TGX"))
                {
                    decode = "1F8B0800A6CC995E00FF5590CB6AC3301045F781FC83C8A2B41022C9D1335FD145F762248D13D5766C6CF541BFBE96D387B3BB0CE70E87FB32BE85E619D2356F37A483A971A30B7DDB8F27F2F870AC75652A2FF6648E2C4A065589D1EA3A0AF3F4DB38AF1A511A2314EC092B2432292AC3FE48BF22BD50E815FF2123D7DC2A59C8BA850657201A258495CB3F691447BB14400B79B4B7421AEEA4BD16212ED21251627DF38728010BEF61FAFFCF168145F10361BEB03945C8E006C89713D9D188357DC74B0A2DD25CE6A21D5C0FF9FC4987B29B7BED3DFD4A1D70D7810B8729A5DD76F30DD14F5F1558010000";
                }
                else if (TruckName.Equals("MAN TGX Euro 6"))
                {
                    decode = "1F8B0800CECC995E00FF5590CB6EC3201045F791F20F288BAA95A2801DC090AFE8A27B34C09050DBB16593B6EAD7D7903EDCDDD5E8DCD1D17D996EAE7D86784DDB0DE9616ECD64DCD00DD3893C3E1C4353ABDAF23D5922F382419DA3D74DF05C3DFD34CEAB86174A71097BC232894CF05AB15FD2AE48CB255A597D93BE6A2A2D452643072DAE405492732DCA3FA16485BA14A0E1E2A8EF8538FE93B60D77BE480B4481E1EE0F5E0066DEC2FCF79F1581A2F88EB05CD8923C243023A4CB89ECA8C740DFF0125D8734E5B9680FD7433A7F18BC4D83A4635ECFBC0E967EC61E2AD34BE30E738CBBEDE60BF79C6FCB5E010000";
                }
                else if (TruckName.Equals("Mercedes Actros"))
                {
                    decode = "1F8B0800E2CC995E00FF5590CB6EC3201045F791F20F288BAA95A2000E609CAFE8A27B3430E386DAAE2D4C5BA95F5FE3F4E1EEAE46E78E8EEE537A0BDD23C4D7BCDFB101E6CE2517C67E4C17767F776EEBCA565E1DD912056A015589D8D42D2AFBF0D378DE34505BAB0C1C99282409AD2A2B7E49BF21BD32E48DFC2651D6B231BA906D0F1D6D40B246A946AFFFB435929AB500B5D2E7E65688D33F695FAB80ABB426D2D4DEFC013550E13DCC7FFFC52AB02A7E102C17B124840C6E827CBDB003476AF93B5D63E889E732171F2805429A4F10721A673E95FDDCCBE8F9671C40BAC185D31CE361BFFB0279D86AF95F010000";
                }
                else if (TruckName.Equals("Mercedes New Actros"))
                {
                    decode = "1F8B0800FECC995E00FF5590CB6EC3201045F791F20F288BAA95A200CE8071BEA28BEED10043436DD796EDB652BFBE86F4E1EEAE46E78E8EEED3F4E6DB474CAFCB7EC77A9C5B3B593F74C37461F777E75857A67270646B144109AC720C4D1D0398879FC6F3A6119431A0F1C844264928A88CF825DD8674A0C969F94D0659CB46AB4CC60E5BDA80643440A3CA3F65B4A4A614B006756E6E8534FE937635F850A41591A278F3C7A09032EF70FEFB2F8A4051FC205C2F624D0117B4232ED70B3BF04091BFD335F98EF892E7E23D4D9E02CD27F4CB34CC9590C0C7BCA17D191CFF4C3D4ADB83F5A739A5C37EF70521AA0E6664010000";
                }
                else if (TruckName.Equals("Renault T"))
                {
                    decode = "1F8B080009CD995E00FF4D90C16EC3201044EF91F20F2887AA91A28031189CAFE8A177B4C0D2503BB185492BF5EB6BDC24F56DB4FB66349AF77473DD1BC46BDE6EC805A6CE24E3867E4827F2FA5207C535B7E24066C9BC64C08BF4AD0A5EE8FDC3F1B17278A9B568E04058219149C1357B9276455AD1A06DAA3BE92B55B58D2C64E8A1C327582D39812B0EBA48E72486A6FE03E3B80A74A295CAF97BA0ABEB16F952D2C2F49FC796FF52E91B61BEB05979C86046C8E713D9518F817EE139BA1E692EF3D08457B8F5F998E95896329F83A53FF102266503C729C6DD76F30B14EF9B5A49010000";
                }
                else if (TruckName.Equals("Renault Premium"))
                {
                    decode = "1F8B080015CD995E00FF5590CB6EC3201045F791F20F288BAA95A2801DC090AFE8A27B3498A1A1B6630BE356EAD7D7903EDCDDD5E8DCD1D17D894BDB3D43B8A5FD8E0C3077269A76ECC778218F0F67DFD4AAB6FC48D6C89C6050E7E874E31D574F3F8DD74DC309A5B884236199442678ADD82F6937A4E512ADACBE49573595962293BE870E37202AC9B916E59F50B2425D0AD07071D6F74298FE49DB86B7AE480B4481FEEE0F4E0066DEC2FCF79F1581A2F881B05ED89A1C243013A4EB851CA8434FDFF11ADA1E69CA73D1883758FA749A220E6119E894F7336FA3A59F6180CAC4C9C0690EE1B0DF7D010A13CD5360010000";
                }
                else if (TruckName.Equals("Renault Magnum"))
                {
                    decode = "1F8B080021CD995E00FF5590CB6EC3201045F791F20F288BAA95A2000E609CAFE8A27B349821A136B685ED56EAD7D7387D38BBABD1B9A3A3FB96E6BA7985D04DFB1D8930362699BA6FFB7421CF4F675F16BAB0E24896C89C6450E4E8AAD23BA15F7E1BD74DC349AD8582236199442645A1D91F6937A4150AADE23FA4E325AF94CCA46FA1C10D885A0951C9F59FD48A63B516A014F25CDD0B617890B6A5A8DD2A2D1125FABB3F380998790BE3FF7FB60AAC8A9F08CB852DC9C1046680E9762107EAD0D30FBC85BA453AE5B968C20EE6763A45B87673A4439ECFBCF7967E8508DCA468E0348670D8EFBE01393111805F010000";
                }
                else if (TruckName.Equals("DAF XF105"))
                {
                    decode = "1F8B080026CD995E00FF5590CB6AC3301045F781FC83C8A2B41022C9D1335FD145F766E41925AA9DDAD8EE837E7D2DA50F777719CE1D0EF7697C6DDA47482FF376C3AE30B5F558377DD78F27767F778CB67255507BB644815A4095237A1B51B9879FC679D540ED9C32B067229324B4AA9CF825C38A0CCA5030F29B4469A5373A93B18396562039A394D7E59F7646922F05B04A1FFDAD90867FD2C1AA068BB426D2146FFE801A28F301A6BFFFA20814C57782E522968430433DC07C39B11D478AFC8D2EA9E988CF792E8E100F1F910F79B6FAB90FFC335D41D6709852DA6D375FBC830ABD54010000";
                }
                else if (TruckName.Equals("DAF XF"))
                {
                    decode = "1F8B080097B6AD5E00FF5590CB4EC3301045F795FA0F561708A4AA76523FFB152CD85BE3CC989AA4244A52407C3DB1CB23ECAE46E78E8EEED3786DDA4748AFF376C32E30B57EF44DDFF5E389DDDF1DA3A96D1DE49E2D51A01250E788CE4494F6E1A7F1BC6AA0B2566AD8339149124AD656FC92614506A929E8EA9BC4CA544EAB4CC60E5A5A8164B5944E957FCAEA8A5C298091EAE86E8534FC930E463658A41591A278F3075440990F30FDFD1745A028BE132C17B1248419FC00F3F9C4761C29F2373AA7A6233EE7B938423C7C444FD7B1D77CC8E3F9973EF0CF7481CAA3F6709852DA6D375FB90F4A5A5D010000";
                }
                else if (TruckName.Equals("Scania S"))
                {
                    decode = "1F8B080045CD995E00FF5590CB6EC3201045F791F20F5616552B45051C0670BEA28BEED1004343EDD896715BA95F5FE3F4E1EEAE66CE1D1DCDF3F4E6DB274CFDBCDF5557CCAD9DAC1FBA613A57F777A7A86B533B79AC96C80370AC4B0C8D8E419A879FC6CBA611C018A9F058F1421207591BFE4BBA0DE9A422A7C4371984168D8242C60E5BDA806494940DACF7C02841CD5A402DE1D4DC0A69FC27EDB4F461950622A078F3C70048857798FFEEF3556055FC205C267C490167B423CE9773756081227BA74BF21DB1B9BC8B658F7DC2C76C6B2E141BCBF7ECEBE0D867BAA2B039F77659A674D8EFBE00091103C15F010000";
                }
                else if (TruckName.Equals("Volvo FH 2012"))
                {
                    decode = "1F8B0800E300D85E00FF5590CB6AC3301045F781FC83C8A2B410A2472459CA5774D1BD1859A358B5531BDB75A15F5F4BE9C3DD5D867387C37D19DFEBF619D2DBBCDF911B4CAD1B5DDD77FD78218F0FE7580923BC3C9235B2A018881C83AD6290E6E9A771DD348232466A38129649644A0AC37E49BF21BDD4E835FF2603AFB8D52A93B1831637201A2DA555E59F329AA32D05A8A43ADB7B210DFFA47D25EB50A415A2C278F787A00033EF61FAFBCF8A4051FC40582F6C4D01667003CCCD851C68C048176C52DD219DF35C74E9BBA53FC5866B27181774C8FBB9D7DED3CF7403EE16E1E034A574D8EFBE003338CCD460010000";
                }
                else if (TruckName.Equals("Scania R 2009"))
                {
                    decode = "1F8B08002502D85E00FF5590CB6EC3201045F791F20F288BAA95A2801DC090AFE8A27B34C090503BB1056E2BF5EB6B481FEEEE6A74EEE8E8BEA437D73F43BCCDDB0DB942EE4D326E1CC674228F0FC7D0B5AAB57C4F96C8BC60D096E875173C574F3F8DF3AAE185525CC29EB0422213BC55EC97B42BD272895636DFA46FBA464B51C830408F2B1095E45C8BFA4F28D9A0AE05E8B838EA7B214EFFA46DC79DAFD2025160B8FB83178085B790FFFEB32A50153F10960B5B928719CC04F3E54476D463A0EF78896E403A97B96876708B7048742AC399D7D1D2CF7885C6E464E09063DC6D375F8819B90859010000";
                }
                else if (TruckName.Equals("Volvo FH16 2009"))
                {
                    decode = "1F8B0800FA00D85E00FF5590CB6E83301045F791F20F5616552B45B14D3CC6E42BBAE8DE1ADBE3E0420A024AA57E7D31E983EEAE46E78E8EEECBF0EE9B674C6FD37EC76E383676B0BE6BBBE1C21E1FCEB12C4CE1D4912D51041058E418AA3206659E7E1AD74D2380314AE391894C92005518F14BBA0DE99426A7E5371964292B0D998C2D36B401C968A52A58FF81D192AAB580A58273752FA4FE9FB42B950FAB341001C5BB3F0640CABCC3F1EFBF580556C50FC2E52296147042DBE3545FD881078A7CA63AF996F894E7E273D7CEDD29D652F33E4F675F3BC73FD30DA59DAD3F8D291DF6BB2FC4D8EBF55A010000";
                }
                else if (TruckName.Equals("Scania R"))
                {
                    decode = "1F8B08001701D85E00FF5590CB6AC3301045F781FC83C9A2341022C9D1CBF98A2EBA17236994A8766C23B92DF4EB6B394DEBEE2EC3B9C3E1BEA677D7BE40ECA7EDA6BA416E4D326EE88674AE9E9F4E41D5BAB6FC50CD917A41A12ED1372A78AEF78FC665D5F0426B2EE150D1422215BCD6F497B42BD2728956B21FD233C51A290A193A687105A2969C3762F927B464D82C05505C9C9A7B218EFFA4ADE2CE2FD2025160B8FB83178085B790FFFED3456051FC44982F744E1E2630234CD773B5231E03F9C06B741D92A9CC45B2833EC231999A3249C6B29E791B2CF98A376026A7DEC031C7B8DB6EBE0101D913CF5F010000";
                }
                else if (TruckName.Equals("Scania Streamline"))
                {
                    decode = "1F8B0800F9D09D5E00FF5590CB4EC3301045F795FA0F5117884A1576528F1FFD0A16ECAD713CA6264FC50124BE9E38051A765733E78E8EE6657AAF9B678CFDBCDF151DA6C64EB61EDA61BA148F0FE7A02A5D39712A96C83D70AC72F446052FF4F1B7F1BA6978D05A483C153C93C441549AFF916E433A21C9C9F287F4A52A8D844C86161BDA80A4A51006D67BA06549662DA0127036B7421CFF493B256ABF4A031150B8F9A307A4CC3B4CF7FB7C1558153F0997095F92C719ED88F3F5521C98A7C03EE81AEB96D89CDFC5528D7DC4A7344F845D1B7B6263FEA07D1B1CFB8A1D963625BBEC633CEC77DF544C478062010000";
                }
                else
                {
                    MessageBox.Show("We got no painting for this truck.");
                    return;
                }

                string inputData = Utilities.ZipDataUtilitiescs.unzipText(decode);
                string[] Lines = inputData.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                if (Lines[0] == "TruckPaint")
                {
                    List<string> paintstr = new List<string>();
                    for (int i = 1; i < Lines.Length; i++)
                    {
                        paintstr.Add(Lines[i]);
                    }

                    UserTruckDictionary[comboBoxUserTruckCompanyTrucks.SelectedValue.ToString()].Parts.Find(xp => xp.PartType == "paintjob").PartData = paintstr;

                    MessageBox.Show("Paint data  has been inserted.");
                }
                else
                    MessageBox.Show("Wrong data. Expected Paint data but\r\n" + Lines[0] + "\r\nwas found.");
            }
            catch
            {
                MessageBox.Show("Something gone wrong with decoding.");
            }
        }
        //end Share buttons
        //end User Trucks tab
    }
}